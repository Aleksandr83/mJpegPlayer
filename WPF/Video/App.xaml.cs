// Copyright (c) 2017 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Video
{    
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(OnUnhandledException);
            Application.Current.Dispatcher.UnhandledExceptionFilter += new DispatcherUnhandledExceptionFilterEventHandler(OnUnhandledException);
            base.OnStartup(e);
        }

        public static void OnUnhandledException(object sender, DispatcherUnhandledExceptionFilterEventArgs e)
        {
            //ShowErrorMessage(e.Exception.Message);
            e.RequestCatch = true;
        }

        public static void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //ShowErrorMessage(e.Exception.Message);
            e.Handled = true;
        }

       
        static void ShowErrorMessage(dynamic message)
        {
            //message = (message == null) ? "" : message.Trim();
            //if (message == "") return;
            //MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
