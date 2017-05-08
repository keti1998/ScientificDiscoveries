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
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            NewDiscoveryWindow newD = new NewDiscoveryWindow();
            newD.Show();
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();
        }

        private void button_Show_Click(object sender, RoutedEventArgs e)
        {
            string[] line = File.ReadAllLines("D:/KDZ/KdzScientificDiscoveries/KdzScientificDiscoveries/list.txt", Encoding.GetEncoding(1251));
            for (int i = 0; i < line.Length; i++)
            {
                string[] items = line[i].Split(',');
                Discovery example = new Discovery(items[0], items[1], items[2], items[3], int.Parse(items[4]), items[5]);
                _discoveriesShow.Add(example);
            }
            dataGrid.ItemsSource = _discoveriesShow;
        }
    }
}
