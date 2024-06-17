using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace HMS_Software_V2.Nurse_Ward
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();

            DataTable dataTable = new DataTable();
            // Define columns.
            dataTable.Columns.Add("Medicin", typeof(string));
            dataTable.Columns.Add("Route", typeof(string));
            dataTable.Columns.Add("Dosage", typeof(string));
            dataTable.Columns.Add("Frequency", typeof(string));
            dataTable.Columns.Add("Duration", typeof(string));
            dataTable.Columns.Add("LabelNo", typeof(string));

            // Manually add 10 records
            dataTable.Rows.Add("Medicin1", "Oral", "500mg", "Twice a day", "7 days", "001");
            dataTable.Rows.Add("Medicin2", "Injection", "250mg", "Once a day", "5 days", "002");
            dataTable.Rows.Add("Medicin3", "Oral", "100mg", "Three times a day", "10 days", "003");
            dataTable.Rows.Add("Medicin4", "Topical", "N/A", "As needed", "N/A", "004");
            dataTable.Rows.Add("Medicin5", "Oral", "50mg", "Once a day", "14 days", "005");
            dataTable.Rows.Add("Medicin6", "Inhalation", "N/A", "Twice a day", "30 days", "006");
            dataTable.Rows.Add("Medicin7", "Oral", "75mg", "Once a day", "7 days", "007");
            dataTable.Rows.Add("Medicin8", "Subcutaneous", "300mg", "Once a week", "4 weeks", "008");
            dataTable.Rows.Add("Medicin9", "Oral", "20mg", "Twice a day", "5 days", "009");
            dataTable.Rows.Add("Medicin10", "Oral", "10mg", "Once a day", "3 days", "010");

            showPrescriptionReq_DataGrid2.ItemsSource = dataTable.DefaultView;


        }
    }
}
