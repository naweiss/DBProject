using SqlProject;
using System;
using System.Data.OracleClient;
using System.Windows;

namespace DBProject
{
    /// <summary>
    /// Interaction logic for UpdateTrain.xaml
    /// </summary>
    public partial class UpdateTrain : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        private string oId;

        public UpdateTrain(object[] itemArray)
        {
            InitializeComponent();
            oId = idTxb.Text = itemArray[0].ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("oldId", OracleType.Number,oId),
                engine.createParamater("id", OracleType.Number,idTxb.Text)
            };
            try
            {
                bool ok = (bool)engine.execStoredProcedure("updateTrain", inParams);
                if (ok)
                {
                    MessageBox.Show("Success");
                    oId = idTxb.Text;
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
