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

namespace HMS_Software_V2.Nurse_Ward.NuresWard_UserControls
{
    /// <summary>
    /// Interaction logic for UC_NW_P_LabRequest.xaml
    /// </summary>
    public partial class UC_NW_P_LabRequest : UserControl
    {

        public UC_NW_P_LabRequest( )
        {
            InitializeComponent();
        }

        public int LabRequestID { get; set; }
        bool IsCompleted { get; set; }
    }
}
