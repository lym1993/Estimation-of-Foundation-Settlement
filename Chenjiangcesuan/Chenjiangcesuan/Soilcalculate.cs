using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenjiangcesuan
{
    //此类用于展示中间过程计算结果
    class Soilcalculate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //距离基底距离Distance from base
        public double Distancefrombase
        {
            get { return _distancefrombase; }
            set { _distancefrombase = value; NotifyPropertyChanged("Distancefrombase"); }
        }
        private double _distancefrombase;

        //基础 l/b
        public double Length_Width
        {
            get { return _length_width; }
            set { _length_width = value; NotifyPropertyChanged("Length_Width"); }
        }
        private double _length_width;

        // 基底深度z/b
        public double DFB_Width
        {
            get { return _dfb_width; }
            set { _dfb_width = value; NotifyPropertyChanged("DFB_Width"); }
        }
        private double _dfb_width;

        // 平均α
        public double Average_Additional_Stress_Coefficient
        {
            get { return _average_additional_stress_coefficient; }
            set { _average_additional_stress_coefficient = value; NotifyPropertyChanged("Average_Additional_Stress_Coefficient"); }
        }
        private double _average_additional_stress_coefficient;

        // z*平均α
        public double DFB_Average_Additional_Stress_Coefficient
        {
            get { return _dfb_average_additional_stress_coefficient; }
            set { _dfb_average_additional_stress_coefficient = value; NotifyPropertyChanged("DFB_Average_Additional_Stress_Coefficient"); }
        }
        private double _dfb_average_additional_stress_coefficient;

        //z*平均α-z-1平均α-1
        public double DFB_Average_Additional_Stress_Coefficient_1
        {
            get { return _dfb_average_additional_stress_coefficient; }
            set { _dfb_average_additional_stress_coefficient_1 = value; NotifyPropertyChanged("DFB_Average_Additional_Stress_Coefficient_1"); }
        }
        private double _dfb_average_additional_stress_coefficient_1;

        ////cal0kpa
        //public double CalVoidratio0kPa
        //{
        //    get { return _calvoidration0kPa; }
        //    set { _calvoidration0kPa = value; NotifyPropertyChanged("CalVoidratio0kPa"); }
        //}
        //private double _calvoidration0kPa;

        ////cal50kpa
        //public double CalVoidratio50kPa
        //{
        //    get { return _calvoidration50kPa; }
        //    set { _calvoidration50kPa = value; NotifyPropertyChanged("CalVoidratio50kPa"); }
        //}
        //private double _calvoidration50kPa;

        ////cal100kpa
        //public double CalVoidratio100kPa
        //{
        //    get { return _calvoidration100kPa; }
        //    set { _calvoidration100kPa = value; NotifyPropertyChanged("CalVoidratio100kPa"); }
        //}
        //private double _calvoidration100kPa;

        ////cal200kpa
        //public double CalVoidratio200kPa
        //{
        //    get { return _calvoidration200kPa; }
        //    set { _calvoidration200kPa = value; NotifyPropertyChanged("CalVoidratio200kPa"); }
        //}
        //private double _calvoidration200kPa;

        ////cal300kpa
        //public double CalVoidratio300kPa
        //{
        //    get { return _calvoidration300kPa; }
        //    set { _calvoidration300kPa = value; NotifyPropertyChanged("CalVoidratio300kPa"); }
        //}
        //private double _calvoidration300kPa;

        ////cal400kpa
        //public double CalVoidratio400kPa
        //{
        //    get { return _calvoidration400kPa; }
        //    set { _calvoidration400kPa = value; NotifyPropertyChanged("CalVoidratio400kPa"); }
        //}
        //private double _calvoidration400kPa;

    }

}

