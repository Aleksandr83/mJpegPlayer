// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimica.Types
{
    public class CDynKeyValue:
        CKeyValue<dynamic,dynamic>
    {

        #region Constructor
        public CDynKeyValue(dynamic key,dynamic value)
            :base((object)key,(object)value)
        {
        }
        #endregion Constructor

    }
}
