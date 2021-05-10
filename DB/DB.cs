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
            try
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
            catch (Exception)
            {
                return "Ошибка";
            }            
        }

        //анимация
        public static void ShowLoader()
        {
            try
            {
                Loader loader = new Loader();
                loader.ShowDialog();
                loader.Close();
            }
            catch (Exception)
            {
            }            
        }
    }
}