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
    /// Interaction logic for PersonTravels.xaml
    /// </summary>
    public partial class PersonTravels : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();

        public PersonTravels()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("id", OracleType.Number,idTxb.Text)
            };
            OracleParameter outParams = engine.createParamater("ans", OracleType.Cursor,null,System.Data.ParameterDirection.Output);
            try
            {
                var x = engine.execCommand("GetAllPassengerTravels", inParams, outParams);
                //if (ok)
                //    MessageBox.Show("Success");
                //else
                //    MessageBox.Show("Invalid Query");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
