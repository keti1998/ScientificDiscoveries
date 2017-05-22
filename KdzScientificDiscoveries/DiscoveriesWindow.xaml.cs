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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KdzScientificDiscoveries
{
    
    /// <summary>
    /// Логика взаимодействия для DiscoveriesWindow.xaml
    /// </summary>
    public partial class DiscoveriesWindow : Window
    {
        List<Discovery> _discoveriesShow;
        
        public DiscoveriesWindow()
        {

            InitializeComponent();
           
            LoadData();
           
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            NewDiscoveryWindow newD = new NewDiscoveryWindow();
            newD.Show();
            this.Close();
            LoadData();
            Logger.Log("Совершён переход в новое окно для добавления нового открытия");


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
                result = MessageBox.Show("Вы точно хотите безвозвратно удалить, выбранный вами, объект", "Удаление элемента", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                   // _discoveriesShow.RemoveAt(selindex - 1);
                    foreach (var row in dataGrid.SelectedItems)
                    {
                        Discovery flat = row as Discovery;
                        _discoveriesShow.Remove(flat);
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
            for (int i = 0; i < _discoveriesShow.Count; i++)
            {
                if (_discoveriesShow[i].Name == textBoxName.Text)

                {
                    MessageBox.Show("Название открытия: " + _discoveriesShow[i].Name + "\nФИО учёного: " + _discoveriesShow[i].Scientist + "\nСфера открытия: " + _discoveriesShow[i].Sphere + "\nСтрана открытия: " + _discoveriesShow[i].Country + "\nГод открытия: " + _discoveriesShow[i].Date + "\nНаличие Нобелевской премии: " + _discoveriesShow[i].NobelPrize, "Информация об открытии");
                    // (dataGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.Yellow;
                    Logger.Log("Выполнен поиск");

                }

                else if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    MessageBox.Show("Введите название");
                    return;
                }
            }
            textBoxName.Clear();
        }
        //  metodi
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
            
    }
}
