using SqlProject;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
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

namespace DBProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new Drivers().Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            new Trains().Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            new Travellers().Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            new PersonTravels().Show();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            new GetDst().ShowDialog();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            new LineInfo().ShowDialog();
        }

        private void button7_Click_1(object sender, RoutedEventArgs e)
        {
            new GetUsers().ShowDialog();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter outParam = engine.createParamater("Result", OracleType.Number, null, System.Data.ParameterDirection.ReturnValue);
            try
            {
                MessageBox.Show(engine.execStoredProcedure("TheMostProfitableLine", null, outParam).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
