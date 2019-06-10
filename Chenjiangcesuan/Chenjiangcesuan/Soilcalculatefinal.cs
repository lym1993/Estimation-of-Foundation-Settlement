using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenjiangcesuan
{
    class Soilcalculatefinal: INotifyPropertyChanged
    {
        //此类用于展示最终计算结果
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //分层深度 LayerDepth
        public double LayerDepth
        {
            get { return _layerdepth; }
            set { _layerdepth = value; NotifyPropertyChanged("LayerDepth"); }
        }
        private double _layerdepth;

        //天然重度Natural unit weight
        public double NaturalUnitWeight
        {
            get { return _naturalunitweight; }
            set { _naturalunitweight = value; NotifyPropertyChanged("NaturalUnitWeight"); }
        }
        private double _naturalunitweight;

        //有效重度effective unit weight
        public double EffectiveUnitWeight
        {
            get { return _effectiveunitweight; }
            set { _effectiveunitweight = value; NotifyPropertyChanged("EffectiveUnitWeight"); }
        }
        private double _effectiveunitweight;

        //土层自重应力Self weight stress of soil layer
        public double SelfWeightStressofSoilLayer
        {
            get { return _selfweithtstressofsoillayer; }
            set { _selfweithtstressofsoillayer = value; NotifyPropertyChanged("SelfWeightStressofSoilLayer"); }
        }
        private double _selfweithtstressofsoillayer;

        //附加应力图面积Additional stress map area
        public double AdditionalStressMapArea
        {
            get { return _additionalstressmaparea; }
            set { _additionalstressmaparea = value; NotifyPropertyChanged("AdditionalStressMapArea"); }
        }
        private double _additionalstressmaparea;

        //分层厚度Layer thickness
        public double LayerThickness
        {
            get { return _layerthickness; }
            set { _layerthickness = value; NotifyPropertyChanged("LayerThickness"); }
        }
        private double _layerthickness;

        //附加应力Additional stress
        public double AdditionalStress
        {
            get { return _additionalstress; }
            set { _layerthickness = value; NotifyPropertyChanged("AdditionalStress"); }
        }
        private double _additionalstress;

        //土层总应力Total stress of soil layer
        public double TotalStressofSoilLayer
        {
            get { return _totalstressofsoillayer; }
            set { _totalstressofsoillayer = value; NotifyPropertyChanged("TotalStressofSoilLayer"); }
        }
        private double _totalstressofsoillayer;

        //受压前孔隙比e1i
        public double E1i
        {
            get { return _e1i; }
            set { _e1i = value; NotifyPropertyChanged("E1i"); }
        }
        private double _e1i;

        //受压后孔隙比e2i
        public double E2i
        {
            get { return _e2i; }
            set { _e2i = value; NotifyPropertyChanged("E2i"); }
        }
        private double _e2i;

        //压缩模量Esi
        public double Esi
        {
            get { return _esi; }
            set { _esi = value; NotifyPropertyChanged("Esi"); }
        }
        private double _esi;

        //土层变形量Deformation of soil layer
        public double DeformationofSoilLayer
        {
            get { return _deformationofsoillayer; }
            set { _deformationofsoillayer = value; NotifyPropertyChanged("DeformationofSoilLayer"); }
        }
        private double _deformationofsoillayer;

        //土层变形总量Total Deformation of Soil Layer
        public double TotalDeformationofSoilLayer
        {
            get { return _totaldeformationofsoillayer; }
            set { _totaldeformationofsoillayer = value; NotifyPropertyChanged("TotalDeformationofSoilLayer"); }
        }
        private double _totaldeformationofsoillayer;

    }
}
