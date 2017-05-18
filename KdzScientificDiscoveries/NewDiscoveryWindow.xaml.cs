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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KdzScientificDiscoveries
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class NewDiscoveryWindow : Window
    {
        public NewDiscoveryWindow()
        {
            InitializeComponent();
        }
        List<Discovery> _discoveriesnew;
        List<Discovery> _discoveriesnew1 = new List<Discovery>();


        int date;
        string _nobelYes;

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("../../discoveries.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    _discoveriesnew = (List<Discovery>)formatter.Deserialize(fs);

                }
                catch
                {
                    _discoveriesnew = new List<Discovery>();
                }
            }
            int.TryParse(textBoxyear.Text, out date);

            if (nobelYes.IsChecked == true)
            {
                _nobelYes = "ДА";
            }
            else if(nobelNo.IsChecked==true)
            {
                _nobelYes = "НЕТ";
            }


            if (string.IsNullOrWhiteSpace(textBoxname.Text))
            {
                MessageBox.Show("Необходимо ввести название");
                textBoxname.Focus();
                return;

            }

            if (string.IsNullOrWhiteSpace(textBoxfio.Text))
            {
                MessageBox.Show("Необходимо ввести ФИО ученого ");
                textBoxfio.Focus();
                return;
            }

           


            if (string.IsNullOrWhiteSpace(textBoxcountry.Text))
            {
                MessageBox.Show("Необходимо ввести страну");
                textBoxcountry.Focus();
                return;
            }



            if (string.IsNullOrWhiteSpace(textBoxsphere.Text))
            {
                MessageBox.Show("Необходимо ввести сферу открытия");
                textBoxsphere.Focus();
                return;

            }

            if (string.IsNullOrWhiteSpace(textBoxyear.Text))
            {
                MessageBox.Show("Необхоимо ввести год");
                textBoxyear.Focus();
                return;
            }
            if (nobelNo.IsChecked == false & nobelYes.IsChecked == false)
            {
                MessageBox.Show("Необходимо выбрать ,получил ли ученый Нобелевскую премию или нет ");
                return;
            }
            var discovery = new Discovery(textBoxname.Text, textBoxfio.Text, textBoxsphere.Text, textBoxcountry.Text,  date, _nobelYes);
           /* object[] discoveryarr = new object[6];
            discoveryarr[0] = textBoxname.Text;
            discoveryarr[1] = textBoxfio.Text;
            discoveryarr[3] = textBoxcountry.Text;
            discoveryarr[2] = textBoxsphere.Text;
            discoveryarr[4] = textBoxyear.Text;
            discoveryarr[5] = _nobelYes;
            */
            _discoveriesnew.Add(discovery);
            _discoveriesnew1.Add(discovery);


            textBoxcountry.Clear();
            textBoxfio.Clear();
            textBoxname.Clear();
            textBoxsphere.Clear();
            textBoxyear.Clear();
            nobelNo.IsChecked = false;
            nobelYes.IsChecked = false;


            dataGridnew.ItemsSource = null;
            dataGridnew.Columns.Clear();
            dataGridnew.ItemsSource = _discoveriesnew1;

          
            using (FileStream fs = new FileStream("../../discoveries.dat", FileMode.Open))
            {
                formatter = new BinaryFormatter();
                formatter.Serialize(fs, _discoveriesnew);
            }
            DiscoveriesWindow discover = new DiscoveriesWindow();
            MessageBox.Show("Открытие добавлено");
            this.Close();
            discover.Show();
          
            
           
        }

       

        private void textBoxyear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
