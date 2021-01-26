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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.Reporting;
using Microsoft.ReportingServices;
using Microsoft.Reporting.WinForms;

namespace zadanie
{
    /// <summary>
    /// Logika interakcji dla klasy Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://desktop-mo4ab4g:80/ReportServer", System.UriKind.Absolute);
            rptViewer.ServerReport.ReportPath = "/Test/Report1";
            rptViewer.RefreshReport();
        }
    }
}
