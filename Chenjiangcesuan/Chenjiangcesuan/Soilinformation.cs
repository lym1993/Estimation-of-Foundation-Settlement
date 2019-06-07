using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenjiangcesuan
{
    public class Soilinformation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double SoilThickness
        {
            get { return _soilThickness; }
            set { _soilThickness = value ; NotifyPropertyChanged("SoilThickness");}
        }
        private double _soilThickness;

        public double SoilUnitWeight
        {
            get { return _soilUnitWeight; }
            set { _soilUnitWeight = value;NotifyPropertyChanged("SoilUnitWeight"); }
        }
        private double _soilUnitWeight;

        public double Voidratio0kPa
        {
            get { return _voidration0kPa; }
            set { _voidration0kPa = value;NotifyPropertyChanged(" Voidratio0kPa"); }
        }
        private double _voidration0kPa;

        public double Voidratio50kPa
        {
            get { return _voidration50kPa; }
            set { _voidration50kPa = value; NotifyPropertyChanged(" Voidratio50kPa"); }
        }
        private double _voidration50kPa ;

        public double Voidratio100kPa
        {
            get { return _voidration100kPa; }
            set { _voidration100kPa = value; NotifyPropertyChanged(" Voidratio100kPa"); }
        }
        private double _voidration100kPa;

        public double Voidratio200kPa
        {
            get { return _voidration200kPa; }
            set { _voidration200kPa = value; NotifyPropertyChanged(" Voidratio200kPa"); }
        }
        private double _voidration200kPa;

        public double Voidratio300kPa
        {
            get { return _voidration300kPa; }
            set { _voidration300kPa = value; NotifyPropertyChanged(" Voidratio300kPa"); }
        }
        private double _voidration300kPa;

        public double Voidratio400kPa
        {
            get { return _voidration400kPa; }
            set { _voidration400kPa = value; NotifyPropertyChanged(" Voidratio400kPa"); }
        }
        private double _voidration400kPa;
       
        
    }
}
