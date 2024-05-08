using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Reception.UserControls
{
    public partial class Recep_D_ClinicEvents : UserControl
    {
        public Recep_D_ClinicEvents()
        {
            InitializeComponent();

            Console.WriteLine("ClinicID: " + ClinicID);
            Console.WriteLine("ClinicEvnetID: " + ClinicEvnetID);
        }

        public int ClinicID { get; set; }
        public int ClinicEvnetID { get; set; }
        public int UserID {  get; set; }

        private void RPA_assign_btn_Click(object sender, EventArgs e)
        {
            Reception_AppontmentRegister reception_AppontmentRegister = new Reception_AppontmentRegister(ClinicID, ClinicEvnetID, UserID);
            reception_AppontmentRegister.Show();
        }
    }
}
