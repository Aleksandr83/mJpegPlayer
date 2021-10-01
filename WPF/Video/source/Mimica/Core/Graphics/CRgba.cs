// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimica.Core.Graphics
{
    public class CRgba
    {
        public byte Alfa  { get; set; }
        public byte Red   { get; set; }
        public byte Green { get; set; }
        public byte Blue  { get; set; }

        #region Constructor
        public CRgba()
        {
            Alfa  = 0xFF;
            Red   = 0x00;
            Green = 0x00;
            Blue  = 0x00;
        }
        #endregion Constructor

        #region ToInteger
        public uint ToInteger()
        {
            return RgbaToUint(Red, Green, Blue, Alfa);
        }
        #endregion ToInteger
        #region Static RgbaToUint
        public static uint RgbaToUint(byte r,byte g,byte b,byte a)
        {
            return (uint)((a << 24) | (r << 16) | (g << 8) | (b << 0));
        }
        #endregion RgbaToUint
        #region Static UIntToRgba
        public static CRgba UIntToRgba(uint rgba)
        {
            return new CRgba()
            {
                Alfa  = (byte)(rgba >> 24),
                Red   = (byte)(rgba >> 16),
                Green = (byte)(rgba >> 8),
                Blue  = (byte)(rgba >> 0)
            };
        }
        #endregion Static UIntToRgba
    }
}
