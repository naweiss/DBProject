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
    /// Interaction logic for UpdateTraveller.xaml
    /// </summary>
    public partial class UpdateTraveller : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        private String oId;

        public UpdateTraveller(object[] itemArray)
        {
            InitializeComponent();
            oId = idTxb.Text = itemArray[0].ToString();
            nameTxb.Text = itemArray[2].ToString();
            cbxTypes.ItemsSource = engine.execSelectCommand("select type from travelertype").DefaultView;
            cbxTypes.Text = itemArray[1].ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("oldId", OracleType.Number,oId),
                engine.createParamater("id", OracleType.Number,idTxb.Text),
                engine.createParamater("name",OracleType.NVarChar,nameTxb.Text),
                engine.createParamater("type",OracleType.NVarChar,cbxTypes.Text)
            };
            try
            {
                bool ok = (bool)engine.execStoredProcedure("updateTraveler", inParams);
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
