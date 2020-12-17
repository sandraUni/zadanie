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

        private void SelectionChangedDataGrid(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = sender as DataGrid;
            DataRowView datarow = datagrid.SelectedItem as DataRowView;
            if (datarow != null)
            {
                text_index.Text = datarow["Index"].ToString();
                text_surname.Text = datarow["Surname"].ToString();
                text_name.Text = datarow["Name"].ToString();
                text_pesel.Text = datarow["Pesel"].ToString();
                text_age.Text = datarow["Age"].ToString();
                image_path.Text = datarow["Image"].ToString();

                Uri uri = new Uri(image_path.Text);
                imgDynamic.Source = new BitmapImage(uri);
            }
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

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            text_index.Text = "";
            text_surname.Text = "";
            text_name.Text = "";
            text_pesel.Text = "";
            text_age.Text = "";
            image_path.Text = "";
            imgDynamic.Source = null;
        }

        private void FunctionTab_Click(object sender, RoutedEventArgs e)
        {
            sql = "SELECT * FROM CountAge()";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(1) + " yers - " + dataReader.GetValue(0) + " Students\n";
            }

            MessageBox.Show(Output);
            command.Dispose();
            dataReader.Close();
            Output = "";
        }

        private void FunctionScalar_Click(object sender, RoutedEventArgs e)
        {
            sql = "SELECT dbo.AverageAge();";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0);
            }

            MessageBox.Show("The average age of students is: " + Output);
            dataReader.Close();
            Output = "";
        }

        private void Wiev_Click(object sender, RoutedEventArgs e)
        {
            sql = "SELECT * FROM myview;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + "\n";
            }

            MessageBox.Show(Output);
            dataReader.Close();
            Output = "";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            if (!Regex.IsMatch(e.Text, "[^a-z]+"))
            {
                e.Handled = true;
                MessageBox.Show("Tylko wartości liczbowe.");
            }
        }
    }
}
