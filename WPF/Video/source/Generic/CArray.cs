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
    public class CArray
    {

        #region Count
        public static dynamic Count(dynamic array)
        {
            dynamic result  = 0;
            try
            {
                if (array == null) return result;
                if (array is IList)
                    return (array as IList).Count;
                if (array is IList<dynamic>)
                    return (array as IList<dynamic>).Count;
                if (array is ArrayList)
                    return (array as ArrayList).Count;
                if (array is IDictionary)
                    return (array as IDictionary).Count;
                if (array is IEnumerable<dynamic>)
                    return (array as IEnumerable<dynamic>).Count();
                return array.Length;
            }
            catch(Exception ex)
            {
                CException.PrintDebugException(ex);
                throw;
            }
        }
        #endregion Count
        #region GetArrayValue
        public static dynamic GetArrayValue(dynamic array,int index,bool isException = false)
        {
            dynamic result = null;
            if ((array != null) && (array.Length > index))
                result = array[index];
            else
            {
                if (isException)
                {
                    if (array == null)
                        throw new ArgumentNullException();
                    if (array.Length > index)
                        throw new IndexOutOfRangeException();
                }
            }
            return  result;
        }
        #endregion GetArrayValue
        #region IsEmpty
        public static bool IsEmpty(dynamic array)
        {
            return (Count(array) > 0) ? false : true;
        }
        #endregion IsEmpty

    }
}
