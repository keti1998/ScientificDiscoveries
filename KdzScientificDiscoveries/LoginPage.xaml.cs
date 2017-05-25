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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<Person> _person;

        public LoginPage()
        {
            InitializeComponent();
            _person = new List<Person>();
            textBoxLogin.Focus();
        }

        
        private void button_signIn_Click(object sender, RoutedEventArgs e)
        {

            
            LoadPersonData();
            List<Person> person = new List<Person>();
            if (textBoxLogin.Text == "")

            {
                MessageBox.Show("Введите логин");
                return;
            }


            if (passwordBox.Password.ToString() == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            for (int i = 0; i < _person.Count; i++)
            {
                if (textBoxLogin.Text == _person[i].Login && passwordBox.Password.ToString() == _person[i].Password)
                {
                    DiscoveriesPage disc = new DiscoveriesPage();
                    NavigationService.Navigate(disc);
                    person.Add(_person[i]);
                    Logger.Log("Совершена авторизация зарегистрированного пользователя, осуществлен переход на страницу со списком открытий.");
                    textBoxLogin.Clear();
                    break;
                    


                }
            }
                

            if (person.Count == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
              
            }
            
            passwordBox.Clear();
          
        }

        private void LoadPersonData()
        {
             
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("../../persons.dat", FileMode.OpenOrCreate))
                {
                    try
                    {
                        _person= (List<Person>)formatter.Deserialize(fs);

                    }
                    catch
                    {
                        _person = new List<Person>();
                    }
                }

            }
            catch
            {
                MessageBox.Show("Ошибка чтения из файла");
            }
           
        }

      

        

       

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)
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

        private void button_signUp_Click(object sender, RoutedEventArgs e)
        {
            textBoxLogin.Clear();
            NavigationService.Navigate(Pages.SignUp);
            Logger.Log("Совершен переход на страницу регистрации.");
        }

        private void button_signIn_MouseEnter(object sender, MouseEventArgs e)
        {
            button_signIn.FontWeight = FontWeights.Bold;
            button_signIn.FontSize = 16;
           

        }

        private void button_signIn_MouseLeave(object sender, MouseEventArgs e)
        {
            button_signIn.FontWeight = FontWeights.Normal;
            button_signIn.FontSize = 12;
           

        }

        private void button_signUp_MouseEnter(object sender, MouseEventArgs e)
        {
            button_signUp.FontWeight = FontWeights.Bold;
            button_signUp.FontSize = 16;
           
        }

        private void button_signUp_MouseLeave(object sender, MouseEventArgs e)
        {
            button_signUp.FontWeight = FontWeights.Normal;
            button_signUp.FontSize = 12;
        }
    }
}
