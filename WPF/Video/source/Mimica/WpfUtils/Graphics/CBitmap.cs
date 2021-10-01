// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Numerics;

namespace Mimica.WpfUtils.Graphics
{
    public class CBitmap : Mimica.Core.Graphics.CBitmap
    {      
        #region Constructor
        public CBitmap(int width,int height, int dpiX = 0, int dpiY = 0)
            :base(width,height,dpiX,dpiY)
        {
            
        }
        #endregion Constructor
        #region GetBitmapImage
        public BitmapImage GetBitmapImage()
        {
            BitmapImage result = new BitmapImage();
            result.StreamSource = GetStream();
            return result;
        }
        #endregion GetBitmapImage
       
        #region GetWriteableBitmap
        public WriteableBitmap GetWriteableBitmap()
        {
            WriteableBitmap result = new WriteableBitmap(
                Width, Height, DpiX, DpiY, PixelFormats.Bgra32, null);     
            var rect = new Int32Rect(0, 0, Width, Height);            
            var buffer = ToArray();           
            result.WritePixels(rect, buffer, Stride, 0);           
            return result;
        }
        #endregion GetWriteableBitmap
        #region Load
        public virtual void Load(BitmapImage image)
        {
            byte[] pixels = new byte[Size];
            image.CopyPixels(pixels, Stride, 0);
            Load(pixels);
        }
        #endregion Load
        
    }
}
