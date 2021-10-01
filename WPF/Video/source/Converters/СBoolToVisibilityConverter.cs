// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
#if (WPF)
using System.Windows.Data;
#endif
#if (WINDOWS_UWP)
using Windows.UI.Xaml;
#endif

namespace Mimica.Utils.Converters
{
    public class СBoolToVisibilityConverter :
        CConverter
    {

        #region Convert
        public override object Convert(object value, Type targetType,
            object parameter, CultureInfo culture, string language)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion Convert
        #region ConvertBack
        public override object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture, string language)
        {
            throw new NotImplementedException();
        }
        #endregion ConvertBack

    }
}
