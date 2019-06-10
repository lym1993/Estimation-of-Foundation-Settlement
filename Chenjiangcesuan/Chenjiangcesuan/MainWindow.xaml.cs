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
            ObservableCollection<Soilinformation>(); //实例化动态数据集合soilinformationsItems

        ObservableCollection<Soilinformation> calsoilinformationsItems= new
            ObservableCollection<Soilinformation>();//实例化中间组，将soil复制到cal中后对cal进行更改
        
        ObservableCollection<Soilcalculate> soilcalculateresult = new
            ObservableCollection<Soilcalculate>(); //实例化计算结果1集合，生成新动态集合 soilcauculateresult

        ObservableCollection<Soilcalculatefinal> soilcalculatefinals = new
            ObservableCollection<Soilcalculatefinal>(); //实例化计算结果2集合，生成新动态集合 soilcalculatefinals

        //主函数
        public MainWindow()
        {
            InitializeComponent();
            //初始化界面
            foundation_width.Text = Convert.ToString(4);
            foundation_length.Text = Convert.ToString(4);
            for (int i = 0; i <= 4; i++)
            {
                AddRow();
            }
            GetComboBoxSource();
            datagrid1.ItemsSource = soilinformationsItems;
        }

        //添加行方法
        private void AddRow()
        {
            Soilinformation soilinformation = new Soilinformation();

            soilinformation.SoilThickness = "";
            soilinformation.SoilUnitWeight = "";
            soilinformation.Voidratio0kPa = "";
            soilinformation.Voidratio50kPa = "";
            soilinformation.Voidratio100kPa = "";
            soilinformation.Voidratio200kPa = "";
            soilinformation.Voidratio300kPa = "";
            soilinformation.Voidratio400kPa = "";

            soilinformationsItems.Add(soilinformation);

        }

        //计算按钮事件
        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            calsoilinformationsItems = soilinformationsItems;//等下需要进行分层的动态集合（防止更改soilinformation导致前台窗口变化）

            double m = FoundationLength / FoundationWidth;  // m=l/b确定之后一直是常量
            int index = get_baseindex();//基础所在土层
            double distance = get_distance();//基底距离下一土层厚度
            int index_water = get_waterindex();//地下水所在土层
            double distance_water = get_waterdistance();//地下水距离下一层土的厚度
            List<double> list_layer = get_soillayerlist();  //用户输入的土层信息表
            double aasb = get_aasb();   //基底处平均附加应力 aasb
            

            //if (distance==0&&UndergroudWater==0)//flag为0，基底恰好位于土层分界点，且无地下水情况

            //根据埋深Depth重新划分土层
            //基础上部土层
            Soilinformation upbase_soilinformation = new Soilinformation();
            upbase_soilinformation.SoilThickness=Convert.ToString(Convert.ToDouble(soilinformationsItems[index].SoilThickness)-distance);
            upbase_soilinformation.SoilUnitWeight= Convert.ToString(Convert.ToDouble(soilinformationsItems[index].SoilUnitWeight));
            upbase_soilinformation.Haswater = 0;
            upbase_soilinformation.Voidratio0kPa= Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio0kPa));
            upbase_soilinformation.Voidratio50kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio50kPa));
            upbase_soilinformation.Voidratio100kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio100kPa));
            upbase_soilinformation.Voidratio200kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio200kPa));
            upbase_soilinformation.Voidratio300kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio300kPa));
            upbase_soilinformation.Voidratio400kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio400kPa));
            //将上半层插入index处，被分解土层移到index+1位置
            calsoilinformationsItems.Insert(index,upbase_soilinformation);

            //基础下部土层（为0时基底位于土层处）
            Soilinformation downbase_soilinformation = new Soilinformation();
            downbase_soilinformation.SoilThickness = Convert.ToString(distance);
            downbase_soilinformation.SoilUnitWeight= Convert.ToString(Convert.ToDouble(soilinformationsItems[index].SoilUnitWeight));
            downbase_soilinformation.Haswater = 0;
            downbase_soilinformation.Voidratio0kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio0kPa));
            downbase_soilinformation.Voidratio50kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio50kPa));
            downbase_soilinformation.Voidratio100kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio100kPa));
            downbase_soilinformation.Voidratio200kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio200kPa));
            downbase_soilinformation.Voidratio300kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio300kPa));
            downbase_soilinformation.Voidratio400kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio400kPa));
            //将下半层插入index+1处，被分解土层移动到index+2位置
            calsoilinformationsItems.Insert(index+1, downbase_soilinformation);
            //移除被分解的土层
            calsoilinformationsItems.RemoveAt(index+2);



            List<double> list_z = new List<double>();//新建列表，此列表接收距离基底深度z的数值

            for (int i = 0; i < list_z.Count; i++)
            {
                Soilcalculate soilcalculate = new Soilcalculate();

                soilcalculate.Distancefrombase = list_z[i];
                soilcalculate.Length_Width = m;
                soilcalculate.DFB_Width = list_z[i] / (FoundationWidth / 2);
                soilcalculate.Average_Additional_Stress_Coefficient = 4 * get_acas(m, soilcalculate.DFB_Width);
                soilcalculate.DFB_Average_Additional_Stress_Coefficient = list_z[i] * soilcalculate.Average_Additional_Stress_Coefficient;
                if (i == 0)
                {
                    soilcalculate.DFB_Average_Additional_Stress_Coefficient_1 = 0;
                }
                else
                {
                    soilcalculate.DFB_Average_Additional_Stress_Coefficient_1 = soilcalculate.DFB_Average_Additional_Stress_Coefficient
                        - soilcalculateresult[i - 1].DFB_Average_Additional_Stress_Coefficient;  //zα与z-1α-1差值
                }
                soilcalculateresult.Add(soilcalculate);//新的计算层加载到soilcaculate实例            
            }

            //计算框2 
            for (int i = 0; i < list_z.Count - 1; i++)
            {

            }

        }

        //添加土层按钮
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRow();
        }

        //删除土层按钮
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid1.SelectedItems != null & datagrid1.SelectedItems.Count > 0)
            {
                for (int i = datagrid1.SelectedItems.Count - 1; i >= 0; i--)
                {
                    soilinformationsItems.Remove(datagrid1.SelectedItems[i] as Soilinformation);
                }
            }
        }

        //添加combobox源
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
            if (e.Key == Key.Enter)
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
            load.Text = "";
            depth.Text = "";
            undergroudwater.Text = "";
            foundation_width.Text = "";
            foundation_length.Text = "";

            for (int i = 0; i <= 4; i++)
            {
                AddRow();
            }
        }

        //基底处平均附加应力 Average Additional Stress of Base
        private double get_aasb()
        {
            double result;
            //基础及其回填土土总重量
            double temp1 = 20 * FoundationWidth * FoundationLength * (Depth - UndergroudWater)  //地下水位以上重度20
                + 10 * FoundationWidth * FoundationLength * UndergroudWater;//地下水位以下重度10

            //基底平均压力
            double temp2 = (temp1 + Load) / (FoundationWidth * FoundationLength);

            //基底处土的自重应力
            double temp3 = 0;
            if (UndergroudWater == 0 || UndergroudWater >= Depth)//无地下水或地下水深度大于基础埋深
            {
                int i = 0; //土层索引
                if (Depth <= Convert.ToDouble(soilinformationsItems[0].SoilThickness)) //基底在第一层土
                {
                    temp3 = Depth * Convert.ToDouble(soilinformationsItems[0].SoilUnitWeight);
                }
                else  //基底在第i层土
                {
                    while (Depth >= Convert.ToDouble(soilinformationsItems[i].SoilThickness))
                    {
                        Depth -= Convert.ToDouble(soilinformationsItems[i].SoilThickness);
                        i++;
                    }
                    double sum = 0;
                    for (int a = 0; a < i - 1; a++)
                    {
                        sum += Convert.ToDouble(soilinformationsItems[a].SoilThickness) * Convert.ToDouble(soilinformationsItems[a].SoilUnitWeight);
                    }
                    temp3 = sum + Depth * Convert.ToDouble(soilinformationsItems[i - 1].SoilUnitWeight);//前几层的和加上最后一段可变的厚度
                }
            }
            else//有地下水并且地下水深度在基础土层之间
            {
                //do something
            }
            result = temp2 - temp3;
            return result;
        }

        //附加应力平均系数 Average coefficient of additional stress
        //第一个参数是m=l/b；第二个参数是n=z/b；
        private double get_acas(double m, double n)
        {
            double result;
            if (n == 0)
            {
                //在基底处时平均应力系数为0
                result = 0.25;
            }
            else
            {
                //附加应力平均系数公式
                double temp1 = Math.Pow(1 + m * m + n * n, 0.5);
                double temp2 = Math.Pow(1 + m * m, 0.5);
                result = 1 / (2 * Math.PI) * (Math.Atan(m / (n * temp1))
                    + m / n * Math.Log((temp1 - 1) * (temp2 + 1) / ((temp1 + 1) * (temp2 - 1)), Math.E)
                    + 1 / n * Math.Log((temp1 - m) * (temp2 + m) / ((temp1 + m) * (temp2 - m)), Math.E));
            }
            return result;
        }

        //得到基础在土层中位置的索引
        private int get_baseindex()
        {
            List<double> list = get_soillayerlist();
            double distance = 0;
            int a = 0;
            for (int i = 0; i < soilinformationsItems.Count; i++)
            {
                distance += list[i];
                if (Depth <= distance)
                {
                    a = i;
                }
            }
            return a;
        }

        //得到基底距离下一层土的距离       
        private double get_distance()
        {
            List<double> list = get_soillayerlist();
            double distance = 0;
            double a = 0;
            for (int i = 0; i < soilinformationsItems.Count; i++)
            {
                distance += list[i];
                if (Depth <= distance)
                {
                    a = distance - Depth;
                }
            }
            return a;
        }

        //得到用户输入的土层列表
        private List<double> get_soillayerlist()
        {
            List<double> list = new List<double>();
            for(int i=0;i<soilinformationsItems.Count;i++)
            {
                list.Add(Convert.ToDouble(soilinformationsItems[i].SoilThickness));
            }
            return list;
        }

        //地下水位在土层位置索引
        private int get_waterindex()
        {
            int a = 0;
            return a;
        }

        //地下水位距离土层的距离
        private double get_waterdistance()
        {
            double distance = 0;
            return distance;
        }

    }
}
