// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimica.Types
{
    public static class CException
    {

        #region PrintDebugException
        public static void PrintDebugException(Exception ex)
        {
            string s = null;
            s = string.Format("{0} : {1}",ex.Message,ex.StackTrace);
            Debug.Print(s);
        }
        #endregion PrintDebugException

    }
}
