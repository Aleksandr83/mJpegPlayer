// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Mimica.Types;
using System.Windows;

namespace Video
{
    public class CVideoGrid : Grid
    {
        const string  ConfigurationFile = "Configuration.xml";        
        public int GridRowCount { get; private set; }
        public int GridColumnsCount { get; private set; }

        class CGridSize
        {
            public int RowsCount { get; set; } = 0;
            public int ColumnsCount { get; set; } = 0;
        }

        #region Constructor
        public CVideoGrid()
            :base()
        {            
            
        }
        #endregion Constructor

        #region SetRowDefinitions
        protected void SetRowDefinitions(int rowsCount)
        {
            GridRowCount = rowsCount;
            for (int i = 0; i < rowsCount; i++)
                RowDefinitions.Add(new RowDefinition()
                {
                    Height = new System.Windows.GridLength(100/rowsCount, System.Windows.GridUnitType.Star)
                });
        }
        #endregion SetRowDefinitions
        #region SetColumnDefinitions
        protected void SetColumnDefinitions(int columnsCount)
        {
            GridColumnsCount = columnsCount;
            for (int i = 0; i < columnsCount; i++)
                ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new System.Windows.GridLength(100/columnsCount, System.Windows.GridUnitType.Star)
                });
        }
        #endregion SetColumnDefinitions        
        #region CreateCameraControl
        protected virtual dynamic CreateCameraControl()
        {            
            dynamic result = null;
            result = new CCamera(this);
            return  result;
        }
        #endregion CreateCameraControl
        #region CreateCamera
        protected virtual dynamic CreateCamera(dynamic[] properties)
        {           
            dynamic result = CreateCameraControl();
            if (properties == null) return result;
            var list = new CKeyValuesList(properties);     
            if (list.IsContainKey("Source"))
            result.Source             = list.GetByNameValue("Source");
            if (list.IsContainKey("GridRow"))
            result.GridRow            = list.GetByNameValue("GridRow");
            if (list.IsContainKey("GridColumn"))
            result.GridColumn         = list.GetByNameValue("GridColumn");
            if (list.IsContainKey("GridRowspan"))
            result.GridRowspan        = list.GetByNameValue("GridRowspan");
            if (list.IsContainKey("GridColumnspan"))
            result.GridColumnspan     = list.GetByNameValue("GridColumnspan");
            if (list.IsContainKey("AutoPlay"))
            result.AutoPlay           = list.GetByNameValue("AutoPlay");
            if (list.IsContainKey("AutoReconect"))
            result.AutoReconect       = list.GetByNameValue("AutoReconect");
            if (list.IsContainKey("IsShowErrorMessage"))
            result.IsShowErrorMessage = list.GetByNameValue("IsShowErrorMessage");
            if (list.IsContainKey("Margin"))
            result.Margin             = list.GetByNameValue("Margin");
            return  result;
        }
        #endregion CreateCamera

        #region LoadValue
        private string LoadValue(dynamic camera, string elementName ,string defaultValue="")
        {
            string result = defaultValue;
            try
            {
                result = camera.Element(elementName).Value;
            }
            catch { }
            return result;
        }
        #endregion LoadValue
        #region LoadIntValue
        private int LoadIntValue(dynamic camera, string elementName, int defaultValue = 0)
        {
            string s;
            int result = defaultValue;
            s = LoadValue(camera, elementName);
            return int.TryParse(s, out result) ? result : defaultValue;             
        }
        #endregion LoadIntValue
        #region LoadBoolValue
        private bool LoadBoolValue(dynamic camera, string elementName, bool defaultValue = false)
        {
            string s;
            bool result = defaultValue;
            s = LoadValue(camera, elementName);
            return bool.TryParse(s, out result) ? result : defaultValue;
        }
        #endregion LoadBoolValue
        #region LoadMarginValue
        private Thickness LoadMarginValue(dynamic camera, string elementName) 
        {
            string s;            
            s = LoadValue(camera, elementName);
            return (CString.IsNotEmpty(s))? CMargin.Parse(s) : new Thickness(0, 0, 0, 0) ;
        }
        #endregion LoadMarginValue

        #region Load
        public virtual CVideoGrid Load()
        {            
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string file = Path.Combine(path, ConfigurationFile);
            if (!File.Exists(file)) return this;
            dynamic reader = (from gridsize in XDocument.Load(file).Descendants("grid")
                              select new CGridSize()
                              {
                                  RowsCount = int.Parse(gridsize.Element("rowscount").Value),
                                  ColumnsCount = int.Parse(gridsize.Element("columnscount").Value),
                              }).First();            
            SetRowDefinitions(reader.RowsCount);
            SetColumnDefinitions(reader.ColumnsCount);
            reader = (from camera in XDocument.Load(file).Descendants("camera")                         
                      select CreateCamera(new dynamic[] 
                      {                          
                          new CDynKeyValue("Source",LoadValue(camera,"source")),
                          new CDynKeyValue("GridRow",LoadIntValue(camera,"gridrow")),
                          new CDynKeyValue("GridColumn",LoadIntValue(camera,"gridcolumn")),                         
                          new CDynKeyValue("GridRowspan",LoadIntValue(camera,"gridrowspan")),
                          new CDynKeyValue("GridColumnspan",LoadIntValue(camera,"gridcolumnspan")),
                          new CDynKeyValue("AutoPlay",LoadBoolValue(camera,"autoplay")),
                          new CDynKeyValue("AutoReconect",LoadBoolValue(camera,"autoreconect")),
                          new CDynKeyValue("IsShowErrorMessage",LoadBoolValue(camera,"errormsg")),
                          new CDynKeyValue("Margin",LoadMarginValue(camera,"margin"))
                      })).ToList();
            foreach (var item in reader)
            {                
                Children.Add(item);
                Grid.SetRow(item, item.GridRow);
                Grid.SetColumn(item, item.GridColumn);
                Grid.SetRowSpan(item, item.GridRowspan);
                Grid.SetColumnSpan(item, item.GridColumnspan); 
            }            
            return this;
        }
        #endregion Load        
       
    }
}
