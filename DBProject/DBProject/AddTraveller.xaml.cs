using SqlProject;
using System;
using System.Data.OracleClient;
using System.Windows;

namespace DBProject
{
    /// <summary>
    /// Interaction logic for AddTravellers.xaml
    /// </summary>
    public partial class AddTraveller : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();

        public AddTraveller()
        {
            InitializeComponent();
            cbxTypes.ItemsSource = engine.execSelectCommand("select type from travelertype").DefaultView;
            cbxTypes.SelectedIndex = 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("id", OracleType.Number,idTxb.Text),
                engine.createParamater("Aname",OracleType.NVarChar,nameTxb.Text),
                engine.createParamater("Atype",OracleType.NVarChar,cbxTypes.Text)
            };
            try
            {
                bool ok = (bool)engine.execStoredProcedure("insertTraveler", inParams);
                if (ok)
                {
                    MessageBox.Show("Success");
                    Close();
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
}
