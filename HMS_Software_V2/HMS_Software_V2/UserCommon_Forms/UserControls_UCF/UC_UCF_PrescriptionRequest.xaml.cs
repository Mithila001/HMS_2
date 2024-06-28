using HMS_Software_V2.General_Purpose;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
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
    /// Interaction logic for UC_UCF_PrescriptionRequest.xaml
    /// </summary>
    public partial class UC_UCF_PrescriptionRequest : UserControl
    {
        public event Action? AddPrescription;


        public UC_UCF_PrescriptionRequest()
        {
            InitializeComponent();
            MyGetMedicinData();


            MedicinSearch_listBox.SelectionChanged += MedicinSearch_tbx_SelectionChanged;
        }

        public string? SelectedRoute;
        public string? SelectedDosage;
        public string? SelectedDFrequency;
        public string? SelectedDuration;




        private List<(int, string)> MedicalData = new List<(int, string)>();
        private void MyGetMedicinData()
        {
            using (SQLiteConnection connection = new Database_Connector().GetConnection())
            {
                string query1 = "SELECT * FROM Medicin_Storage";

                SQLiteCommand cmd = new SQLiteCommand(query1, connection);

                try
                {
                    connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Medicin_ID"]);
                        string name = (string)reader["Medicin_Name"];

                        MedicalData.Add((id, name));

                    }
                    reader.Close();
                }

                catch (SQLiteException ex)
                {
                    Debug.WriteLine("\nError1: \n" + ex.Message);
                    MessageBox.Show("Error1: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }

        private void MedicinSearch_tbx_KeyUp(object sender, KeyEventArgs e)
        {
            string query = MedicinSearch_tbx.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                MedicinSearch_listBox.ItemsSource = null;
                MedicinSearch_popup.IsOpen = false;
                return;
            }
            // Select(item => item.Item2) to select the 2nd item from the tuple
            var filteredData = MedicalData.Select(item => item.Item2).Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            MedicinSearch_listBox.ItemsSource = filteredData;
            MedicinSearch_popup.IsOpen = filteredData.Any();

            MedicinSearch_listBox.Height = filteredData.Count * 20;
            MedicinSearch_listBox.Height += 5;
        }


        // To remove this user control from the parent WrapPanel
        private WrapPanel? parent;
        public void SetParent(WrapPanel parent)
        {
            this.parent = parent;
        }

        bool isClicked = false; // Flag
        private void AddPrescription_btn_Click(object sender, RoutedEventArgs e)
        {
            

            if (isClicked) // If the button is already clicked, remove the user control
            {
                if (parent != null)
                {
                    parent.Children.Remove(this);
                }
                return;
            }

            isClicked = true;

            AddPrescription?.Invoke();

            // Change the image source
            var image = (Image)AddPrescription_Viewbox.Child;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/HMS_Software_V2;component/Assest/icons/icons8-delete-100.png"));

            // Lower the image scale
            AddPrescription_Viewbox.Width = AddPrescription_Viewbox.Height = 50;

            // Change the button background color
            AddPrescription_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA5882"));

            // Add hover and click effects
            AddPrescription_btn.MouseEnter += (s, e) => AddPrescription_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0040"));
            AddPrescription_btn.MouseLeave += (s, e) => AddPrescription_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA5882"));
            AddPrescription_btn.PreviewMouseDown += (s, e) => AddPrescription_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0040"));
        }



        public string? MedicinName_Selected;
        public int MedcinID_Selected;
        private void MedicinSearch_tbx_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (MedicinSearch_listBox.SelectedItem != null)
            {
                string? selectedItem = MedicinSearch_listBox.SelectedItem.ToString();


                // Find the ID that matches the selected item
                var matchingItem = MedicalData.FirstOrDefault(item => item.Item2 == selectedItem);

                if (matchingItem != default)
                {
                    MedcinID_Selected = matchingItem.Item1;
                    // Now you can use id
                }
                MedicinName_Selected = selectedItem;
                Debug.WriteLine("\nMedicinName_Selected: " + MedicinName_Selected);
                Debug.WriteLine("MedcinID_Selected: " + MedcinID_Selected);


                MedicinSearch_tbx.Text = selectedItem;
                MedicinSearch_listBox.SelectedItem = null;
                MedicinSearch_popup.IsOpen = false;
            }
        }

        private void MedicinSearch_tbx_GotFocus(object sender, RoutedEventArgs e)
        {
            MedicinSearch_listBox.ItemsSource = null;
            MedicinSearch_popup.IsOpen = false;

            MedicinSearch_listBox.ItemsSource = null;
            MedicinSearch_popup.IsOpen = false;
        }

        private void AddDuration_tbx_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (SelectDosage_ComboBox.SelectedItem == null || SelectDuration_comboBox.SelectedItem == null)
            {
                return;
            }
            SelectedRoute = (SelectRoute_comboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";
            SelectedDFrequency = (SelectFrequency_comboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";


            SelectedDosage = $"{Dosage_tbx.Text} , {(SelectDosage_ComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? ""}";
            SelectedDuration = $"{AddDuration_tbx.Text} , {(SelectDuration_comboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? ""}";
        }
    }
}
