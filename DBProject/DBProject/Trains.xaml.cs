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
    /// Interaction logic for Trains.xaml
    /// </summary>
    public partial class Trains : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();

        public Trains()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            dataGrid.ItemsSource = engine.execSelectCommand("select * from train").DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("id", OracleType.Number,"1111")};
            try
            {
                bool ok = (bool)engine.execStoredProcedure("insertTrain", inParams);
                if (ok)
                    MessageBox.Show("Success");
                else
                    MessageBox.Show("Invalid Query");
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
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
                    bool ok = (bool)engine.execCommand("delete from train where trainId = &id", inParams);
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
                new UpdateTrain(((DataRowView)dataGrid.SelectedItem).Row.ItemArray).ShowDialog();
                Refresh();
            }
        }
    }
}
