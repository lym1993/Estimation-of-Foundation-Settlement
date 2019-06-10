using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenjiangcesuan
{
    //定义土层信息类，该类继承于 INotifyPropertyChanged类，与前台datagrid实行双向绑定。
    //用于收集用户输入信息

    public class Soilinformation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string SoilThickness
        {
            get { return _soilThickness; }
            set { _soilThickness = value ; NotifyPropertyChanged("SoilThickness");}
        }
        private string _soilThickness;

        public string SoilUnitWeight
        {
            get { return _soilUnitWeight; }
            set { _soilUnitWeight = value;NotifyPropertyChanged("SoilUnitWeight"); }
        }
        private string _soilUnitWeight;

        public double Haswater
        {
            get { return _haswater; }
            set { _haswater = value; NotifyPropertyChanged("Haswater"); }
        }
        private double _haswater;

        public string Voidratio0kPa
        {
            get { return _voidration0kPa; }
            set { _voidration0kPa = value;NotifyPropertyChanged(" Voidratio0kPa"); }
        }
        private string _voidration0kPa;

        public string Voidratio50kPa
        {
            get { return _voidration50kPa; }
            set { _voidration50kPa = value; NotifyPropertyChanged(" Voidratio50kPa"); }
        }
        private string _voidration50kPa ;

        public string Voidratio100kPa
        {
            get { return _voidration100kPa; }
            set { _voidration100kPa = value; NotifyPropertyChanged(" Voidratio100kPa"); }
        }
        private string _voidration100kPa;

        public string Voidratio200kPa
        {
            get { return _voidration200kPa; }
            set { _voidration200kPa = value; NotifyPropertyChanged(" Voidratio200kPa"); }
        }
        private string _voidration200kPa;

        public string Voidratio300kPa
        {
            get { return _voidration300kPa; }
            set { _voidration300kPa = value; NotifyPropertyChanged(" Voidratio300kPa"); }
        }
        private string _voidration300kPa;

        public string Voidratio400kPa
        {
            get { return _voidration400kPa; }
            set { _voidration400kPa = value; NotifyPropertyChanged(" Voidratio400kPa"); }
        }
        private string _voidration400kPa;
             
    }
}
