// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using Mimica.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
#if (WINDOWS_UWP)
using Windows.UI.Xaml;
#endif


namespace Mimica.Controls
{
    public class CScale :
        DependencyObject
        , INotifyPropertyChanged
    {
        #region Value
        public static readonly DependencyProperty ValueProperty = CXamlUtils
           .RegisterDependencyProperty("Value", typeof(double), typeof(CScale),
            CXamlUtils.CreatePropertyMetadata(null));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                OnPropertyChanged("Value");
            }
        }
        #endregion Value
        #region Minimum
        public static readonly DependencyProperty MinimumProperty = CXamlUtils
           .RegisterDependencyProperty("Minimum", typeof(double), typeof(CScale),
            CXamlUtils.CreatePropertyMetadata(null));
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set
            {
                SetValue(MinimumProperty, value);
                OnPropertyChanged("Minimum");
            }
        }
        #endregion Minimum
        #region Maximum
        public static readonly DependencyProperty MaximumProperty = CXamlUtils
           .RegisterDependencyProperty("Maximum", typeof(double), typeof(CScale),
            CXamlUtils.CreatePropertyMetadata(null));
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set
            {
                SetValue(MaximumProperty, value);
                OnPropertyChanged("Maximum");
            }
        }
        #endregion Maximum

        #region Constructor
        public CScale()
            : base()
        {
            Value = 0;
            Minimum = 0;
            Maximum = 100;
        }
        #endregion Constructor

        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion Property Changed
    }
}
