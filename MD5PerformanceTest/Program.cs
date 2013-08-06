using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MD5PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string resultStats = string.Empty;

            Console.WriteLine("Create value base.");

            List<List<string>> valList = new List<List<string>>();

            List<string> vals1000 = new List<string>();
            for (int i = 0; i < 1000; i++)
                vals1000.Add(i.ToString());
            valList.Add(vals1000);

            List<string> vals10000 = new List<string>();
            for (int i = 0; i < 10000; i++)
                vals10000.Add(i.ToString());
            valList.Add(vals10000);

            List<string> vals50000 = new List<string>();
            for (int i = 0; i < 50000; i++)
                vals50000.Add(i.ToString());
            valList.Add(vals50000);

            List<string> vals100000 = new List<string>();
            for (int i = 0; i < 100000; i++)
                vals100000.Add(i.ToString());
            valList.Add(vals100000);

            List<string> vals1000000 = new List<string>();
            for (int i = 0; i < 1000000; i++)
                vals1000000.Add(i.ToString());
            valList.Add(vals1000000);

            List<string> vals10000000 = new List<string>();
            for (int i = 0; i < 10000000; i++)
                vals10000000.Add(i.ToString());
            valList.Add(vals10000000);

            Console.WriteLine("Values created");
           


            Stopwatch sw = new Stopwatch();
            string hash = string.Empty;

            foreach (var vals in valList)
            {
                sw.Restart();
                MD5 hasher = MD5.Create();
                hash = string.Empty;
                foreach (string s in vals)
                {
                    hash = GetMd5Hash(hasher, s);
                }
                sw.Stop();
                resultStats += ".NET-Method | " + vals.Count + " items | " + sw.Elapsed.TotalMilliseconds + " ms\r\n";
                
            }


            Console.WriteLine("\r\nStart manual implementation...");
            foreach (var vals in valList)
            {
                sw.Restart();
                hash = string.Empty;
                ManualMD5.MD5 md = new ManualMD5.MD5();
                foreach (string s in vals)
                {
                    md.Value = s;
                    hash = md.FingerPrint;
                }
                sw.Stop();
                resultStats += ".Manual-Method | " + vals.Count + " items | " + sw.Elapsed.TotalMilliseconds + " ms\r\n";
            }
            Console.Write(resultStats);           
            Console.ReadKey();

        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));           
         
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }
    }


}
