// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video
{
    public class CActiveCamera
    {
        public CCamera Camera { get; set; }
        public int GridRow { get; set; }
        public int GridRowSpan { get; set; }
        public int GridColumn { get; set; }
        public int GridColumnSpan { get; set; }

        #region Constructor
        public CActiveCamera(CCamera camera)
        {
            Camera = camera;
            GridRow = 0;
            GridColumn  = 0;
            GridRowSpan = 0;
            GridColumnSpan = 0;
        }
        #endregion Constructor
    }
}
