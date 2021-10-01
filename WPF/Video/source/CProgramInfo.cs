// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Video
{
    public class CProgramInfo : DependencyObject
    {
        #region Version
        public static readonly DependencyProperty VersionProperty = DependencyProperty
          .Register("Version", typeof(string), typeof(CProgramInfo),
           new PropertyMetadata(null));
        public string Version
        {
            get { return (string)GetValue(VersionProperty); }
            set { SetValue(VersionProperty,(string)value); }
        }
        #endregion Version
        public static dynamic Instance { get; set; }

        #region Static Constructor
        static CProgramInfo()
        {
            Instance = new CProgramInfo()
            {
                Version = GetVersion()
            };
        }
        #endregion Static Constructor
        #region GetVersion
        static string GetVersion()
        {        
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();            
            var attribut = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetCustomAttributesData()
                .Where(x => x.AttributeType.Name == "AssemblyFileVersionAttribute")
                .FirstOrDefault();           
            return String.Format("version: {0}", version);
        }
        #endregion GetVersion
    }
}
