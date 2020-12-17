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
using System.Data;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace zadanie
{
    /// <summary>
    /// Logika interakcji dla klasy BaseSQL.xaml
    /// </summary>
    public partial class BaseSQL : Window
    {
        static string connetionString;
        static SqlConnection cnn;
        static SqlCommand command;
        static SqlDataReader dataReader;
        static SqlDataAdapter adapter = new SqlDataAdapter();
        static DataTable dataTable = new DataTable();
        static String sql = "";
        static String Output = "";
        static BitmapImage image;

        public BaseSQL()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connetionString = @"Data source=DESKTOP-MO4AB4G\MSSQLSERVER04; database=StudentList; Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            MessageBox.Show("Connection Open!");
            BindingDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            cnn.Close();
            MessageBox.Show("Connection Close!");
        }

        private void BindingDataGrid()
        {
            command = new SqlCommand();

            command.CommandText = "Select * from List";
            command.CommandType = CommandType.Text;
            command.Connection = cnn;

            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable("List");
            adapter.Fill(dataTable);

            myDataGrid.ItemsSource = dataTable.DefaultView;

            text_index.Text = "";
            text_surname.Text = "";
            text_name.Text = "";
            text_pesel.Text = "";
            text_age.Text = "";
            image_path.Text = "";
            imgDynamic.Source = null;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
                image = new BitmapImage(fileUri);
                image_path.Text = openFileDialog.FileName;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Insert into List ([Index], [Surname], [Name], [Age], [Pesel], [Image]) values(@Index, @Surname, @Name, @Age, @Pesel, @Image)";

            cmd.Parameters.AddWithValue("Index", text_index.Text);
            cmd.Parameters.AddWithValue("Surname", text_surname.Text);
            cmd.Parameters.AddWithValue("Name", text_name.Text);
            cmd.Parameters.AddWithValue("Pesel", text_pesel.Text);
            cmd.Parameters.AddWithValue("Age", text_age.Text);
            cmd.Parameters.AddWithValue("Image", image_path.Text);

            cmd.Connection = cnn;

            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("Data add Sucessfully!");
                BindingDataGrid();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Update List set [Index]=@Index, [Surname]=@Surname, [Name]=@Name, [Age]=@Age, [Pesel]=@Pesel, [Image]=@Image where [Index]=@Index";


            cmd.Parameters.AddWithValue("Index", text_index.Text);
            cmd.Parameters.AddWithValue("Surname", text_surname.Text);
            cmd.Parameters.AddWithValue("Name", text_name.Text);
            cmd.Parameters.AddWithValue("Pesel", text_pesel.Text);
            cmd.Parameters.AddWithValue("Age", text_age.Text);
            cmd.Parameters.AddWithValue("Image", image_path.Text);

            cmd.Connection = cnn;

            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("Information updated!");
                BindingDataGrid();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Delete From List where [Index]=@Index";

            cmd.Parameters.AddWithValue("Index", text_index.Text);

            cmd.Connection = cnn;

            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("Deleted Sucessfully!");
                BindingDataGrid();
            }
        }
    }
}
