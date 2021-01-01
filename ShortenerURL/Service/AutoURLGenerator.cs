using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public static class AutoURLGenerator
    {
        public static string ShortUrlGenerator(int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var str = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return str;
        }
    }
}
