// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using Mimica.Types;
using Mimica.Wpf.Controls;
using MjpegProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Video
{
    public class CCamera : 
        CVideoPlayer
    {       
        bool IsStop { get; set; }
        bool IsUnvisible { get; set; }
        private MjpegDecoder Decoder { get; set; }
        static List<CCamera> Players { get; set; }
        static CActiveCamera ActiveCamera { get; set; }
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public int GridRowspan { get; set; }
        public int GridColumnspan { get; set; }
        public bool AutoReconect { get; set; }
        public bool IsShowErrorMessage { get; set; }
        private CVideoGrid VideoGrid { get; set; }

        #region Static Constructor
        static CCamera()
        {
            Players = new List<CCamera>();
        }
        #endregion Static Constructor
        #region Constructor
        public CCamera(CVideoGrid videoGrid)
            : this("", videoGrid)
        {           
            
        }

        public CCamera(String source,CVideoGrid videoGrid) :base(source)
        {
            IsStop = true;
            VideoGrid = videoGrid;
            IsUnvisible = false;
            Players.Add(this);
            InitDecoder();
        }
        #endregion Constructor

        #region SetStyle
        protected override dynamic SetStyle()
        {
            dynamic result = TryFindResource("DefaultCameraStyle");
            return  result;
        }
        #endregion SetStyle

        #region InitDecoder
        protected virtual void InitDecoder()
        {            
            Decoder = new MjpegDecoder();
            Decoder.FrameReady += new EventHandler<FrameReadyEventArgs>((x, y) =>
            {
                OnFrameReady(x, y.BitmapImage);
            });
            Decoder.Error += OnError;
        }
        #endregion InitDecoder

        #region CreateBitmap
        protected override void CreateBitmap(BitmapImage frame)
        {            
            Bitmap = new CBitmap
                (
                    frame.PixelWidth, 
                    frame.PixelHeight, 
                    (int)frame.DpiX, 
                    (int)frame.DpiY
                );            
        }
        #endregion CreateBitmap
        #region FrameReady
        public override void FrameReady(BitmapImage frame)
        {
            if ((IsStop)||(IsUnvisible)) return;
            base.FrameReady(frame);
            (Bitmap as Video.CBitmap).ApplyFilters();
        }
        #endregion FrameReady
        #region OnFrameReady
        protected virtual void OnFrameReady(object sender, dynamic frame)
        {
            try
            {
                var bi = frame.Clone();                
                FrameReady(bi);                   
                ShowFrame();
            }
            catch (Exception) { }
        }
        #endregion OnFrameReady        
        #region OnError
        void OnError(object sender, ErrorEventArgs e)
        {
            //if (IsShowErrorMessage) MessageBox.Show(e.Message);
            if (AutoReconect)
            {
                Stop();              
                Play();                
            }
        }
        #endregion OnError
        
        async Task<int> ExecuteRun()
        {
            
            int result = 0;
            if (!IsStop) return result;
            Source = Source.Trim();
            if ((Source == null) || (Source == "")) return result;
            Decoder.ParseStream(new Uri(Source));
            OnPlay();
            return 0;
        }

        #region Play
        public override void Play()
        {
            ExecuteRun();
        }
        #endregion Play
        #region Stop
        public override void Stop()
        {
            try
            {               
                OnStop();
                Decoder.StopStream();
            }
            catch { }
        }
        #endregion Stop    
        #region OnPlay
        protected virtual void OnPlay()
        {
            IsStop = false;
        }
        #endregion OnPlay
        #region OnStop
        protected virtual void OnStop()
        {   
            
            IsStop = true;
            Bitmap.Fill(0x00000000);            
        }
        #endregion OnStop

        #region SaveProperties
        void SaveProperties()
        {
            ActiveCamera = new CActiveCamera(this)
            {
                GridRow = Grid.GetRow(this),
                GridColumn = Grid.GetColumn(this),
                GridRowSpan = Grid.GetRowSpan(this),
                GridColumnSpan = Grid.GetColumnSpan(this),
            };

        }
        #endregion SaveProperties
        #region RestoryProperties
        void RestoryProperties()
        {
            var camera = ActiveCamera.Camera;
            Grid.SetRow(camera, ActiveCamera.GridRow);
            Grid.SetColumn(camera, ActiveCamera.GridColumn);
            Grid.SetRowSpan(camera, ActiveCamera.GridRowSpan);
            Grid.SetColumnSpan(camera, ActiveCamera.GridColumnSpan);
            ActiveCamera = null;
        }
        #endregion RestoryProperties
        #region EnterFullscreen
        public override void EnterFullscreen()
        {
            foreach (var item in Players)
            {
                if (item != this)
                {
                    item.IsUnvisible = true;
                    item.Visibility = Visibility.Collapsed;
                }
            }
            SaveProperties();
            Grid.SetRow(this, 0);
            Grid.SetColumn(this, 0);
            Grid.SetRowSpan(this, VideoGrid.GridRowCount);
            Grid.SetColumnSpan(this, VideoGrid.GridColumnsCount);  
        }
        #endregion EnterFullscreen
        #region ExitFullscreen
        public override void ExitFullscreen()
        {
            foreach (var item in Players)
            {                
                item.IsUnvisible = false;
                item.Visibility = Visibility.Visible;
                item.Scale.Value = 0.1;
            }
            RestoryProperties();
        }
        #endregion ExitFullscreen       

        #region LoadScaleValue
        protected override void LoadScaleValue()
        {
            Scale.Value = 0.1;
        }
        #endregion LoadScaleValue

    }
}
