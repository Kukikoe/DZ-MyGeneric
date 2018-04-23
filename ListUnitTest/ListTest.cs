using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCollections;

namespace ListUnitTest
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void Ctor1Test()
        {
            MyList<int> list = new MyList<int>();
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(false, list.IsReadOnly);
        }

        [TestMethod]
        public void Ctor2Test()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6 };
            MyList<int> list = new MyList<int>(arr);
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void AddTest()
        {
            MyList<int> list = new MyList<int>(4);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
            list.Add(10);
            Assert.AreEqual(1, list.Count);
            for(int i = 0; i < 100; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(101, list.Count);
        }

        [TestMethod]
        public void ClearTest()
        {
            MyList<int> list = new MyList<int>();
            for (int i = 0; i < 100; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(100, list.Count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void IndexTest()
        {
            int[] arr = { 1, 2, 5, 4, 3, 7, 9 };
            MyList<int> list = new MyList<int>();
            foreach(int val in arr)
            {
                list.Add(val);
            }
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(arr[i], list[i]);
            }
        }

        [TestMethod]
        public void RemoveTest()
        {
            MyList<int> list = new MyList<int>() { 1, 2, 5, 4 };
            MyList<int> list1 = new MyList<int>() { 2, 5, 4 };
            bool value = list.Remove(1);
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void CopyToTest()
        {
            MyList<int> list = new MyList<int>() { 1, 2, 5, 4, 6, 4 };
            int[] arr = new int[6];
            list.CopyTo(arr, 0);

            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(arr[i], list[i]);
            }
        }

        [TestMethod]
        public void IndexOfTest()
        {
            MyList<int> list = new MyList<int>() { 1, 2, 5, 4 };
            var index = list.IndexOf(5);
            Assert.AreEqual(2, index);
        }

        [TestMethod]
        public void ContainsTest()
        {
            MyList<int> list = new MyList<int>() { 1, 2, 5, 4 };
            bool value = list.Contains(5);
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            MyList<int> list = new MyList<int>() { 1, 2, 5, 4 };
            MyList<int> list1 = new MyList<int>() { 2, 5, 4 };
            list.RemoveAt(0);
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], list1[i]);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            MyList<int> list = new MyList<int>() { 1, 2, 5, 4 };
            MyList<int> list1 = new MyList<int>() { 2, 5, 4 };
            list1.Insert(0, 1);
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], list1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTest_1()
        {
            MyList<int> list = new MyList<int>();
            list[-1] = 100;
        }

        [TestMethod]
        public void IEnumerableTest()
        {
            MyList<int> list = new MyList<int>();
            Assert.IsTrue(list.GetEnumerator() is IEnumerator<int>);
        }

    }
}
