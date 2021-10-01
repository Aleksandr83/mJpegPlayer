// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimica.Types
{
    public class CKeyValue<TKey, TValue>
    {
        public TKey Key { get; private set; }
        public TValue Value { get; set; }

        #region Constructor
        public CKeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        #endregion Constructor
    }
}
