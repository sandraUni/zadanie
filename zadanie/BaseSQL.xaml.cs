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
using System.Data.SqlClient;

namespace zadanie
{
    /// <summary>
    /// Logika interakcji dla klasy BaseSQL.xaml
    /// </summary>
    public partial class BaseSQL : Window
    {
        public BaseSQL()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Base.readBase();

            myDataGrid.ItemsSource = Base.dataTable.DefaultView;
        }
    }
}
