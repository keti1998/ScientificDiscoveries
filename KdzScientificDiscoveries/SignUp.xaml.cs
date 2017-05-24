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
using System.Windows.Threading;

namespace KdzScientificDiscoveries
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {

        List<Person> _person;
        public SignUp()
        {
            InitializeComponent();
            textBox.Focus();
            _person = new List<Person>();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Person pers;
            LoadPersonData();
            for (int i = 0; i < _person.Count; i++)
            {
                if (textBox.Text == _person[i].Login)
                {
                    MessageBox.Show("Такой логин уже существует");
                    return;
                }

            }
            if (textBox.Text == "")

            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (passwordBox.Password.ToString() == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (passwordBox1.Password.ToString() == "")
            {
                MessageBox.Show("Повторите пароль");
                return;
            }
            if (passwordBox.Password.ToString() == passwordBox1.Password.ToString())
            {
                pers = new Person(textBox.Text, passwordBox1.Password.ToString());
                _person.Add(pers);
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream("../../persons.dat", FileMode.Open))
                {
                    formatter = new BinaryFormatter();
                    formatter.Serialize(fs, _person);
                }
                NavigationService.Navigate(Pages.LoginPage);
                Logger.Log("Зарегистрирован новый пользователь.");
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
        }
        

        private void LoadPersonData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("../../persons.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    _person = (List<Person>)formatter.Deserialize(fs);

                }
                catch
                {
                    _person= new List<Person>();
                }
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Tab))
            {
                Dispatcher.BeginInvoke(
                        DispatcherPriority.ContextIdle,
                        new Action(delegate ()
                        {
                            passwordBox.Focus();
                        }));
            }
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Tab))
            {
                Dispatcher.BeginInvoke(
                        DispatcherPriority.ContextIdle,
                        new Action(delegate ()
                        {
                            passwordBox1.Focus();
                        }));
            }
        }

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            button.FontWeight = FontWeights.Bold;
            button.FontSize = 16;
         

        }

        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            button.FontWeight = FontWeights.Normal;
            button.FontSize = 12;
        }
    }
}
