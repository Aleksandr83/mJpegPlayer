// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if (WPF)
using System.Windows.Data;
#endif
#if (WINDOWS_UWP)
using Windows.UI.Xaml.Data;
#endif


namespace Mimica.Utils.Converters
{
    public abstract class CConverter :
         IValueConverter
    {
        #region Convert WPF
#if (WPF)
        public virtual object Convert
            (
                object value,
                Type targetType,
                object parameter,
                CultureInfo culture
            )
        {
            return Convert(value, targetType, parameter, culture, null);
        }
#endif
        #endregion Convert WPF
        #region Convert UWP
#if (WINDOWS_UWP)
        public virtual object Convert
            (
                object value,
                Type   targetType,
                object parameter,                
                string language
            )
        {
            return Convert(value,targetType,parameter,null,language);
        }
#endif
        #endregion Convert UWP
        public abstract object Convert
            (
                object value,
                Type targetType,
                object parameter,
                CultureInfo culture,
                string language
            );
        #region ConvertBack WPF
#if (WPF)
        public virtual object ConvertBack
            (
                object value,
                Type targetType,
                object parameter,
                CultureInfo culture
            )
        {
            return ConvertBack(value, targetType, parameter, culture, null);
        }
#endif
        #endregion ConvertBack WPF
        #region ConvertBack UWP
#if (WINDOWS_UWP)
        public virtual object ConvertBack
            (
                object value,
                Type targetType,
                object parameter,
                string language
            )
        {
            return ConvertBack(value, targetType, parameter, null, language);
        }
#endif
        #endregion ConvertBack UWP
        public abstract object ConvertBack
            (
                object value,
                Type targetType,
                object parameter,
                CultureInfo culture,
                string language
            );
    }
}
