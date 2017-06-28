using SqlProject;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows;

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
            Refresh();
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
                    bool ok = (bool)engine.execCommand("delete from traveler where personId = &id", inParams);
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
                Refresh();
            }
        }
    }
}
