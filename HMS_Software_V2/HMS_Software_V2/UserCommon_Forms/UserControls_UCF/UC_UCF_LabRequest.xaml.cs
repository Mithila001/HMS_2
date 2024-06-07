using System;
using System.Collections.Generic;
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
    /// Interaction logic for UC_UCF_LabRequest.xaml
    /// </summary>
    public partial class UC_UCF_LabRequest : UserControl
    {
        public event Action? AddLabRequestClicked;

        public UC_UCF_LabRequest()
        {
            InitializeComponent();

            investigationTypeSearch_listBox.SelectionChanged += investigationTypeSearch_tbx_SelectionChanged;
        }

        private List<string> data = new List<string>
            {
                "Alexander", "Amelia", "Aria", "Asher", "Ava", "Anthony", "Aiden", "Andrew", "Aurelia", "Arlo"
            };


        private List<string> data2 = new List<string>
            {
                "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew","Ann" ,"Annyooooooooooooo"
            };

        private void specimentSearch_tbx_KeyUp(object sender, KeyEventArgs e)
        {
            string query = specimentSearch_tbx.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                specimentSearch_ListBox.ItemsSource = null;
                specimentSearch_popup.IsOpen = false;
                return;
            }

            var filteredData = data2.Where(item => item.ToLower().Contains(query)).Take(10).ToList();
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

            var filteredData = data.Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            investigationTypeSearch_listBox.ItemsSource = filteredData;
            investigationTypeSearch_popup.IsOpen = filteredData.Any();

            investigationTypeSearch_listBox.Height = filteredData.Count * 20;
            investigationTypeSearch_listBox.Height += 5;
        }

        private void RequestType_tbx_KeyUp(object sender, KeyEventArgs e)
        {
            string query = RequestType_tbx.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                RequestType_ListBox.ItemsSource = null;
                RequestType_PopUp.IsOpen = false;
                return;
            }

            var filteredData = data.Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            RequestType_ListBox.ItemsSource = filteredData;
            RequestType_PopUp.IsOpen = filteredData.Any();

            RequestType_ListBox.Height = filteredData.Count * 20;
            RequestType_ListBox.Height += 5;
        }



        private void RequestType_tbx_LostFocus(object sender, RoutedEventArgs e)
        {
            RequestType_ListBox.ItemsSource = null;
            RequestType_PopUp.IsOpen = false;
        }

        private void specimentSearch_tbx_LostFocus(object sender, RoutedEventArgs e)
        {
            RequestType_ListBox.ItemsSource = null;
            RequestType_PopUp.IsOpen = false;
        }

        private void investigationTypeSearch_tbx_LostFocus(object sender, RoutedEventArgs e)
        {
            RequestType_ListBox.ItemsSource = null;
            RequestType_PopUp.IsOpen = false;
        }


        // ---------------------------------------------------------------------------------------------------------------

        private WrapPanel? parent;

        public void SetParent(WrapPanel parent)
        {
            this.parent = parent;
        }


        bool isClicked = false;

        private void AddLabRequest_btn_Click(object sender, RoutedEventArgs e)
        {
            if(isClicked)
    {
                // Check if parent is not null
                if (parent != null)
                {
                    parent.Children.Remove(this);
                }
                return;
            }

            isClicked = true;

            // Raise the event
            AddLabRequestClicked?.Invoke();
            Debug.WriteLine("AddLabRequest_btn Clicked");

            // Change the image source
            var image = (Image)AddLabRequestViewbox.Child;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/HMS_Software_V2;component/Assest/icons/icons8-delete-100.png"));

            // Lower the image scale
            AddLabRequestViewbox.Width = AddLabRequestViewbox.Height = 50;

            // Change the button background color to red
            AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA5882"));

            // Add hover and click effects
            AddLabRequest_btn.MouseEnter += (s, e) => AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0040"));
            AddLabRequest_btn.MouseLeave += (s, e) => AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA5882"));
            AddLabRequest_btn.PreviewMouseDown += (s, e) => AddLabRequest_btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0040"));
        }

        private void investigationTypeSearch_tbx_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected
            if (investigationTypeSearch_listBox.SelectedItem != null)
            {
                // Get the selected item
                string? selectedItem = investigationTypeSearch_listBox.SelectedItem.ToString();

                // Assign the selected item's value to the TextBox
                investigationTypeSearch_tbx.Text = selectedItem;

                // Clear the ListBox's selection
                investigationTypeSearch_listBox.SelectedItem = null;

                // Close the popup
                investigationTypeSearch_popup.IsOpen = false;
            }
        }
    }
}
