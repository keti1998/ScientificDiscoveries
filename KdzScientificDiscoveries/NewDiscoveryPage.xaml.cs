using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace KdzScientificDiscoveries
{
    /// <summary>
    /// Логика взаимодействия для NewDiscoveryPage.xaml
    /// </summary>
    public partial class NewDiscoveryPage : Page
    {
        public NewDiscoveryPage()
        {
            InitializeComponent();
            textBoxname.Focus();
        }
        List<Discovery> _discoveriesnew;
        List<Discovery> _discoveriesnew1 = new List<Discovery>();


        int date;
        string _nobelYes;
        private void textBoxname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }


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
            else if (nobelNo.IsChecked == true)
            {
                _nobelYes = "НЕТ";
            }


            if (string.IsNullOrWhiteSpace(textBoxname.Text))
            {
                MessageBox.Show("Необходимо ввести название открытия", "Предупреждение");
                textBoxname.Focus();
                return;

            }
            for (int i = 0; i < _discoveriesnew.Count; i++)
            {
                if (_discoveriesnew[i].Name.ToLower() == textBoxname.Text.ToLower())

                {
                    MessageBox.Show("Открытие с таким названием уже существует в данном списке", "Предупреждение");
                    return;
                }
            }


                    if (string.IsNullOrWhiteSpace(textBoxfio.Text))
            {
                MessageBox.Show("Необходимо ввести ФИО учёного ", "Предупреждение");
                textBoxfio.Focus();
                return;
            }




            if (string.IsNullOrWhiteSpace(textBoxcountry.Text))
            {
                MessageBox.Show("Необходимо ввести страну открытия", "Предупреждение");
                textBoxcountry.Focus();
                return;
            }



            if (string.IsNullOrWhiteSpace(textBoxsphere.Text))
            {
                MessageBox.Show("Необходимо ввести сферу открытия", "Предупреждение");
                textBoxsphere.Focus();
                return;

            }

            if (string.IsNullOrWhiteSpace(textBoxyear.Text))
            {
                MessageBox.Show("Необходимо ввести год открытия", "Предупреждение");
                textBoxyear.Focus();
                return;
            }
            if (Convert.ToInt32(textBoxyear.Text) < 0 || Convert.ToInt32(textBoxyear.Text) > 2017)
            {
                MessageBox.Show("Введите корректный год", "Предупреждение");
                return;
            }
            if (nobelNo.IsChecked == false & nobelYes.IsChecked == false)
            {
                MessageBox.Show("Необходимо выбрать, получил ли ученый Нобелевскую премию или нет", "Предупреждение");
                return;
            }
            var discovery = new Discovery(textBoxname.Text, textBoxfio.Text, textBoxsphere.Text, textBoxcountry.Text, date, _nobelYes);
           
            
            _discoveriesnew.Add(discovery);
            _discoveriesnew1.Add(discovery);
            dataGridnew.ItemsSource = null;
            dataGridnew.Columns.Clear();
            dataGridnew.ItemsSource = _discoveriesnew1;

            textBoxcountry.Clear();
            textBoxfio.Clear();
            textBoxname.Clear();
            textBoxsphere.Clear();
            textBoxyear.Clear();
            nobelNo.IsChecked = false;
            nobelYes.IsChecked = false;


            


            using (FileStream fs = new FileStream("../../discoveries.dat", FileMode.Open))
            {
                formatter = new BinaryFormatter();
                formatter.Serialize(fs, _discoveriesnew);
            }
            ;
            MessageBox.Show("Информация об открытии успешно добавлена");

            DiscoveriesPage disc = new DiscoveriesPage();
            NavigationService.Navigate(disc);

            Logger.Log("Добавлено новое открытие.");
        }
        private void textBoxfio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
                MessageBox.Show("Некорректный ввод данных. Введите ФИО учёного", "Предупреждение");
            }
        }

        private void textBoxcountry_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
                MessageBox.Show("Некорректный ввод данных. Введите страну открытия", "Предупреждение");
            }
        }

        private void textBoxsphere_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
                MessageBox.Show("Некорректный ввод данных. Введите сферу открытия", "Предупреждение");

            }
        }

        private void textBoxyear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
                MessageBox.Show("Некорректный ввод данных. Введите год в численном выражении", "Предупреждение");
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        { MessageBoxResult result;
            result = MessageBox.Show("Вы хотите вернуться к списку, не добавив открытия? ", "Возвращение к исходной странице", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {

                NavigationService.GoBack();
                Logger.Log("Пользователь вернулся на исходную страницу.");
                return;
            }
            else
            {
                return;
            }
         
            
        }

        private void buttonAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonAdd.FontWeight = FontWeights.Normal;
            buttonAdd.FontSize = 12;
          
        }

        private void buttonAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonAdd.FontWeight = FontWeights.Bold;
            buttonAdd.FontSize = 16;
         
        }

        private void buttonBack_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonBack.FontWeight = FontWeights.Bold;
            buttonBack.FontSize = 16;
        }

        private void buttonBack_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonBack.FontWeight = FontWeights.Normal;
            buttonBack.FontSize = 12;

        }

        private void textBoxname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Tab))
            {
                textBoxfio.Focus();
            }
        }
    }
}
