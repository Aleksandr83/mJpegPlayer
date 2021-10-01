// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using Mimica.Utils;
using Mimica.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
#if(WINDOWS_UWP)
using Windows.UI.Xaml;
#endif

namespace Mimica.Controls
{
    public abstract class CScaleControl
        :
        CControl,        
        INotifyPropertyChanged
    {
        public ICommand SaveScaleCommand { get; set; }

        #region Scale
        public static readonly DependencyProperty ScaleProperty = CXamlUtils
           .RegisterDependencyProperty("Scale", typeof(CScale), typeof(CScaleControl),
            CXamlUtils.CreatePropertyMetadata(null));
#if (WPF)
        public CScale Scale
#endif
#if (WINDOWS_UWP)
        public new CScale Scale
#endif
        {
            get { return (CScale)GetValue(ScaleProperty); }
            set
            {
                SetValue(ScaleProperty, (CScale)value);
                OnPropertyChanged("Scale");
            }
        }
        #endregion Scale        

        #region Static Constructor
        static CScaleControl()
        {

        }
        #endregion Static Constructor
        #region Constructor
        public CScaleControl()
            : base()
        {
            dynamic style = null;
            try
            {
                Scale = new CScale();
                style = SetStyle();
                if (style != null)
                    Style = (Style)style;
                LoadScaleValue();
            }
            catch
            {
                throw;
            }
        }
        #endregion Constructor

        protected abstract dynamic SetStyle();
        protected abstract void LoadScaleValue();


    }
}
