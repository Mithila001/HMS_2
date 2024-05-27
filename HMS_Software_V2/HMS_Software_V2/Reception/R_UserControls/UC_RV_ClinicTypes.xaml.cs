﻿using System;
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

namespace HMS_Software_V2.Reception.R_UserControls
{
    /// <summary>
    /// Interaction logic for UC_RV_ClinicTypes.xaml
    /// </summary>
    public partial class UC_RV_ClinicTypes : UserControl
    {
        public UC_RV_ClinicTypes()
        {
            InitializeComponent();
        }


        public string? ClinicType { get; set; }
        public string? Availability { get; set; }

        public event EventHandler<MyClinicTypeEventArgs>? ClinicTypeClicked;

        private void ClinicType_UC_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClinicTypeClicked?.Invoke(this, new MyClinicTypeEventArgs(ClinicType, Availability));
        }
    }
    public class MyClinicTypeEventArgs : EventArgs
    {
        public string ClinicType { get; }
        public string Availability { get; }

        public MyClinicTypeEventArgs(string clinicType, string availability)
        {
            ClinicType = clinicType;
            Availability = availability;
        }
    }
}
