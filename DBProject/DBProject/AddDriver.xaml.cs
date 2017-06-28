using SqlProject;
using System;
using System.Data.OracleClient;
using System.Windows;

namespace DBProject
{
    /// <summary>
    /// Interaction logic for AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();

        public AddDriver()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("id", OracleType.Number,idTxb.Text),
                engine.createParamater("Aname",OracleType.NVarChar,nameTxb.Text),
                engine.createParamater("salary",OracleType.Number,salaryTxb.Text),
                engine.createParamater("startWorking",OracleType.NVarChar,datePic.SelectedDate.Value.ToString("dd/MM/yyyy"))
            };
            try
            {
                bool ok = (bool)engine.execStoredProcedure("insertDriver", inParams);
                if (ok)
                {
                    MessageBox.Show("Success");
                    this.Close();
                }
                else
                    MessageBox.Show("Invalid Query");
            } catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
