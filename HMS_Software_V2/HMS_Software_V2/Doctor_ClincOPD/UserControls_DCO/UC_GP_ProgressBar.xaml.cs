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

namespace HMS_Software_V2.General_Purpose.General_UserControls
{
    /// <summary>
    /// Interaction logic for UC_GP_ProgressBar.xaml
    /// </summary>
    public partial class UC_GP_ProgressBar : UserControl
    {
        public UC_GP_ProgressBar()
        {
            InitializeComponent();

            UpdateProgressBar(0);
        }

        public void UpdateProgressBar(double percentage)
        {
            // Ensure the percentage is within bounds
            percentage = Math.Max(0, Math.Min(100, percentage));

            // Calculate the angle in degrees
            double angle = (percentage / 100) * 360;

            // Convert angle to radians for calculations
            double radians = (Math.PI / 180) * angle;

            // Calculate the end point of the arc
            double x = 100 * Math.Sin(radians);
            double y = -100 * Math.Cos(radians);

            // Determine if the arc should be large (more than 50%)
            bool isLargeArc = percentage > 50;

            if (progressPath.Data is PathGeometry geometry && geometry.Figures.Count > 0)
            {
                PathFigure figure = geometry.Figures[0];
                if (figure.Segments.Count > 1 && figure.Segments[1] is ArcSegment arcSegment)
                {
                    // Update the ArcSegment properties
                    arcSegment.Point = new Point(x, y);
                    arcSegment.IsLargeArc = isLargeArc;
                }
            }
        }
    }
}
