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
    /// Логика взаимодействия для DiscoveriesPage.xaml
    /// </summary>
    public partial class DiscoveriesPage : Page
    {
        List<Discovery> _discoveriesShow;

        public DiscoveriesPage()
        {
            InitializeComponent();
            LoadData();

        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            NewDiscoveryPage newd = new NewDiscoveryPage();
            NavigationService.Navigate(newd);
            Logger.Log("Совершён переход в новое окно для добавления нового открытия.");
        }



        private void button_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Вы точно хотите сохранить изменения ?", "Сохранение изменений", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                SaveData();
                Logger.Log("Сохранены изменения сделанные пользователем");
            }
            else
            {
                LoadData();
            }


        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            var selindex = dataGrid.SelectedIndex + 1;
            if (selindex == 0)

            {
                MessageBox.Show("Вы ничего не выбрали", "Удаление элемента");
            }
            else
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы точно хотите безвозвратно удалить, выбранный вами, объект ?", "Удаление элемента", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    // _discoveriesShow.RemoveAt(selindex - 1);
                    foreach (var row in dataGrid.SelectedItems)
                    {
                        Discovery disc = row as Discovery;
                        _discoveriesShow.Remove(disc);
                    }
                    dataGrid.ItemsSource = null;
                    dataGrid.Columns.Clear();
                    dataGrid.ItemsSource = _discoveriesShow;
                    SaveData();
                    Logger.Log("Удалён, выбранный пользователем, объект");


                }


            }
        }

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            LoadData();

            List<Discovery> listsearch = new List<Discovery>();
            for (int i = 0; i < _discoveriesShow.Count; i++)
            {
                if (_discoveriesShow[i].Name.ToLower() == textBoxName.Text.ToLower())

                {
                    listsearch.Add(_discoveriesShow[i]);
                    dataGrid.UpdateLayout();
                    dataGrid.ScrollIntoView(dataGrid.Items[i]);
                    var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                    MessageBox.Show("Название открытия: " + _discoveriesShow[i].Name + "\nФИО учёного: " + _discoveriesShow[i].Scientist + "\nСфера открытия: " + _discoveriesShow[i].Sphere + "\nСтрана открытия: " + _discoveriesShow[i].Country + "\nГод открытия: " + _discoveriesShow[i].Date + "\nНаличие Нобелевской премии: " + _discoveriesShow[i].NobelPrize, "Информация об открытии");
                    row.Background = Brushes.Yellow;
                    Logger.Log("Выполнен поиск.");
                    row.Focus();
                }

                else if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    MessageBox.Show("Введите название");
                    return;
                }
                continue;

            }
            if (listsearch.Count == 0)
            {
                MessageBox.Show("Такого открытия в данном списке не существует ");
                return;
            }
            textBoxName.Clear();
        }



        //metodi
        private void LoadData()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("../../discoveries.dat", FileMode.OpenOrCreate))
                {
                    try
                    {
                        _discoveriesShow = (List<Discovery>)formatter.Deserialize(fs);

                    }
                    catch
                    {
                        _discoveriesShow = new List<Discovery>();
                    }
                }

            }
            catch
            {
                Logger.Log("Ошибка чтения из файла.");
                MessageBox.Show("Ошибка чтения из файла");
            }
            dataGrid.ItemsSource = _discoveriesShow;
        }

        private void SaveData()
        {
            using (FileStream filest = new FileStream("../../discoveries.dat", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(filest, _discoveriesShow);
            }
        }
        // for buttons
        private void button_Add_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Add.FontWeight = FontWeights.Bold;
            button_Add.FontSize = 20;
        }

        private void button_Add_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Add.FontWeight = FontWeights.Normal;
            button_Add.FontSize = 12;


        }

        private void button_Delete_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Delete.FontWeight = FontWeights.Bold;
            button_Delete.FontSize = 20;
        }

        private void button_Delete_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Delete.FontWeight = FontWeights.Normal;
            button_Delete.FontSize = 12;
        }

        private void button_SaveChanges_MouseEnter(object sender, MouseEventArgs e)
        {
            button_SaveChanges.FontWeight = FontWeights.Bold;
            button_SaveChanges.FontSize = 13;
        }

        private void button_SaveChanges_MouseLeave(object sender, MouseEventArgs e)
        {
            button_SaveChanges.FontWeight = FontWeights.Normal;
            button_SaveChanges.FontSize = 12;
          
        }

        private void button_Search_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Search.FontWeight = FontWeights.Bold;
            button_Search.FontSize = 16;
        }

        private void button_Search_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Search.FontWeight = FontWeights.Normal;
            button_Search.FontSize = 12;
        }

        private void button_Back_MouseEnter(object sender, MouseEventArgs e)
        {
            button_Back.FontWeight = FontWeights.Bold;
            button_Back.FontSize = 16;
        }

        private void button_Back_MouseLeave(object sender, MouseEventArgs e)
        {
            button_Back.FontWeight = FontWeights.Normal;
            button_Back.FontSize = 12;
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Вы точно хотите выйти из учётной записи?", "Выход из учётной записи", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                NavigationService.Navigate(Pages.LoginPage);
                Logger.Log("Пользователь вышел из учётной записи.");
            }
            else
            {
                return;
            }
        }
    }

}
