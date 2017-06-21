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
                engine.createParamater("name",OracleType.NVarChar,nameTxb.Text),
                engine.createParamater("salary",OracleType.Number,salaryTxb.Text),
                engine.createParamater("startWorking",OracleType.NVarChar,datePic.SelectedDate.Value.ToString("dd/MM/yyyy"))
            };
            engine.execStoredProcedure("insertDriver",inParams);
        }
    }
}
