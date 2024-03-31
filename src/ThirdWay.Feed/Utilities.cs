using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ThirdWay.Data.Model;

namespace ThirdWay.Feed
{
    internal class Utilities
    {
        public static string GetHashFromString(string str)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();

            foreach (var b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
