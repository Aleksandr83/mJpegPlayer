// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video
{
    public class CBitmap : 
        Mimica.WpfUtils.Graphics.CBitmap
    {
        #region Constructor
        public CBitmap(int width, int height, int dpiX = 0, int dpiY = 0):
            base(width,height,dpiX,dpiY)
        {

        }
        #endregion Constructor

        #region ApplyFilters
        public virtual void ApplyFilters()
        {            
        }
        #endregion ApplyFilters

    }
}
