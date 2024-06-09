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
    /// Interaction logic for UC_UCF_PrescriptionRequest.xaml
    /// </summary>
    public partial class UC_UCF_PrescriptionRequest : UserControl
    {
        public event Action? AddPrescription;

        public UC_UCF_PrescriptionRequest()
        {
            InitializeComponent();

            MedicinSearch_listBox.SelectionChanged += MedicinSearch_tbx_SelectionChanged;
        }

        private List<string> data = new List<string>
        {
            "Alexander", "Amelia", "Aria", "Asher", "Ava", "Anthony", "Aiden", "Andrew", "Aurelia", "Arlo"
        };

        private void MedicinSearch_tbx_KeyUp(object sender, KeyEventArgs e)
        {
            string query = MedicinSearch_tbx.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                MedicinSearch_listBox.ItemsSource = null;
                MedicinSearch_popup.IsOpen = false;
                return;
            }

            var filteredData = data.Where(item => item.ToLower().Contains(query)).Take(10).ToList();
            MedicinSearch_listBox.ItemsSource = filteredData;
            MedicinSearch_popup.IsOpen = filteredData.Any();

            MedicinSearch_listBox.Height = filteredData.Count * 20;
            MedicinSearch_listBox.Height += 5;
        }

        private WrapPanel? parent;

        public void SetParent(WrapPanel parent)
        {
            this.parent = parent;
        }

        bool isClicked = false; // Flag
        static int PrescriptionRequestAddCount = 1;

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

            PerscriptionRequestCount_lbl.Content = PrescriptionRequestAddCount.ToString();
            isClicked = true;
            PrescriptionRequestAddCount += 1;

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

        private void MedicinSearch_tbx_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (MedicinSearch_listBox.SelectedItem != null)
            {
                string? selectedItem = MedicinSearch_listBox.SelectedItem.ToString();
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
    }
}
