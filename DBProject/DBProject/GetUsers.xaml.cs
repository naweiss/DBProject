using SqlProject;
using System;
using System.Data.OracleClient;
using System.Windows;

namespace DBProject
{
    /// <summary>
    /// Interaction logic for GetUsers.xaml
    /// </summary>
    public partial class GetUsers : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        public GetUsers()
        {
            InitializeComponent();
            cbxTypes.ItemsSource = engine.execSelectCommand("select type from travelertype").DefaultView;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("tmp", OracleType.NVarChar,cbxTypes.Text)
            };
            try
            {
                dataGrid.ItemsSource = engine.execSelectCommand("select round(type1/travelers*100,2) || '%' as percent from(select count(*) as type1 from Traveler natural join TravelerType group by type having type = &tmp), (select count(*) as travelers from Traveler) ", inParams).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
