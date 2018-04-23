using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    public class MyList<T> : IList<T>, IEnumerator<T>, IEnumerator
    {
        #region fields
        private T[] _arr;
        private int _count;
        private int _current_pos;
        #endregion

        #region properties and indexator
        public T this[int index]
        {
            get
            {
                return _arr[index];
            }
            set
            {
                if (index < 0 || index >= _count) throw new ArgumentOutOfRangeException("");
                _arr[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T Current
        {
            get
            {
                return _arr[_current_pos];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

       // int IEnumerator<T>.Current => throw new NotImplementedException();
        #endregion

        #region ctors
        public MyList(int capacity = 10)
        {
            _arr = new T[capacity];
            _count = 0;
            _current_pos = -1;
        }

        public MyList(ICollection source)
        {
            _arr = new T[_count = source.Count];

            _current_pos = -1;
        }
        #endregion

        public void Add(T value)
        {
            if (_count == _arr.Length)
            {
                T[] tmp = new T[_arr.Length * 2];
                for (int j = 0; j < _arr.Length; ++j)
                {
                    tmp[j] = _arr[j];
                }
                _arr = tmp;
            }
            _arr[_count++] = value;
        }

        public void Clear()
        {
            _count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_arr[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(int i = 0, j = arrayIndex; i < _count; ++i, ++j)
            {
                array[j] = _arr[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)_arr).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_arr[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (_arr.Length == _count)
            {
                T[] tmp = new T[_count * 2];
                for (int i = 0, j = 0; i < _count; ++i, ++j)
                {
                    if (i == index)
                    {
                        tmp[index++] = item;
                        ++j;
                    }
                    else
                    {
                        tmp[j] = _arr[i];
                    }
                }
                _arr = tmp;
                ++_count;
            }
            else
            {
                for (int i = _count; i > index; --i)
                {
                    _arr[i] = _arr[i - 1];
                }
                _arr[index] = item;
                ++_count;
            }
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_arr[i].Equals(item))
                {
                    for (int j = i; j < _count; ++j)
                    {
                        _arr[j] = _arr[j + 1];
                    }
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < _count; ++i)
            {
                _arr[i] = _arr[i + 1];
            }
           
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)_arr).GetEnumerator();
        }

        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            ++_current_pos;
            return _current_pos < _count;
        }

        public void Reset()
        {
            _current_pos = -1;
        }
    }
}
