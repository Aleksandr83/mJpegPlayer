// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mimica.Wpf.Controls
{
    public abstract class CControl:
        Control,
        INotifyPropertyChanged
    {

        #region Constructor
        public CControl()
        {
        }
        #endregion Constructor

        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion Property Changed

    }
}
