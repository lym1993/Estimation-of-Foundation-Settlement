using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chenjiangcesuan
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Soilinformation> soilinformationsItems = new
            ObservableCollection<Soilinformation>(); //实例化动态数据集合

        public MainWindow()
        {
            InitializeComponent();
            
            GetComboBoxSource();
            datagrid1.ItemsSource = soilinformationsItems;
        }

        public void AddRow()
        {
            Soilinformation soilinformation = new Soilinformation();

            soilinformation.SoilThickness = 0;
            soilinformation.SoilUnitWeight = 0;
            soilinformation.Voidratio0kPa = 0;
            soilinformation.Voidratio50kPa = 0;
            soilinformation.Voidratio100kPa = 0;
            soilinformation.Voidratio200kPa = 0;
            soilinformation.Voidratio300kPa = 0;
            soilinformation.Voidratio400kPa = 0;

            soilinformationsItems.Add(soilinformation);
        }
        //添加土层按钮
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRow();
        }

        //删除土层按钮
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if(datagrid1.SelectedItems!=null&datagrid1.SelectedItems.Count>0)
            {
                for(int i=datagrid1.SelectedItems.Count-1;i>=0; i--)
                {
                    soilinformationsItems.Remove(datagrid1.SelectedItems[i] as Soilinformation);
                }
            }
        }
       
        private void GetComboBoxSource()
        {
            cbbSelectMode.Items.Add(DataGridSelectionUnit.Cell);
            cbbSelectMode.Items.Add(DataGridSelectionUnit.FullRow);
            cbbSelectMode.Items.Add(DataGridSelectionUnit.CellOrRowHeader);
            cbbSelectMode.SelectedIndex = 2;
        }

        //自动添加行号
        private void Datagrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        //Enter达到tab效果
        private void Datagrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key==Key.Enter)
            {
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }
            
        //荷载
        private double Load
        {
            get { return double.Parse(load.Text); }
            set { Load = value; }
        }
        //基础埋深
        private double Depth
        {
            get { return double.Parse(depth.Text); }
            set { Load = value; }
        }
        //地下水位
        private double UndergroudWater
        {
            get { return double.Parse(undergroudwater.Text); }
            set { Load = value; }
        }
        //基础横向宽度
        private double FoundationWidth
        {
            get { return double.Parse(foundation_width.Text); }
            set { Load = value; }
        }
        //基础竖向宽度
        private double FoundationLength
        {
            get { return double.Parse(foundation_length.Text); }
            set { Load = value; }
        }

        
        //combox改变选择事件
        private void CbbSelectMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datagrid1.SelectionUnit = (DataGridSelectionUnit)cbbSelectMode.SelectedValue;
        }
       
        //clear按钮事件
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
    }
}
