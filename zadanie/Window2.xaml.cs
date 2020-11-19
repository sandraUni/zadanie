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
using Microsoft.Win32;

namespace zadanie
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Uri fileUri;

        public Window2(int i)
        {
            InitializeComponent();

            text_index.Text = Convert.ToString(MainWindow.personList[i].index);
            text_surname.Text = MainWindow.personList[i].surname;
            text_name.Text = MainWindow.personList[i].name;
            text_pesel.Text = Convert.ToString(MainWindow.personList[i].pesel);
            text_age.Text = Convert.ToString(MainWindow.personList[i].age);
            imgDynamic.Source = MainWindow.personList[i].image;
            index.Content = i;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.personList[Convert.ToInt32(index.Content)].index = Convert.ToInt32(text_index.Text);
            MainWindow.personList[Convert.ToInt32(index.Content)].surname = text_surname.Text;
            MainWindow.personList[Convert.ToInt32(index.Content)].name = text_name.Text;
            MainWindow.personList[Convert.ToInt32(index.Content)].pesel = Convert.ToInt64(text_pesel.Text);
            MainWindow.personList[Convert.ToInt32(index.Content)].age = Convert.ToInt32(text_age.Text);
            MainWindow.personList[Convert.ToInt32(index.Content)].image = new BitmapImage(fileUri);
            this.Close();
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog.ShowDialog() == true)
            {
                fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
                
            }

        }
    }
}
