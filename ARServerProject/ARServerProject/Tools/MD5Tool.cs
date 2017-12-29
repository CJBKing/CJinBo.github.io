using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ARServerProject.Tools
{
    public class MD5Tool
    {
        public static string GetMD5Ret(string str)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byteArray = md5.ComputeHash(byteArray);
            string ret = "";
            for (int i = 0; i < byteArray.Length; i++)
            {
                ret += Convert.ToString(byteArray[i], 16).PadLeft(2,'0');
            }
            return ret;
        }
    }
}
