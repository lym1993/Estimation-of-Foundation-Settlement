﻿using System;
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

namespace Chenjiangcesuan
{
    /// <summary>
    /// finalresult.xaml 的交互逻辑
    /// </summary>
    public partial class finalresult : Window
    {
        public finalresult()
        {
            InitializeComponent();
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;                   
            this.Close();
        }

        private void Final_datagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
