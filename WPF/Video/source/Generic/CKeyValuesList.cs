// Copyright (c) 2015-2021 Lukin Aleksandr
// email: lukin.a.g.spb@gmail.com
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimica.Types
{
    public class CKeyValuesList
    {
        dynamic Items = null;

        #region Constructor
        public CKeyValuesList(dynamic items = null)
        {
            if (items != null)
                Items = items;
            else
                Items = CreateItems(); 
        }
        #endregion Constructor

        #region Add
        public virtual void Add(dynamic item)
        {
            Exception ex = null;
            if (Items == null)
            {
                ex = new Exception("Items is null");
                CException.PrintDebugException(ex);
                throw ex;
            }
            Items.Add(item);
        }

        public virtual void Add(dynamic key, dynamic value)
        {
            Exception ex = null;
            if (CString.IsNullOrEmpty(key))
            {
                ex = new Exception("key is null or empty");
                CException.PrintDebugException(ex);
                throw ex;
            }
            Add(new CDynKeyValue(key, value));
        }
        #endregion Add
        #region CreateItems
        protected virtual dynamic CreateItems()
        {
            dynamic result = null;
            result = new ArrayList();
            return  result;
        }
        #endregion CreateItems
        #region GetByNameValue
        public virtual dynamic GetByNameValue(dynamic key,bool isCaseInsensitivity = true)
        {
            string name = null;            
            dynamic result = null;
            key = ((key is string)&&(isCaseInsensitivity)) ?
                key.ToUpper(): key;
            if (CArray.IsEmpty(Items)) return result;
            foreach (dynamic item in Items)
            {
                name = ((key is string) && (isCaseInsensitivity)) ? 
                    (item.Key as string).ToUpper(): item.Key;
                if (name == key)
                {
                    result = item.Value;
                    break;
                }
            }
            return  result;
        }
        #endregion GetByNameValue
        #region SetByNameValue
        public virtual dynamic SetByNameValue(dynamic key,dynamic value,bool isCaseInsensitivity = true)
        {
            bool   isFind  = false;
            string name    = null;
            dynamic result = null;            
            key = ((key is string)&&(isCaseInsensitivity)) ?
                key.ToUpper() : key;
            foreach (dynamic item in Items)
            {
                name = ((key is string)&&(isCaseInsensitivity)) ?
                    (item.Key as string).ToUpper() : item.Key;
                if (name == key)
                {
                    result = item.Value = value;
                    isFind = true;
                    break;
                }
            }
            if (!isFind)
            {
                if (Items is IList)
                {
                    Add(new CDynKeyValue(key, value));
                    result = value;
                }
                else
                {
                    try
                    {
                        Add(new CDynKeyValue(key, value));
                    }
                    catch(Exception ex)
                    {
                        CException.PrintDebugException(ex);
                    }
                }
            }
            return result;
        }
        #endregion SetByNameValue
        #region IsContainKey
        public virtual bool IsContainKey
            (string key, bool isCaseInsensitivity = true)
        {
            string  name   = null;
            dynamic result = false;
            key = ((key is string) && (isCaseInsensitivity)) ?
                key.ToUpper() : key;
            if (CArray.IsEmpty(Items)) return result;
            foreach (dynamic item in Items)
            {
                name = ((key is string) && (isCaseInsensitivity)) ?
                    (item.Key as string).ToUpper() : item.Key;
                if (name == key)
                {
                    result = true;
                    break;
                }
            }
            return  result;
        }
        #endregion IsContainKey


    }
}
