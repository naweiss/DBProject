using SqlProject;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows;

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
            Refresh();
        }

        private void Refresh()
        {
            dataGrid.ItemsSource = engine.execSelectCommand("select * from driver natural join person").DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            new AddDriver().ShowDialog();
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
                new UpdateDriver(((DataRowView)dataGrid.SelectedItem).Row.ItemArray).ShowDialog();
                Refresh();
            }
        }
    }
}
