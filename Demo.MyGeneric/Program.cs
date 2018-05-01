using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCollections;

namespace Demo.MyGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyValuePair<int, string>[] pairs =
              {
                    new KeyValuePair<int, string>(50,"Vasya"),
                    new KeyValuePair<int, string>(38,"Stepan"),
                    new KeyValuePair<int, string>(77,"Lilo"),
                    new KeyValuePair<int, string>(13,"Stich"),
                    new KeyValuePair<int, string>(20,"Alladin"),
                    new KeyValuePair<int, string>(90,"Gaben"),
                    new KeyValuePair<int, string>(45,"IceFrog")
                };
            MyDictionary<int, string> dict = new MyDictionary<int, string>(pairs);
            string str;
            dict.TryGetValue(50, out str);
            Console.WriteLine(str);
            //KeyValuePair<int, string>[] pair = new KeyValuePair<int, string>[7];
            //dict.CopyTo(pair, 0);
            //foreach(KeyValuePair<int, string> p in pair)
            //{
            //    Console.WriteLine("{0}  {1}", p.Key, p.Value);
            //}

            Console.ReadKey();
        }
    }
}
