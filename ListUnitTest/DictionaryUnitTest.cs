using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCollections;

namespace DictUnitTest
{
    [TestClass]
    public class DictionaryUnitTest
    {
        public void MyDictionaryCtorTest()
        {
            MyDictionary<int, string> dict = new MyDictionary<int, string>();
            Assert.AreNotEqual(null, dict);
            Assert.AreEqual(0, dict.Count);
            Assert.AreEqual(false, dict.IsReadOnly);
        }

        [TestMethod]
        public void MyDictionaryCtorTest_2()
        {
            KeyValuePair<int, string>[] pairs =
                {
                    new KeyValuePair<int, string>(50,"Vasya"),
                    new KeyValuePair<int, string>(38,"Stepan"),
                    new KeyValuePair<int, string>(77,"Lilo"),
                    new KeyValuePair<int, string>(13,"Stich"),
                    new KeyValuePair<int, string>(20,"Alladin"),
                    new KeyValuePair<int, string>(90,"Gaben")
                };
            MyDictionary<int, string> dict = new MyDictionary<int, string>(pairs);
            Assert.AreEqual(6, dict.Count);
        }

        [TestMethod]
        public void MyDictionaryAddTest()
        {
            MyDictionary<int, string> dict = new MyDictionary<int, string>();
            Assert.AreNotEqual(null, dict);
            Assert.AreEqual(0, dict.Count);
            dict.Add(10, "Stepan");
            Assert.AreEqual(1, dict.Count);

            for (int i = 0; i < 100; ++i)
            {
                dict.Add(i, " ");
            }
            Assert.AreEqual(100, dict.Count); //100 because Dictionary do not support the same Keys
        }

        [TestMethod]
        public void MyDictionaryAddTest_2()
        {
            MyDictionary<int, string> dict = new MyDictionary<int, string>();
            Assert.AreNotEqual(null, dict);
            Assert.AreEqual(0, dict.Count);
            dict.Add(new KeyValuePair<int, string>(15, "Solo"));
            Assert.AreEqual(1, dict.Count);

            for (int i = 0; i < 100; ++i)
            {
                dict.Add(new KeyValuePair<int, string>(i, "Solo"));
            }
            Assert.AreEqual(100, dict.Count); //100 because Dictionary do not support the same Keys
        }

        [TestMethod]
        public void MyDictionaryClearTest()
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
            Assert.AreEqual(7, dict.Count);
            dict.Clear();
            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void MyDictionaryContainsTest()
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
            Assert.IsTrue(dict.Contains(new KeyValuePair<int, string>(20, "Alladin")));
            Assert.IsTrue(dict.Contains(new KeyValuePair<int, string>(13, "Stich")));
            Assert.IsFalse(dict.Contains(new KeyValuePair<int, string>(18, "Not Contains")));
        }

        [TestMethod]
        public void MyDictionaryContainsKeyTest()
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
            Assert.IsTrue(dict.ContainsKey(77));
            Assert.IsTrue(dict.ContainsKey(20));
            Assert.IsFalse(dict.ContainsKey(55));
        }

        [TestMethod]
        public void MyDictionaryCopyToTest()
        {
            KeyValuePair<int, string>[] check =
               {
                    new KeyValuePair<int, string>(50,"Vasya"),
                    new KeyValuePair<int, string>(38,"Stepan"),
                    new KeyValuePair<int, string>(13,"Stich"),
                    new KeyValuePair<int, string>(77,"Lilo"),
                    new KeyValuePair<int, string>(45,"IceFrog"),
                    new KeyValuePair<int, string>(20,"Alladin"),
                    new KeyValuePair<int, string>(90,"Gaben"),                
                };
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
            KeyValuePair<int, string>[] pair = new KeyValuePair<int, string>[7];
            dict.CopyTo(pair, 0);
            foreach (var x in pair)
            {
                Assert.AreEqual(check, x);
            }
        }

        [TestMethod]
        public void MyDictionaryRemoveTest()
        {
            KeyValuePair<int, string>[] pairs =
                {
                    new KeyValuePair<int, string>(50,"Vasya"),
                    new KeyValuePair<int, string>(38,"Stepan"),
                    new KeyValuePair<int, string>(77,"Lilo"),
                    new KeyValuePair<int, string>(13,"Stich"),
                    new KeyValuePair<int, string>(20,"Alladin"),
                    new KeyValuePair<int, string>(90,"Gaben")
                };
            MyDictionary<int, string> dict = new MyDictionary<int, string>(pairs);
            Assert.AreEqual(6, dict.Count);
            Assert.IsTrue(dict.Remove(new KeyValuePair<int, string>(90, "Gaben")));
            Assert.AreEqual(5, dict.Count);
            Assert.IsFalse(dict.Remove(new KeyValuePair<int, string>(19, "Gaben")));
            Assert.AreEqual(5, dict.Count);
        }

        [TestMethod]
        public void MyDictionaryRemoveTest_2()
        {
            KeyValuePair<int, string>[] pairs =
                {
                    new KeyValuePair<int, string>(50,"Vasya"),
                    new KeyValuePair<int, string>(38,"Stepan"),
                    new KeyValuePair<int, string>(77,"Lilo"),
                    new KeyValuePair<int, string>(13,"Stich"),
                    new KeyValuePair<int, string>(20,"Alladin"),
                    new KeyValuePair<int, string>(90,"Gaben")
                };
            MyDictionary<int, string> dict = new MyDictionary<int, string>(pairs);
            Assert.AreEqual(6, dict.Count);
            Assert.IsTrue(dict.Remove(38));
            Assert.AreEqual(5, dict.Count);
            Assert.IsFalse(dict.Remove(80));
            Assert.AreEqual(5, dict.Count);
        }

        [TestMethod]
        public void MyDictionaryEnumeratorTest()
        {
            KeyValuePair<int, string>[] check =
              {
                    new KeyValuePair<int, string>(50,"Vasya"),
                    new KeyValuePair<int, string>(38,"Stepan"),
                    new KeyValuePair<int, string>(13,"Stich"),
                    new KeyValuePair<int, string>(77,"Lilo"),
                    new KeyValuePair<int, string>(45,"IceFrog"),
                    new KeyValuePair<int, string>(20,"Alladin"),
                    new KeyValuePair<int, string>(90,"Gaben"),
                };
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
           // var sortedPairs = pairs.OrderBy(x => x.Key).ToArray();
            //int counter = 0;
            foreach (var x in dict)
            {
                Assert.AreEqual(check, x);
            }
        }
    }
}
