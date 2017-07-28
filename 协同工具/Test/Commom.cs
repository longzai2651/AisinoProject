using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assist
{
    class Commom
    {
        private static string ChangeStr(string str)
        {
            return Utf8ToDefault(DefaultToUtf8(str));
        }
        private static string DefaultToUtf8(string str)
        {
            //byte[] temp = Encoding.UTF8.GetBytes(str);
            byte[] temp = Encoding.Default.GetBytes(str);
            //byte[] temp1 = Encoding.Convert(Encoding.UTF8, Encoding.Default, temp);
            byte[] temp1 = Encoding.Convert(Encoding.Default, Encoding.UTF8, temp);
            string result = Encoding.UTF8.GetString(temp1);
            return result;
        }

        private static string Utf8ToDefault(string str)
        {
            byte[] temp = Encoding.UTF8.GetBytes(str);
            //byte[] temp = Encoding.Default.GetBytes(str);
            //byte[] temp1 = Encoding.Convert(Encoding.Default, Encoding.UTF8, temp);
            byte[] temp1 = Encoding.Convert(Encoding.UTF8, Encoding.Default, temp);
            string result = Encoding.Default.GetString(temp1);
            return result;
        }
    }
}
