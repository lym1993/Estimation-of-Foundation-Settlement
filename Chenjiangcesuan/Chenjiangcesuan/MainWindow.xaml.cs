﻿using System;
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
            load.Text = Convert.ToString(100);
            depth.Text = Convert.ToString(1.5);
            undergroudwater.Text = Convert.ToString(3);

            for (int i = 0; i <2; i++)
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

            soilinformation.SoilThickness = "0";
            soilinformation.SoilUnitWeight = "0";
            soilinformation.Haswater = 0;//默认均不含地下水影响
            soilinformation.Voidratio0kPa = "0";
            soilinformation.Voidratio50kPa = "0";
            soilinformation.Voidratio100kPa = "0";
            soilinformation.Voidratio200kPa = "0";
            soilinformation.Voidratio300kPa = "0";
            soilinformation.Voidratio400kPa = "0";
            
            soilinformationsItems.Add(soilinformation);
        }

        //计算按钮事件
        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {          
            data_processing1st();//处理用户输入数据1st
            data_processing2nd();//处理数据2nd        
        }

        //1st处理用户输入数据
        private void data_processing1st()
        {
            int index = get_baseindex(soilinformationsItems);//基础所在土层
            double distance = get_distance(soilinformationsItems);//基底距离下一土层厚度         
            List<double> list_layer = get_soillayerlist(soilinformationsItems);  //用户输入的土层信息表
           
            //根据埋深Depth重新划分土层
            //基础上部土层
            Soilinformation upbase_soilinformation = new Soilinformation();
            upbase_soilinformation.SoilThickness = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].SoilThickness) - distance);
            upbase_soilinformation.SoilUnitWeight = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].SoilUnitWeight));
            upbase_soilinformation.Haswater = 0;
            upbase_soilinformation.Voidratio0kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio0kPa));
            upbase_soilinformation.Voidratio50kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio50kPa));
            upbase_soilinformation.Voidratio100kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio100kPa));
            upbase_soilinformation.Voidratio200kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio200kPa));
            upbase_soilinformation.Voidratio300kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio300kPa));
            upbase_soilinformation.Voidratio400kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio400kPa));
            soilinformationsItems.Insert(index, upbase_soilinformation);//将上半层插入index处，被分解土层移到index+1位置

            //基础下部土层（为0时基底位于土层处）
            Soilinformation downbase_soilinformation = new Soilinformation();
            downbase_soilinformation.SoilThickness = Convert.ToString(distance);
            downbase_soilinformation.SoilUnitWeight = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].SoilUnitWeight));
            downbase_soilinformation.Haswater = 0;
            downbase_soilinformation.Voidratio0kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio0kPa));
            downbase_soilinformation.Voidratio50kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio50kPa));
            downbase_soilinformation.Voidratio100kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio100kPa));
            downbase_soilinformation.Voidratio200kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio200kPa));
            downbase_soilinformation.Voidratio300kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio300kPa));
            downbase_soilinformation.Voidratio400kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index].Voidratio400kPa));

            soilinformationsItems.Insert(index + 1, downbase_soilinformation);//将下半层插入index+1处，被分解土层移动到index+2位置            
            soilinformationsItems.RemoveAt(index + 2);//移除被分解的土层           
            List<double> list_updownlayer = get_baselayerlist(soilinformationsItems);//对划分的第一个列表提取深度

            int index_water = get_waterindex(soilinformationsItems);//地下水所在土层
            double distance_water = get_waterdistance(soilinformationsItems);//地下水距离下一层土的厚度

            //重新定位与划分含水层
            //含水层上部分haswater属性均设置为0
            for (int i = 0; i < index_water; i++)
            {
                soilinformationsItems[i].Haswater = 0;
            }
            //含水层上部土层
            Soilinformation upwater_soilinformation = new Soilinformation();
            upwater_soilinformation.SoilThickness = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].SoilThickness) - distance_water);
            upwater_soilinformation.SoilUnitWeight = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].SoilUnitWeight));
            upwater_soilinformation.Haswater = 0;
            upwater_soilinformation.Voidratio0kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio0kPa));
            upwater_soilinformation.Voidratio50kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio50kPa));
            upwater_soilinformation.Voidratio100kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio100kPa));
            upwater_soilinformation.Voidratio200kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio200kPa));
            upwater_soilinformation.Voidratio300kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio300kPa));
            upwater_soilinformation.Voidratio400kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio400kPa));
            soilinformationsItems.Insert(index_water, upwater_soilinformation);//上半层插入index_water处，被分解土层移动到index+1位置

            //含水层下部土层
            Soilinformation downwater_soilinformation = new Soilinformation();
            downwater_soilinformation.SoilThickness = Convert.ToString(distance_water);
            downwater_soilinformation.SoilUnitWeight = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].SoilUnitWeight));
            downwater_soilinformation.Haswater = 10;   //地下水位下方土层均为10
            downwater_soilinformation.Voidratio0kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio0kPa));
            downwater_soilinformation.Voidratio50kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio50kPa));
            downwater_soilinformation.Voidratio100kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio100kPa));
            downwater_soilinformation.Voidratio200kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio200kPa));
            downwater_soilinformation.Voidratio300kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio300kPa));
            downwater_soilinformation.Voidratio400kPa = Convert.ToString(Convert.ToDouble(soilinformationsItems[index_water].Voidratio400kPa));
            soilinformationsItems.Insert(index_water + 1, downwater_soilinformation);//下半层插入index_water+1处，被分解土层移动到index+2位置
            soilinformationsItems.RemoveAt(index_water + 2);//移除被分解的含水土层

            //地下水位线以下haswater属性均设置为10
            for (int i = index_water + 1; i < soilinformationsItems.Count; i++)
            {
                soilinformationsItems[i].Haswater = 10;
            }
            //移除厚度为0的土层
            for (int i = 0; i < soilinformationsItems.Count; i++)
            {
                if (Convert.ToDouble(soilinformationsItems[i].SoilThickness) == 0)
                {
                    soilinformationsItems.RemoveAt(i);
                }
            }
        }

        private void data_processing2nd()
        {
            double m = FoundationLength / FoundationWidth;  // m=l/b确定之后一直是常量
            double temp1=get_aasb();//基底平均附加应力，要把这个数传递到外面

            //移除基底以上土层 至此soilinformationsItems不再变动
            for (int i=0;i<=get_baseindex(soilinformationsItems);i++)
            {
                soilinformationsItems.RemoveAt(i);
            }

            //对soilcalculate类赋值并计算 
            for(int i=0;i<=soilinformationsItems.Count;i++)
            {
                if(i==0)//基底处的z为0 z*α差值也为0
                {
                    soilcalculateresult[i].Distancefrombase = 0;
                    soilcalculateresult[i].Length_Width = m;
                    soilcalculateresult[i].DFB_Width = 0;
                    soilcalculateresult[i].Average_Additional_Stress_Coefficient =4*get_acas(m, 0);
                    soilcalculateresult[i].DFB_Average_Additional_Stress_Coefficient = 0;
                    soilcalculateresult[i].DFB_Average_Additional_Stress_Coefficient_1 = 0;                    
                }
                else
                {
                    soilcalculateresult[i].Distancefrombase += Convert.ToDouble(soilinformationsItems[i-1].SoilThickness);
                    soilcalculateresult[i].Length_Width = m;
                    soilcalculateresult[i].DFB_Width = soilcalculateresult[i].Distancefrombase/(FoundationWidth/2);
                    soilcalculateresult[i].Average_Additional_Stress_Coefficient = 4 * get_acas(m, soilcalculateresult[i].DFB_Width);
                    soilcalculateresult[i].DFB_Average_Additional_Stress_Coefficient = 
                        soilcalculateresult[i].Distancefrombase* soilcalculateresult[i].Average_Additional_Stress_Coefficient;
                    soilcalculateresult[i].DFB_Average_Additional_Stress_Coefficient_1 = 
                        soilcalculateresult[i].DFB_Average_Additional_Stress_Coefficient- soilcalculateresult[i-1].DFB_Average_Additional_Stress_Coefficient;                    
                }
                soilcalculateresult.Add(soilcalculateresult[i]);
            }

            //弹出子对话框 显示soilcalculateresult的信息，用户点击ok后进入最终运算。

            //点击ok后
            for(int i=0;i<soilinformationsItems.Count;i++)
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
            double result=0;
            //基础及其回填土土总重量
            double temp1 = 20 * FoundationWidth * FoundationLength * (Depth - UndergroudWater)  //地下水位以上重度20
                + 10 * FoundationWidth * FoundationLength * UndergroudWater;//地下水位以下重度10

            //基底平均压力
            double temp2 = (temp1 + Load) / (FoundationWidth * FoundationLength);

            //基底处土的自重应力
            double temp3=0;            
            for(int i=0;i<=get_baseindex(soilinformationsItems);  i++)
            {
                temp3 += Convert.ToDouble(soilinformationsItems[i].SoilThickness) * (Convert.ToDouble(soilinformationsItems[i].SoilUnitWeight) - soilinformationsItems[i].Haswater);
            }

            result = temp2 - temp3;//基底平均附加应力等于平均压力-自重应力
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

        //基础在土层中位置的索引
        private int get_baseindex(ObservableCollection<Soilinformation>items)
        {
            List<double> list = get_soillayerlist(items);
            double distance = 0;
            int a = 0;
            for (int i = 0; i < items.Count; i++)
            {
                distance += list[i];
                if (Depth <= distance)
                {
                    a = i;
                    break;
                }
            }
            return a;
        }

        //基底距离下一层土的距离       
        private double get_distance(ObservableCollection<Soilinformation> items)
        {
            List<double> list = get_soillayerlist(items);
            double distance = 0;
            double a = 0;
            for (int i = 0; i < items.Count; i++)
            {
                distance += list[i];
                if (Depth <= distance)
                {
                    a = distance - Depth;
                    break;
                }
            }
            return a;
        }

        //用户输入的土层列表
        private List<double> get_soillayerlist(ObservableCollection<Soilinformation> items)
        {
            List<double> list = new List<double>();
            for(int i=0;i<items.Count;i++)
            {
                list.Add(Convert.ToDouble(items[i].SoilThickness));
            }
            return list;
        }

        //依据基础埋深新划分的土层列表
        private List<double> get_baselayerlist(ObservableCollection<Soilinformation> items)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < items.Count; i++)
            {
                list.Add(Convert.ToDouble(items[i].SoilThickness));
            }
            return list;
        }

        //地下水位在土层位置索引
        private int get_waterindex(ObservableCollection<Soilinformation>items)
        {
            List<double> list = get_baselayerlist(items);
            double distance = 0;
            int a = 0;
            for (int i = 0; i < items.Count; i++)
            {
                distance += list[i];
                if (UndergroudWater <= distance)
                {
                    a = i;
                    break;
                }
            }
            return a;
        }

        //地下水位距离土层的距离
        private double get_waterdistance(ObservableCollection<Soilinformation> items)
        {
            List<double> list = get_baselayerlist(items);
            double distance = 0;
            double a = 0;
            for (int i = 0; i < items.Count; i++)
            {
                distance += list[i];
                if (UndergroudWater <= distance)
                {
                    a = distance-UndergroudWater;
                    break;
                }
            }
            return a;
        }
      
    }
}
