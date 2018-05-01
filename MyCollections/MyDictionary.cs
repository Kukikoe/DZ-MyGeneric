using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    /// <summary>
    /// Dmo class impliment Dictionary
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary</typeparam>
    public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
    {
        #region inner classes
        class DictionaryItem
        {
            public KeyValuePair<TKey, TValue> _pair;
            public DictionaryItem _parent;
            public DictionaryItem _left;
            public DictionaryItem _right;
            public DictionaryItem(TKey key, TValue value, DictionaryItem parent = null, DictionaryItem left = null, DictionaryItem right = null)
            {
                _pair = new KeyValuePair<TKey, TValue>(key, value);
                _parent = parent;
                _left = left;
                _right = right;
            }
            public DictionaryItem(KeyValuePair<TKey, TValue> pair, DictionaryItem parent = null, DictionaryItem left = null, DictionaryItem right = null)
            {
                _pair = pair;
                _parent = parent;
                _left = left;
                _right = right;
            }
        }
        #endregion

        #region fields
        private DictionaryItem _root = null;
        private bool _allowDublicateKeys;
        private int _count = 0;
        private List<TKey> keysList = new List<TKey>();
        private List<TValue> keysValue = new List<TValue>();
        #endregion

        #region properties and indexer
        public ICollection<TKey> Keys => GetKeys();

        public ICollection<TValue> Values => GetValues();

        public int Count { get => _count; }

        public bool IsReadOnly { get => false; }

        public TValue this[TKey key]
        {
            get => this.First(x => x.Key.Equals(key)).Value;
            
            set
            {
                Add(key, value);
            }
        }
        #endregion

        #region ctors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MyDictionary()
        {

        }

        public MyDictionary(bool allowDublicateKeys = false)
        {
            _allowDublicateKeys = allowDublicateKeys;
        }
        /// <summary>
        /// Initializes a new inctance of MyDictionary class that contains elements copied from the specefied IDictionary
        /// </summary>
        /// <param name="dict">IDictionary collection</param>
        public MyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            foreach (var d in dictionary)
            {
                Add(d);
            }
        }
        /// <summary>
        /// Initializes a new inctance of MyDictionary class from specified array KeyValuePair
        /// </summary>
        /// <param name="pairs">Pair from Key and Value</param>
        public MyDictionary(params KeyValuePair<TKey, TValue>[] pairs)
        {
            foreach (var p in pairs)
            {
                Add(p);
            }
        }
        #endregion

        #region private methods
        private List<TKey> GetKeys()
        {
            keysList.Clear();
            foreach (var x in this)
            {
                keysList.Add(x.Key);
            }
            return keysList;
        }

        private List<TValue> GetValues()
        {
            keysValue.Clear();
            foreach (var x in this)
            {
                keysValue.Add(x.Value);
            }
            return keysValue;
        }

        private void Add(KeyValuePair<TKey, TValue> pair, DictionaryItem item)
        {
            //если повторяются ключи
            if (!_allowDublicateKeys && pair.Key.CompareTo(item._pair.Key) == 0)
            {
                item._pair = pair;
            }
            //go to left
            else if (pair.Key.CompareTo(item._pair.Key) < 0)
            {
                if (item._left == null)
                {
                    item._left = new DictionaryItem(pair, item);
                    ++_count;
                }
                else
                {
                    Add(pair, item._left);
                }
            }
            else //go to right 
            {
                if (item._right == null)
                {
                    item._right = new DictionaryItem(pair, item);
                    ++_count;
                }
                else
                {
                    Add(pair, item._right);
                }
            }
        }
        /// <summary>
        /// Private method that remove item from the tree
        /// </summary>
        /// <param name="item">Item that removed</param>
        /// <param name="parent">Parent of the item</param>
        private void RemoveItem(DictionaryItem item, DictionaryItem parent)
        {
            if (item._left == null && item._right == null)
            {
                RemoveItemWithoutChildren(item, parent);
            }
            else if (item._left != null && item._right != null)
            {
                RemoveItemWithBothChildren(item, parent);
            }
            else
            {
                RemoveItemWithOneChild(item, parent);
            }
            --_count;
        }
        /// <summary>
        /// Method that remove item without children
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parent"></param>
        private void RemoveItemWithoutChildren(DictionaryItem item, DictionaryItem parent)
        {
            if (item == _root)
            {
                _root = null;
                return;
            }
            if (parent._left == item)
            {
                parent._left = null;
            }
            else
            {
                parent._right = null;
            }
        }
        /// <summary>
        /// Method that remove item with one child
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parent"></param>
        private void RemoveItemWithOneChild(DictionaryItem item, DictionaryItem parent)
        {
            DictionaryItem succ = item._right;
            DictionaryItem succparent = item;
            if (item._right != null)
            {
                while (succ._right != null)
                {
                    succparent = succ;
                    succ = succ._right;
                }
            }
            else
            {
                succ = item._left;
                while (succ._right != null)
                {
                    succparent = succ;
                    succ = succ._right;
                }
            }
            //replacevalue deleted node and successor
            item._pair = succ._pair;

            //delete successor 
            succparent._right = succ._left;
        }
        /// <summary>
        /// Method that remove item with both children
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parent"></param>
        private void RemoveItemWithBothChildren(DictionaryItem item, DictionaryItem parent)
        {
            //Find successor Node
            DictionaryItem success = item._right;
            DictionaryItem successParent = item;
            while (success._left != null)
            {
                successParent = success;
                success = success._left;
            }

            //replace value deleted node and successor
            item._pair = success._pair;

            //delete successor
            successParent._left = success._right;
        }
        /// <summary>
        /// Return the list of the items of the dictionary
        /// </summary>
        /// <returns></returns>
        private List<DictionaryItem> GetListItems()
        {
            List<DictionaryItem> resList = new List<DictionaryItem>();
            DictionaryItem item = _root;              //if item - null, get him _root of tree
            Stack<DictionaryItem> itemStack = new Stack<DictionaryItem>();
            while (item != null || itemStack.Count != 0)
            {
                if (itemStack.Count != 0)
                {
                    item = itemStack.Pop();
                    //this is my action
                    resList.Add(item);

                    if (item._right != null)
                    {
                        item = item._right;
                    }
                    else
                    {
                        item = null;
                    }
                }
                while (item != null)
                {
                    itemStack.Push(item);
                    item = item._left;
                }
            }
            return resList;
        }

        private IEnumerator<DictionaryItem> GetTreeItemEnumerator(DictionaryItem item)
        {
          // Stack<DictionaryItem> itemStack = new Stack<DictionaryItem>();
            Queue<DictionaryItem> itemStack = new Queue<DictionaryItem>();
            while (item != null || itemStack.Count != 0)
            {
                if (itemStack.Count != 0)
                {
                    //извлекает елемент из стека
                    //item = itemStack.Pop();
                    item = itemStack.Dequeue();
                    //this is my action
                    yield return item;
                    if (item._right != null)
                    {
                        item = item._right;
                    }
                    else
                    {
                        item = null;
                    }
                }
                while (item != null)
                {
                    //put
                    //itemStack.Push(item);
                    itemStack.Enqueue(item);
                    item = item._left;
                }
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Adds an element with the provided key and value to the MyDictionary
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add</param>
        /// <param name="value">The object to use as the value of the element to add</param>
        public void Add(TKey key, TValue value)
        {
            if (_root == null)
            {
                _root = new DictionaryItem(key, value);
                ++_count;
            }
            else
            {
                Add(new KeyValuePair<TKey, TValue>(key, value), _root);
            }
        }
        /// <summary>
        /// Adds an item to the MyDictionary
        /// </summary>
        /// <param name="pair">The object to add</param>
        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            if (_root == null)
            {
                _root = new DictionaryItem(pair);
                ++_count;
            }
            else
            {
                //Call a private method
                Add(pair, _root);
            }
        }
        /// <summary>
        /// Removes all items from the MyDictionary
        /// </summary>
        public void Clear()
        {
            _root = null;
            _count = 0;           
        }
        /// <summary>
        /// Determines whether the MyDictionary contains a specific value
        /// </summary>
        /// <param name="item">The object to locate in the MyDictionary</param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            foreach (var x in this)
            {
                if (x.Key.Equals(item.Key) && x.Value.Equals(item.Value))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Determines whether the MyDictionaty contains an element with the specified key
        /// </summary>
        /// <param name="key">The key to locate in the MyDictionary</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            foreach (var x in this)
            {
                if (x.Key.Equals(key))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Copies the elements of the MyDictionary to an Array, starting at a particular Array index
        /// </summary>
        /// <param name="array">Array where the element would copied</param>
        /// <param name="arrayIndex">Index of the array</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if(arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("");
            }
            if(array == null)
            {
                throw new ArgumentNullException("");
            }
            if(_count > array.Length - arrayIndex)
            {
                throw new ArgumentException("");
            }
            using (IEnumerator<DictionaryItem> e = GetTreeItemEnumerator(_root))
            {
                while (e.MoveNext())
                {
                    array[arrayIndex++] = e.Current._pair;
                }
            }
        }
 
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            using (IEnumerator<DictionaryItem> e = GetTreeItemEnumerator(_root))
            {
                while (e.MoveNext())
                {
                    yield return e.Current._pair;
                }
            }
        }
        /// <summary>
        /// Removes the element with the specified key from the MyDictionary
        /// </summary>
        /// <param name="key">The key of the element to remove</param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("");
            }
            List<DictionaryItem> listItems = GetListItems();
            if (ContainsKey(key))
            {
                RemoveItem(listItems.First(x => x._pair.Key.Equals(key)),
                    listItems.First(x => x._pair.Key.Equals(key))._parent);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Removes the first occurrence of a specific object from the MyDictionary
        /// </summary>
        /// <param name="item">The object to remove</param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            List<DictionaryItem> listItems = GetListItems();
            if (Contains(item))
            {
                RemoveItem(listItems.First(x => x._pair.Equals(item)),
                    listItems.First(x => x._pair.Equals(item))._parent);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the value associated with the specified key
        /// </summary>
        /// <param name="key">The key whose value to get</param>
        /// <param name="value">The value associated with the specified key</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("");
            }
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else
            {
                value = default(TValue);
            }
            return false;
        }
        #endregion

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
