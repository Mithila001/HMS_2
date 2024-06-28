using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS_Software_V2.UserCommon_Forms.UserControls_UCF
{
    /// <summary>
    /// Interaction logic for UC_UCF_LabRequest.xaml
    /// </summary>
    public partial class UC_UCF_LabRequest : UserControl
    {
        public event Action? AddLabRequestClicked;

        public UC_UCF_LabRequest()
        {
            InitializeComponent();
            GetLabInfoFromDatabase();

            investigationTypeSearch_listBox.SelectionChanged += investigationTypeSearch_tbx_SelectionChanged;
            specimentSearch_ListBox.SelectionChanged += specimentSearch_tbx_SelectionChanged;
        }

        private List<(int,string)> LabInvestigations = new List<(int, string)>();
        private List<(int, string)> LabSpeciment = new List<(int, string)>();
        private void GetLabInfoFromDatabase()
        {

            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM Lab_Investigation";
                string query2 = "SELECT * FROM Lab_Specimen"; 

                try
                {
                    connection.Open();


                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        int id = Convert.ToInt32(reader1["Lab_Investigation_ID"]);
                        string name = (string)reader1["Lab_Investigation_Name"];

                        LabInvestigations.Add((id, name));
                    }
                    reader1.Close();

                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                    SQLiteDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        int id = Convert.ToInt32(reader2["Lab_Specimen_ID"]);
                        string name = (string)reader2["Lab_Specimen_Name"];

                        LabSpeciment.Add((id, name));
                    }
                    reader2.Close();
                }

                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }

        private List<string> data = new List<string>
        {
            "Alexander", "Amelia", "Aria", "Asher", "Ava", "Anthony", "Aiden", "Andrew", "Aurelia", "Arlo"
        };


        private List<string> data2 = new List<string>
        {
            "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew","Ann" ,"Annyooooooooooooo"
        };


        // For Display the ListBox when the user types in the TextBox
        private void specimentSearch_tbx_KeyUp(object sender, KeyEventArgs e)
        {
            string query = specimentSearch_tbx.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                specimentSearch_ListBox.ItemsSource = null;
                specimentSearch_popup.IsOpen = false;
                return;
            }

            var filteredData = LabSpeciment.Select(item => item.Item2).Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            specimentSearch_ListBox.ItemsSource = filteredData;
            specimentSearch_popup.IsOpen = filteredData.Any();

            specimentSearch_ListBox.Height = filteredData.Count * 20;
            specimentSearch_ListBox.Height += 5;
        }

        private void investigationTypeSearch_tbx_KeyUp(object sender, KeyEventArgs e)
        {
            string query = investigationTypeSearch_tbx.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                investigationTypeSearch_listBox.ItemsSource = null;
                investigationTypeSearch_popup.IsOpen = false;
                return;
            }

            var filteredData = LabInvestigations.Select(item => item.Item2).Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            investigationTypeSearch_listBox.ItemsSource = filteredData;
            investigationTypeSearch_popup.IsOpen = filteredData.Any();

            investigationTypeSearch_listBox.Height = filteredData.Count * 20;
            investigationTypeSearch_listBox.Height += 5;
        }


        // ---------------------------------------------------------------------------------------------------------------

        private WrapPanel? parent;

        public void SetParent(WrapPanel parent)
        {
            this.parent = parent;
        }


        bool isClicked = false; // Flag
        static int LabRequestAddCount = 1;


        private void AddLabRequest_btn_Click(object sender, RoutedEventArgs e)
        {
            if(isClicked) // If the button is already clicked, remove the user control
            {
                if (parent != null)
                {
                    parent.Children.Remove(this);
                }
                return;
            }

            LabRequestCount_lbl.Content = LabRequestAddCount.ToString();

            isClicked = true;
            LabRequestAddCount += 1;

            // Raise the event
            AddLabRequestClicked?.Invoke();
            Debug.WriteLine("AddLabRequest_btn Clicked");

            // Change the image source
            var image = (Image)AddLabRequestViewbox.Child;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/HMS_Software_V2;component/Assest/icons/icons8-delete-100.png"));

            // Lower the image scale
            AddLabRequestViewbox.Width = AddLabRequestViewbox.Height = 50;

            // Change the button background color
            AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA5882"));

            // Add hover and click effects
            AddLabRequest_btn.MouseEnter += (s, e) => AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0040"));
            AddLabRequest_btn.MouseLeave += (s, e) => AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA5882"));
            AddLabRequest_btn.PreviewMouseDown += (s, e) => AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0040"));
        }


        public string? InvestgationType_selected;
        public int InvestgationID_selected;
        // For Select an item from the ListBox
        private void investigationTypeSearch_tbx_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if (investigationTypeSearch_listBox.SelectedItem != null)
            {
                string? selectedItem = investigationTypeSearch_listBox.SelectedItem.ToString();

                // Find the ID that matches the selected item
                var matchingItem = LabInvestigations.FirstOrDefault(item => item.Item2 == selectedItem);

                if (matchingItem != default)
                {
                    InvestgationID_selected = matchingItem.Item1;
                    // Now you can use id
                }

                // Assign the selected item's value to the TextBox
                investigationTypeSearch_tbx.Text = selectedItem;
                // Clear the ListBox's selection
                investigationTypeSearch_listBox.SelectedItem = null;
                // Close the popup
                investigationTypeSearch_popup.IsOpen = false;

                InvestgationType_selected = selectedItem;
                Debug.WriteLine("\nInvestgationType_selected: " + InvestgationType_selected);
                Debug.WriteLine("InvestgationID_selected: " + InvestgationID_selected);
            }




            
        }


        public string? SpecimenType_selected;
        public int SpecimenID_selected;
        private void specimentSearch_tbx_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (specimentSearch_ListBox.SelectedItem != null)
            {
                string? selectedItem = specimentSearch_ListBox.SelectedItem.ToString();

                var matchingItem = LabSpeciment.FirstOrDefault(item => item.Item2 == selectedItem);

                if (matchingItem != default)
                {
                    SpecimenID_selected = matchingItem.Item1;
                    // Now you can use id
                }
                SpecimenType_selected = selectedItem;
                Debug.WriteLine("\nSpecimenType_selected: " + SpecimenType_selected);
                Debug.WriteLine("SpecimenID_selected: " + SpecimenID_selected);

                specimentSearch_tbx.Text = selectedItem;
                specimentSearch_ListBox.SelectedItem = null;
                specimentSearch_popup.IsOpen = false;
            }
        }




        // If the user clicks an another TextBox, close the rest of the popup
        private void investigationTypeSearch_tbx_GotFocus(object sender, RoutedEventArgs e)
        {

            specimentSearch_ListBox.ItemsSource = null;
            specimentSearch_popup.IsOpen = false;

        }

        private void specimentSearch_tbx_GotFocus(object sender, RoutedEventArgs e)
        {
            investigationTypeSearch_listBox.ItemsSource = null;
            investigationTypeSearch_popup.IsOpen = false;

        }

    }
}
