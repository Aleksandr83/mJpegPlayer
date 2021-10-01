// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
//using Mimica.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
#if (WPF)
using System.Windows.Data;
using System.Windows.Media;
#endif
#if (WINDOWS_UWP)
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#endif

namespace Mimica.Utils
{
    public sealed class CXamlUtils
    {
        public const BindingMode DefaultBindingMode = BindingMode.OneWay;

        #region ColorToBrush
        public static Brush ColorToBrush(string color) // color = "#E7E44D"
        {
            color = color.Replace("#", "");
            if (color.Length == 8)
            {
                return new SolidColorBrush(Color.FromArgb(
                    byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)));
            }
            else
            {
                return null;
            }
        }
        #endregion ColorToBrush
        #region TryFindResource
        public static dynamic TryFindResource(dynamic control, dynamic resource)
        {
            dynamic result = null;
            try
            {
#if (WPF)
                //if (control != null)
                //    result = control.TryFindResource((string)resource);
                if (result == null)
                    result = Application.Current.TryFindResource((string)resource);
#endif
#if (WINDOWS_UWP)              
                if (!Application.Current.Resources.TryGetValue((string)resource,out result))
                {
                    result = null;
                } 
#endif
            }
            catch { throw; }
            return result;
        }
        #endregion TryFindResource
        #region RegisterDependencyProperty
        public static dynamic RegisterDependencyProperty
            (
                string name,
                Type propertyType,
                Type ownerType,
                PropertyMetadata typeMetadata = null
            )
        {
            dynamic result;
            result = DependencyProperty.Register
                (
                    name,
                    propertyType,
                    ownerType,
                    typeMetadata
                );
            return result;
        }
        #endregion RegisterDependencyProperty
        #region CreatePropertyMetadata
        public static dynamic CreatePropertyMetadata
            (PropertyChangedCallback callback)

        {
            dynamic result = null;
#if (WPF)
            result = new PropertyMetadata(callback);
#endif
#if (WINDOWS_UWP)
            result = new PropertyMetadata(null, callback);
#endif
            return result;
        }
        #endregion CreatePropertyMetadata
        #region CreateRoutedEventArgs
        public static dynamic CreateRoutedEventArgs(dynamic eventHandler)
        {
            dynamic result = null;
#if (WPF)
            result = new RoutedEventArgs(eventHandler);
#endif
#if (WINDOWS_UWP)
            result = new RoutedEventArgs();
#endif
            return result;
        }
        #endregion CreateRoutedEventArgs
        #region CreateBinding
        public static dynamic CreateBinding(string path = null)
        {
            dynamic result;
            result = new Binding();
            if (!string.IsNullOrEmpty(path))
                (result as Binding).Path = new PropertyPath(path);
            return result;
        }
        #endregion CreateBinding

        #region RaiseEvent
        public static void RaiseEvent(UIElement control, dynamic eventArgs)
        {
#if (WPF)
            
#endif
        }
        #endregion RaiseEvent
        #region AddHandler
        public static void AddHandler(UIElement control, dynamic routedEvent, dynamic handler)
        {
            //return;
            //if (control == null) return;
#if (WPF)
            control.AddHandler(routedEvent, handler);
#endif
#if (WINDOWS_UWP)
            control.AddHandler(routedEvent, handler, true);
#endif           
        }
        #endregion AddHandler
        #region RemoveHandler
        public static void RemoveHandler(UIElement control, dynamic routedEvent, dynamic handler)
        {
            if (control == null) return;
            control.RemoveHandler(routedEvent, handler);
        }
        #endregion RemoveHandler

    }

}
