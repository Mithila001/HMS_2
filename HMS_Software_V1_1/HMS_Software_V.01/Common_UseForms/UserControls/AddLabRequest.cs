using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Common_UseForms.UserControls
{
    public partial class AddLabRequest : UserControl
    {
        // Come from the MakeLabRequest From
        public int LabInvestigationsID { get; set; }
        public int SpecimenNameID { get; set; }
        public AddLabRequest()
        {
            InitializeComponent();
        }
    }
}
