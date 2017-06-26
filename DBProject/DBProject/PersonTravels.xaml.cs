﻿using SqlProject;
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
