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
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;

namespace zadanie
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Student> personList = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Student>));
            using (Stream s = File.OpenRead("../list.xml"))
            {
                personList = (List<Student>)xs.Deserialize(s);
            }
            list.ItemsSource = personList;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }


        private void List_Click(object sender, MouseButtonEventArgs e)
        {
            int i = (sender as ListView).SelectedIndex;
            if (i > -1)
            {
                Window2 window2 = new Window2(i);
                window2.Show();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = null;
            list.ItemsSource = personList;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Student>));
            using (Stream s = File.Create("../list.xml"))
            {
                xs.Serialize(s, personList);
            }

        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data source=DESKTOP-MO4AB4G\MSSQLSERVER04; database=StudentList; Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            MessageBox.Show("Open connection!");
            cnn.Close();

        }
    }
}
