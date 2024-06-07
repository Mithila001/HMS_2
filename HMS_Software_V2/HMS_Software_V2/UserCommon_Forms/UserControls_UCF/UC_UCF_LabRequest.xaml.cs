using System;
using System.Collections.Generic;
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
        public UC_UCF_LabRequest()
        {
            InitializeComponent();
        }

        private List<string> data = new List<string>
        {
            "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew","Ann" ,"Annyooooooooooooo"
        };

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string query = SearchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                SearchResultsListBox.ItemsSource = null;
                SearchResultsPopup.IsOpen = false;
                return;
            }

            var filteredData = data.Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            SearchResultsListBox.ItemsSource = filteredData;
            SearchResultsPopup.IsOpen = filteredData.Any();


            // Assuming each item in the ListBox has a height of 20
            SearchResultsListBox.Height = filteredData.Count * 20;
        }

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
    }
}
