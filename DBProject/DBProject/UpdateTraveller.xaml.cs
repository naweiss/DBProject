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

        public UpdateTraveller(object[] itemArray)
        {
            InitializeComponent();
            idTxb.Text = itemArray[0].ToString();
            nameTxb.Text = itemArray[2].ToString();
            cbxTypes.ItemsSource = engine.execSelectCommand("select type from travelertype").DefaultView;
            cbxTypes.SelectedItem = itemArray[1].ToString(); // לבדוק שעובד
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("id", OracleType.Number,idTxb.Text),
                engine.createParamater("name",OracleType.NVarChar,nameTxb.Text),
                engine.createParamater("type",OracleType.Number,cbxTypes.Text)
            };
            try
            {
                bool ok = (bool)engine.execStoredProcedure("updateTraveller", inParams);
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
