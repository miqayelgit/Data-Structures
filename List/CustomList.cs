using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures.List
{
    public class CustomList<T> : IEnumerable<T>
    {
        private T[] _mainArr;
        private int _count = 0;
        private int _capacity = 0;
        private int _currentIndex = -1;

        public CustomList(int capacity = 8)
        {
            _mainArr = new T[capacity];
            _capacity = capacity;
        }

        public T this[int index]
        {
            get => _mainArr[index];
            set => _mainArr[index] = value;
        }

        public T Current => _mainArr[++_currentIndex]!;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            if (_currentIndex < _count)
            {
                return true;
            }
            else
            {
                ResetIndex();
            }

            return false;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public void Add(T value)
        {
            if (_count >= _mainArr.Length)
            {
                _mainArr = ResizeArray(_mainArr);
            }
            _mainArr[_count] = value;
            _count++;

        }
        public void AddRange(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Add(arr[i]);
            }
        }

        public void Remove(T value)
        {
            T[] newArr = new T[_capacity];
           
            int j = 0;
            bool isItemFoundInList = false;

            for (int i = 0; i < _capacity; i++)
            {
                if (_mainArr[i] is not null && !_mainArr[i]!.Equals(value) || isItemFoundInList)
                {
                    newArr[j++] = _mainArr[i];
                }
                else
                {
                    isItemFoundInList = true;
                    _count--;
                }
            }

            if (isItemFoundInList)
            {
                _mainArr = newArr;
            }
        }
        public void RemoveRange(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Remove(arr[i]);
            }
        }

        public void RemoveAt(int index)
        {
            T removableValue = _get(index);
            Remove(removableValue);
        }
        private T _get(int index)
        {
            return _mainArr[index];
        }

        public void Insert(int index, T value)
        {
            if (index > _count)
            {
                throw new IndexOutOfRangeException();
            }

            T[] newArr = new T[_capacity];

            if (++_count > _capacity)
            {
                newArr = ResizeArray(newArr);
            }

            int j = 0;
            
            for (int i = 0; i < _count; i++)
            {
                if (i == index)
                {
                    newArr[i] = value;
                }

                else
                {
                    newArr[i] = _mainArr[j++];
                }  
            }

            _mainArr = newArr;
        }

        private T[] ResizeArray(T[] initialArray)
        {
            T[] newArray = new T[initialArray.Length * 2];
            _capacity = newArray.Length;

            if (initialArray.Length >= newArray.Length)
            {
                throw new Exception("New Array's array Length should be bigger than initial's.");
            }

            for (int i = 0; i < initialArray.Length; i++)
            {
                newArray[i] = initialArray[i];
            }

            return newArray;
        }

        private void ResetIndex()
        {
            _currentIndex = -1;
        }

    }
}
