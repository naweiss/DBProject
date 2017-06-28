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
                    MessageBox.Show("Success");
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
