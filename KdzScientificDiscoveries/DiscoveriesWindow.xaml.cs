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
        List<Discovery> _discoveriesShow = new List<Discovery>();
        public DiscoveriesWindow()
        {
            InitializeComponent();
            _discoveriesShow.Clear();
            string[] line = File.ReadAllLines("../../list.txt", Encoding.GetEncoding(1251));
            for (int i = 0; i < line.Length; i++)
            {
                string[] items = line[i].Split(',');
                Discovery example = new Discovery(items[0], items[1], items[2], items[3], int.Parse(items[4]), items[5]);
                _discoveriesShow.Add(example);
            }
            dataGrid.ItemsSource = _discoveriesShow;

        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            NewDiscoveryWindow newD = new NewDiscoveryWindow();
            newD.Show();
            
            
            
        }

       
        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
           

            var selindex = dataGrid.SelectedIndex + 1;
            if (selindex == 0)

            {
                MessageBox.Show("Вы ничего не выбрали");
            }
            else
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы точно хотите безвозвратно удалить " + _discoveriesShow[selindex - 1].Name + "?", "Удаление элемента", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    _discoveriesShow.RemoveAt(selindex - 1);

                    dataGrid.ItemsSource = null;
                    dataGrid.Columns.Clear();
                    dataGrid.ItemsSource = _discoveriesShow;

                    string[] lines = File.ReadAllLines("../../list.txt", Encoding.GetEncoding(1251));
                    StreamWriter sw = new StreamWriter("../../list.txt", false, Encoding.UTF8);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i != selindex - 1)
                        {
                            sw.WriteLine(lines[i]);
                        }
                    }
                    sw.Close();
                }


            }
        }

        private void button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();
            _discoveriesShow.Clear();
            string[] line = File.ReadAllLines("../../list.txt", Encoding.GetEncoding(1251));
            for (int i = 0; i < line.Length; i++)
            {
                string[] items = line[i].Split(',');
                Discovery example = new Discovery(items[0], items[1], items[2], items[3], int.Parse(items[4]), items[5]);
                _discoveriesShow.Add(example);
            }
            dataGrid.ItemsSource = _discoveriesShow;

        }

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _discoveriesShow.Count; i++)
            {
                if (_discoveriesShow[i].Name == textBoxName.Text)


                    (dataGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.Yellow;



                else if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    MessageBox.Show("Введите название");
                    return;
                }
            }
        }
    }
}
