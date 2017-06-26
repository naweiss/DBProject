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
    /// Interaction logic for GetDst.xaml
    /// </summary>
    public partial class GetDst : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();
        public GetDst()
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
                dataGrid.ItemsSource = engine.execSelectCommand("select * from(select linenumber, StationName as Source from Line natural join Station ) join (select s.stationname as Destination,l.linenumber from Line l, Station s where l.stationid1 = s.stationid) using(lineNumber) where lineNumber=&tmp", inParams).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
