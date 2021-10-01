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
    public abstract class CString
    {
        public const string Empty = "";
        public const string Space = " ";

        protected static readonly dynamic SeparatorsWords;
        protected static readonly dynamic XmlReplaceWords;

        #region Static Constructor
        static CString()
        {
            XmlReplaceWords = CreateXmlReplaceWords();
            SeparatorsWords = CreateSeparatorsWords();
        }
        #endregion Static Constructor
        #region Constructor
        public CString()
        {
        }
        #endregion Constructor

        #region CreateXmlReplaceWords
        private static dynamic CreateXmlReplaceWords()
        {
            dynamic result = null;
            result = new Dictionary<string, string>()
            {
                {"&amp;","&"},
                {"&quot;"," "},
            };
            return result;
        }
        #endregion CreateXmlReplaceWords
        #region SeparatorsWords
        public static dynamic CreateSeparatorsWords()
        {
            dynamic result = null;
            result = new char[]
            { ' ',',','.',';',':','\n','\t','\r','\a','(',')','[',']','{','}','=','|' };
            return result;
        }
        #endregion SeparatorsWords

        #region IsString
        public static bool IsString(dynamic value)
        {
            return ((value is string) || (value is String));
        }
        #endregion IsString
        #region Trim
        public static string Trim(dynamic str)
        {
            dynamic result = "";
            if (IsNullOrEmpty(str)) return result;
            result = str.Trim();
            return result;
        }
        #endregion Trim
        #region IsNullOrEmpty
        public static bool IsNullOrEmpty(dynamic s)
        {            
            if (s == null) return true;
            if (!(s is string)) return false;
            return string.IsNullOrEmpty((string)s);            
        }
        #endregion IsNullOrEmpty
        #region IsNotEmpty
        public static bool IsNotEmpty(dynamic value)
        {
            return !(IsNullOrEmpty(value));
        }
        #endregion IsNotEmpty
        #region ToByteArray
        public static byte[] ToByteArray(string s,Encoding encoding = null)
        {
            dynamic result = null;
            encoding = (encoding == null) ? Encoding.Default : encoding;
            result = encoding.GetBytes(s);
            return result;
        }
        #endregion ToByteArray
        #region Split
        public static dynamic Split
            (
                dynamic s,
                char[] chars,
                bool isRemoveNullStrings = true,
                int max = 1000
            )
        {
            dynamic result = null;
            if (s == null) return result;
            dynamic options = StringSplitOptions.RemoveEmptyEntries;
            if (!isRemoveNullStrings) options = StringSplitOptions.None;
            result = (s as string).Split(chars, max, options);
            return result;
        }

        public static dynamic Split
            (
                dynamic s,
                char ch,
                 bool isRemoveNullStrings = true,
                int max = 1000
            )
        {
            dynamic result = null;
            result = Split(s, new char[] { ch }, isRemoveNullStrings, max);
            return result;
        }
        #endregion Split
        
        #region GetPosition
        protected static dynamic GetPosition(dynamic positions, int index)
        {
            dynamic result = null;
            foreach (var position in positions)
            {
                if ((index >= position.Start) && (index <= position.End))
                {
                    result = position;
                    break;
                }
            }
            return result;
        }
        #endregion GetPosition
        #region StartsWith
        public static bool StartsWith(dynamic s, dynamic value)
        {
            int i = -1;
            dynamic result = false;
            if (!(IsString(s))) return result;
            if ((IsNullOrEmpty(s)) || (IsNullOrEmpty(value))) return false;
            i = (s as string).IndexOf(value);
            if (i == 0) result = true;
            return result;
        }
        #endregion StartsWith
        #region IsContain
        public static bool IsContain(dynamic s, dynamic value)
        {
            dynamic result = false;
            if ((IsNullOrEmpty(s)) || (IsNullOrEmpty(value))) return false;

            return  result;
        }
        #endregion IsContain

        #region Word
        public static dynamic Word(dynamic s, int n)
        {
            string[] cache = null;
            dynamic result = null;
            result = Word((string)s, n, ref cache);
            return result;
        }

        public static dynamic Word(dynamic s, int n, ref string[] cache)
        {
            int count = 0;
            dynamic items = null;
            dynamic result = null;
            if (CString.IsNullOrEmpty(s)) return result;
            if (cache == null)
                cache = Split((string)s, SeparatorsWords);
            items = cache;
            count = (items as IList).Count;
            if (count == 0) return result;
            if (n >= count) return result;
            result = items[n];
            return result;
        }
        #endregion Word
        #region NewLine
        public static dynamic NewLine()
        {
            dynamic result = "\t\n";
            return result;
        }
        #endregion NewLine
        #region NormalizeWord
        public static string NormalizeWord(string word)
        {
            dynamic result = "";
            if (IsNullOrEmpty(word)) return "";
            result = word.Substring(0, 1).ToUpper();
            result += word.Substring(1).ToLower();
            return result;
        }
        #endregion NormalizeWord
    }
}
