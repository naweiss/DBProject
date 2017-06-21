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
using System.Windows.Shapes;

namespace DBProject
{
    /// <summary>
    /// Interaction logic for UpdateDriver.xaml
    /// </summary>
    public partial class UpdateDriver : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        private String oId;

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
                engine.createParamater("name",OracleType.NVarChar,nameTxb.Text),
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
