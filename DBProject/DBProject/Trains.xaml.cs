﻿using SqlProject;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Trains.xaml
    /// </summary>
    public partial class Trains : Window
    {
        private OracleEngine engine = OracleEngine.getInstance();

        public Trains()
        {
            InitializeComponent();
            dataGrid.ItemsSource = engine.execSelectCommand("select * from train").DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            new AddTrain().ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
