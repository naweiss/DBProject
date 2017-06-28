using SqlProject;
using System;
using System.Data.OracleClient;
using System.Windows;

namespace DBProject
{
    /// <summary>
    /// Interaction logic for UpdateDriver.xaml
    /// </summary>
    public partial class UpdateDriver : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        private string oId;

        public UpdateDriver(object[] itemArray)
        {
            InitializeComponent();
            oId= idTxb.Text = itemArray[0].ToString();
            nameTxb.Text = itemArray[3].ToString();
            salaryTxb.Text = itemArray[1].ToString();
            datePic.SelectedDate = Convert.ToDateTime(itemArray[2].ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("oldId", OracleType.Number,oId),
                engine.createParamater("id", OracleType.Number,idTxb.Text),
                engine.createParamater("Aname",OracleType.NVarChar,nameTxb.Text),
                engine.createParamater("Asalary",OracleType.Number,salaryTxb.Text),
                engine.createParamater("AstartWorking",OracleType.NVarChar,datePic.SelectedDate.Value.ToString("dd/MM/yyyy"))
            };
            try
            {
                bool ok = (bool)engine.execStoredProcedure("updateDriver", inParams);
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
