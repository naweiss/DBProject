using SqlProject;
using System;
using System.Data.OracleClient;
using System.Windows;

namespace DBProject
{
    /// <summary>
    /// Interaction logic for LineInfo.xaml
    /// </summary>
    public partial class LineInfo : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        public LineInfo()
        {
            InitializeComponent();
            cbxTypes.ItemsSource = engine.execSelectCommand("select Linenumber from line").DefaultView;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OracleParameter[] inParams = {
                engine.createParamater("tmp", OracleType.Number,cbxTypes.Text)
            };
            try
            {
                dataGrid.ItemsSource = engine.execSelectCommand("select Linenumber, count(*) as travelers,count(distinct trainid) as trains from travel natural join partitialtravel join ride using (travelId) where Linenumber = &tmp group by Linenumber order by travelers DESC", inParams).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
