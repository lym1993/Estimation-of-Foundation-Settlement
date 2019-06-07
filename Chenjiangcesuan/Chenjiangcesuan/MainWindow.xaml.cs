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
        

        ObservableCollection<Soilinformation> soilinformationsItems = new
            ObservableCollection<Soilinformation>(); //实例化动态数据集合

        //主函数
        public MainWindow()
        {
            InitializeComponent();

            foundation_width.Text = Convert.ToString(4);
            foundation_length.Text = Convert.ToString(4);

            for(int i=0;i<=4;i++)
            {
                AddRow();
            }

            GetComboBoxSource();
            datagrid1.ItemsSource = soilinformationsItems;
        }

        //添加行方法
        public void AddRow()
        {
            Soilinformation soilinformation = new Soilinformation();

            soilinformation.SoilThickness ="";
            soilinformation.SoilUnitWeight ="";
            soilinformation.Voidratio0kPa = "";
            soilinformation.Voidratio50kPa = "";
            soilinformation.Voidratio100kPa = "";
            soilinformation.Voidratio200kPa = "";
            soilinformation.Voidratio300kPa = "";
            soilinformation.Voidratio400kPa = "";

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
                   
        //combox改变选择事件
        private void CbbSelectMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datagrid1.SelectionUnit = (DataGridSelectionUnit)cbbSelectMode.SelectedValue;
        }
       
        //clear按钮事件
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            soilinformationsItems.Clear();
            load.Text ="" ;
            depth.Text = "";
            undergroudwater.Text = "";
            foundation_width.Text = "";
            foundation_length.Text = "";

            for (int i=0; i <= 4; i++)
            {
                AddRow();
            }
        }

        //计算按钮事件
        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            //用户输入完成后，开始计算沉降            
            //  n=z/b(变量)
            //  m=l/b 
            double m = double.Parse(foundation_length.Text) / double.Parse(foundation_width.Text) / 4;
        }

        //基底平均附加应力Average Additional Stress of Base
        private double get_aasb(double a)
        {
            double result;

            //判断埋深是否超过第一层土层厚度          
            if (Depth <= Convert.ToDouble(soilinformationsItems[0].SoilThickness))
            {
                //小于第一层土厚度，不用划分土层，直接计算
                result = (20 * FoundationLength * FoundationWidth + Load) / (FoundationLength * FoundationWidth) - Convert.ToDouble(soilinformationsItems[0].SoilUnitWeight);
            }
            //埋深超过第一层土
            else
            {
                for (int i = soilinformationsItems.Count; i > 1; i--)
                {
                    result = 0;
                }
            }
            return result;
        }

        //附加应力平均系数average coefficient of additional stress
        //第一个参数是m=l/b；第二个参数是n=z/b；
        private double get_acas(double m,double n)
        {
            double result;
            double temp1 = Math.Pow(1 + m * m + n * n, 0.5);
            double temp2 = Math.Pow(1 + m * m, 0.5);
            result = 1 / (2 * Math.PI) * (Math.Atan(m/(n*temp1))
                +m/n*Math.Log((temp1-1)*(temp2+1)/((temp1+1)*(temp2-1)),Math.E)
                +1/n*Math.Log((temp1-m)*(temp2+m)/((temp1+m)*(temp2-m)),Math.E));
            return result;           
        }      
    }
}
