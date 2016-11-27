using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BA.ScrumPoker.Utilities
{
    public static class Base36Generator
    {
        private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GenerateString(int length)
        {
            string result = "";
            Random rd = new Random();

            for (int i = 0; i < length; i++)
            {
                result += Chars[rd.Next(0,36)];
            }

            return result;
        }
    }
}