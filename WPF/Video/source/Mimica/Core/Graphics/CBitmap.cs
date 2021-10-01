// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimica.Core.Graphics
{
    public class CBitmap
    {
        public int Width  { get; private set; }
        public int Height { get; private set; }
        protected  UInt32[,] pixelMatrix { get; set; }
        protected  int Stride { get; private set; }
        protected  int Size   { get; private set; }
        private    byte[] Matrix { get; set; }
        public int DpiX { get; private set; }
        public int DpiY { get; private set; }

        #region Constructor
        public CBitmap(int width, int height,int dpiX = 0,int dpiY = 0)
        {
            Width = width;
            Height = height;
            DpiX = (dpiX == 0) ? 96 : dpiX;
            DpiY = (dpiY == 0) ? 96 : dpiY;          
            Stride = Width  * 4;
            Size   = Height * Stride;
            if ((width == 0) || (height == 0)) return;
            pixelMatrix = new UInt32[width, height];
            Matrix = new byte[Height * Width * 4];
        }
        #endregion Constructor
        #region GetPixel
        public UInt32 GetPixel(int x,int y)
        {
            return pixelMatrix[x, y];
        }
        #endregion GetPixel
        #region SetPixel
        public void SetPixel(int x,int y,UInt32 value)
        {
            pixelMatrix[x, y] = value;
        }
        #endregion SetPixel
        #region GetSize
        public virtual int GetSize()
        {
            return Size;
        }
        #endregion GetSize
        #region Load
        public void Load(byte[] pixels)
        {
            if (pixels == null) return;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Stride + 4 * x;
                    byte red = pixels[index];
                    byte green = pixels[index + 1];
                    byte blue = pixels[index + 2];
                    byte alpha = pixels[index + 3];
                    pixelMatrix[x, y] = CRgba.RgbaToUint(red, green, blue, alpha);
                }
            }

        }
        #endregion Load        
        #region ToArray
        public byte[] ToArray()
        {
            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var rgba = CRgba.UIntToRgba(pixelMatrix[x, y]);
                    Matrix[index++] = rgba.Red;
                    Matrix[index++] = rgba.Green;
                    Matrix[index++] = rgba.Blue;
                    Matrix[index++] = rgba.Alfa;
                }
            }
            return Matrix;
        }
        #endregion ToArray
        #region GetStream
        public Stream GetStream()
        {
            return new MemoryStream(ToArray());
        }
        #endregion GetStream
        #region Fill
        public virtual void Fill(UInt32 color = 0xFFFFFFFF)
        {
            for(int i = 0;i < Height;i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    pixelMatrix[j, i] = color;
                }
            }
        }
        #endregion Fill
    }
}
