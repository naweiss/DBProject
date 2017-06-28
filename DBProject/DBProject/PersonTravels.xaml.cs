using SqlProject;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows;

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
                engine.createParamater("Id", OracleType.Number,idTxb.Text)
            };
            OracleParameter outParams = engine.createParamater("o_cursor", OracleType.Cursor,null,System.Data.ParameterDirection.ReturnValue);
            try
            {
                DataTable dataTable = (DataTable)engine.execStoredProcedure("GetAllPassengerTravels", inParams, outParams);
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
