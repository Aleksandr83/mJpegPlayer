using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video
{   
    sealed class CMargin
    {
        public static System.Windows.Thickness Parse(string value)
        {
            double top = 0.0;
            double left = 0.0;
            double right = 0.0;
            double bottom = 0.0;
            string[] items = value.Trim().Split(new String[] {","," "},StringSplitOptions.RemoveEmptyEntries);
            if ((items == null) || (items.Length == 0)) return new System.Windows.Thickness();
            if (items.Length == 1) return new System.Windows.Thickness(double.Parse(items.First()));
            if (items.Length == 2)
            {
                left = right = double.Parse(items.First());
                top  = bottom = double.Parse(items.Last());
            }
            else
            {
                if (items.Length != 4) return new System.Windows.Thickness();
                left   = double.Parse(items[0]);
                top    = double.Parse(items[1]);
                right  = double.Parse(items[2]);
                bottom = double.Parse(items[3]);
            }
            return new System.Windows.Thickness(left,top,right,bottom);
        }
    }
}
