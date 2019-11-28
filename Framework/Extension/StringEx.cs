using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Extension
{
    public static class StringEx
    {
        /// <summary>
        /// 字符串两端插入字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">字符</param>
        /// <returns></returns>
        public static string Fill(this string str, string c = " ")
        {
            return $"{c}{str}{c}";
        }
    }
}
