using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Media.Imaging;

namespace zadanie
{
    public class Student
    {
        public int index { get; set; }
        public string surname { get; set; }  //nazwisko
        public string name { get; set; }
        public long pesel { get; set; }
        public int age { get; set; }

        [XmlIgnore()]
        public BitmapImage image { get; set; }
        [XmlElement("Imgxml")]
        public string imgxml
        {
            get
            {
                return image.UriSource.ToString();
            }
            set
            {
                image = new BitmapImage(new Uri(value));
            }
        }



        //Konstruktor bezargumentowy (domyślny)
        public Student()
        {
            index = 0;
            surname = "";
            name = "";
            pesel = 0;
            age = 0;
            image = new BitmapImage();
        }

        //Konstruktor wieloargumentowy
        public Student(int index, string surname, string name, long pesel, int age, BitmapImage image)
        {
            this.index = index;
            this.surname = surname;
            this.name = name;
            this.pesel = pesel;
            this.age = age;
            this.image = image;
        }
    }
}
