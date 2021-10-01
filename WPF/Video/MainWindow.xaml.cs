// Copyright (c) 2017 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MjpegProcessor;

namespace Video
{    
    public partial class MainWindow : Window
    {
        #region VideoGrid
        public static readonly DependencyProperty VideoGridProperty = DependencyProperty
            .Register("VideoGrid", typeof(CVideoGrid), typeof(MainWindow), null);
        public UIElement VideoGrid
        {
            get { return (UIElement)GetValue(VideoGridProperty); }
            set { SetValue(VideoGridProperty,value); }
        }
        #endregion VideoGrid

        #region Constructor
        public MainWindow()
        {          
            InitializeComponent();            
            VideoGrid = new CVideoGrid().Load();
            DataContext = this;
        }
        #endregion Constructor

    }
}
