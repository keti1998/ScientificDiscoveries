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
        List<Discovery> _discoveries = new List<Discovery>();


        int date;
        string _nobelYes;

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(textBoxyear.Text, out date);

            if (nobelYes.IsChecked == true)
            {
                _nobelYes = "yes";
            }
            else
            {
                _nobelYes = "no";
            }

            var discovery = new Discovery(textBoxname.Text, textBoxfio.Text, textBoxcountry.Text, textBoxsphere.Text, date, _nobelYes);

            _discoveries.Add(discovery);
            textBoxcountry.Clear();
            textBoxfio.Clear();
            textBoxname.Clear();
            textBoxsphere.Clear();
            textBoxyear.Clear();
            nobelNo.IsChecked = false;
            nobelYes.IsChecked = false;
            dataGrid.ItemsSource = _discoveries;
            if (string.IsNullOrWhiteSpace(textBoxcountry.Text))
            {
                MessageBox.Show("Необходимо ввести страну");
                textBoxcountry.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxfio.Text))
            {
                MessageBox.Show("Необходимо ввести ФИО ученого ");
                textBoxfio.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxname.Text))
            {
                MessageBox.Show("Необходимо ввести название");
                textBoxname.Focus();
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
        }

       

        private void textBoxyear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
