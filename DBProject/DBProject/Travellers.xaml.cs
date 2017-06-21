using SqlProject;
using System;
using System.Collections.Generic;
using System.Data;
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
            Refresh();
        }

        private void Refresh()
        {
            dataGrid.ItemsSource = engine.execSelectCommand("select * from traveler natural join person").DefaultView;
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            new AddTraveller().ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                OracleParameter[] inParams = {
                    engine.createParamater("id", OracleType.Number,((DataRowView)dataGrid.SelectedItem).Row.ItemArray[0].ToString())
                };
                try
                {
                    bool ok = (bool)engine.execCommand("delete from driver where personId = &id", inParams);
                    if (ok)
                    {
                        Refresh();
                        MessageBox.Show("Success");
                    }
                    else
                        MessageBox.Show("Invalid Query");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                new UpdateTraveller(((DataRowView)dataGrid.SelectedItem).Row.ItemArray).ShowDialog();
            }
        }
    }
}
