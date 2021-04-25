using CourseProjectWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.DB
{
    class DB
    {
        public static string Hash(string input)
        {
            byte[] hash = Encoding.ASCII.GetBytes(input);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);            
            string output = "";
            foreach (var b in hashenc)
            {
                output += b.ToString("x2");
            }
            return output;
        }

        //анимация
        public static void ShowLoader()
        {
            Loader loader = new Loader();
            loader.ShowDialog();
            loader.Close();
        }
    }
}
