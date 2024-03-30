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
            using var algorithm = SHA256.Create();
            var bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();

            foreach (var b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
