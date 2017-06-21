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
    /// Interaction logic for Travellers.xaml
    /// </summary>
    public partial class Travellers : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();

        public Travellers()
        {
            InitializeComponent();
            dataGrid.ItemsSource = engine.execSelectCommand("select * from traveler natural join person").DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            new AddTravellers().ShowDialog();
        }
    }
}
