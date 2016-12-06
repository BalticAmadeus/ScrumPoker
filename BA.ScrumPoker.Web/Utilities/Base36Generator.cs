using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BA.ScrumPoker.Utilities
{
    public static class Base36Generator
    {
        private const string Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static string GenerateString(int length, Random rng)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(Chars[rng.Next(0,36)]);
            }

            return sb.ToString();
        }
    }
}