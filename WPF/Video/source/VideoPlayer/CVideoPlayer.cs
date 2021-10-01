// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using Mimica.Controls;
using Mimica.Utils;
using Mimica.WpfUtils.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Mimica.Wpf.Controls
{
    public abstract class CVideoPlayer : 
        CScaleControl
    {
        public String Source { get; set; }
        
        #region IsFullscreen
        public static readonly DependencyProperty IsFullscreenProperty = CXamlUtils
        .RegisterDependencyProperty("IsFullscreen", typeof(bool), typeof(CVideoPlayer), null);

        public dynamic IsFullscreen
        {
            get { return (dynamic)GetValue(IsFullscreenProperty); }
            set
            {
                SetValue(IsFullscreenProperty, (dynamic)value);
                OnPropertyChanged("IsFullscreen");
            }
        }
        #endregion IsFullscreen

        protected CBitmap Bitmap { get; set; }
        #region AutoPlay
        bool _AutoPlay = false;
        public bool AutoPlay
        {
            get { return _AutoPlay; }
            set
            {
                _AutoPlay = value;
                if (_AutoPlay) Play();
            }
        }
        #endregion AutoPlay
        #region ViewPort
        public static readonly DependencyProperty ViewPortProperty = DependencyProperty
            .Register("ViewPort", typeof(object), typeof(CVideoPlayer), null);
        public object ViewPort
        {
            get { return (object)GetValue(ViewPortProperty); }
            private set { SetValue(ViewPortProperty, value); }
        }
        #endregion ViewPort        

        #region Commands
        public static RoutedCommand PlayCommand { get; private set; }
        public static RoutedCommand StopCommand  { get; private set; }
        public static RoutedCommand FullscreenCommand  { get; private set; }
        #endregion Commands

        #region Static Constructor
        static CVideoPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CVideoPlayer),
                new FrameworkPropertyMetadata(typeof(CVideoPlayer)));
            #region PlayCommand
            PlayCommand = new RoutedCommand("PlayCommand",
                typeof(CVideoPlayer));
            CommandManager.RegisterClassCommandBinding(typeof(CVideoPlayer),
                new CommandBinding(PlayCommand, OnPlayCommand));
            #endregion PlayCommand
            #region StopCommand
            StopCommand = new RoutedCommand("StopCommand",
                typeof(CVideoPlayer));
            CommandManager.RegisterClassCommandBinding(typeof(CVideoPlayer),
                new CommandBinding(StopCommand, OnStopCommand));
            #endregion StopCommand
            #region FullscreenCommand
            FullscreenCommand = new RoutedCommand("FullscreenCommand",
                typeof(CVideoPlayer));
            CommandManager.RegisterClassCommandBinding(typeof(CVideoPlayer),
                new CommandBinding(FullscreenCommand, OnFullscreenCommand));
            #endregion FullscreenCommand
        }
        #endregion Static Constructor
        #region Constructor
        public CVideoPlayer(String source = "")
            :base()
        {
            Source = source;
            dynamic viewPort = new Image();       
            viewPort.HorizontalAlignment = HorizontalAlignment.Stretch;
            viewPort.VerticalAlignment   = VerticalAlignment.Stretch;
            ViewPort = viewPort;
        }
        #endregion Constructor

        public abstract void Play();       
        public abstract void Stop();
        public abstract void EnterFullscreen();
        public abstract void ExitFullscreen();

        #region CreateBitmap
        protected virtual void CreateBitmap(BitmapImage frame)
        {
            if (Bitmap != null) return;
            Bitmap = new CBitmap(frame.PixelWidth, frame.PixelHeight, (int)frame.DpiX, (int)frame.DpiY);
        }
        #endregion CreateBitmap
        #region FrameReady
        public virtual void FrameReady(BitmapImage frame)
        {
            if ((ViewPort == null)||(frame == null)) return;   
            CreateBitmap(frame);
            if (Bitmap == null) return;
            Bitmap.Load(frame);
        }
        #endregion FrameReady
        #region ShowFrame
        protected virtual void ShowFrame()
        {
            dynamic viewPort = ViewPort;
            var image = Bitmap.GetWriteableBitmap();
            viewPort.Source = image;

        }
        #endregion ShowFrame

        #region OnPlayCommand
        static void OnPlayCommand(object sender, RoutedEventArgs args)
        {
            if (!(sender is CVideoPlayer)) return;
            var player = (CVideoPlayer)sender;
            player.Play();
        }
        #endregion OnPlayCommand
        #region OnStopCommand
        static void OnStopCommand(object sender, RoutedEventArgs args)
        {
            if (!(sender is CVideoPlayer)) return;
            var player = (CVideoPlayer)sender;
            player.Stop();
         
        }
        #endregion OnStopCommand
        #region OnFullscreenCommand
        static void OnFullscreenCommand(object sender, RoutedEventArgs args)
        {
            if (!(sender is CVideoPlayer)) return;
            var player = (CVideoPlayer)sender;
            if (player.IsFullscreen)
            {
                player.ExitFullscreen();
                player.IsFullscreen = false;
            }
            else
            {
                player.EnterFullscreen();
                player.IsFullscreen = true;
            }           
        }
        #endregion OnFullscreenCommand

    }
}
