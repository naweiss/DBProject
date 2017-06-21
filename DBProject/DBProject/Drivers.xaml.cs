using SqlProject;
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

namespace DBProject
{
    /// <summary>
    /// Interaction logic for Drivers.xaml
    /// </summary>
    public partial class Drivers : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        public Drivers()
        {
            InitializeComponent();
            dataGrid.ItemsSource = engine.execSelectCommand("select * from driver").DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            new AddDriver().ShowDialog();
        }
    }
}
