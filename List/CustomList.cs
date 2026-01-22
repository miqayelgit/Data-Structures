using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures.List
{
    public class CustomList<T> : IEnumerable, IEnumerator
    {
        private T[] _mainArr;
        private int _count = 0;
        private int _capacity = 0;
        private int _currentIndex = -1;

        public CustomList(int size = 8)
        {
            _mainArr = new T[size];
            _capacity = size;
        }

        object IEnumerator.Current => _mainArr[++_currentIndex];
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        bool IEnumerator.MoveNext()
        {
            if (_currentIndex < _count)
            {
                //IncrementCurrentIndex();
                return true;
            }
            else
            {
                ResetIndex();
            }

            return false;
        }

        void IEnumerator.Reset()
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
            T[] newArr = new T[_mainArr.Length - 1];
            int j = 0;
            bool numberFoundInList = false;

            for (int i = 0; i < _mainArr.Length; i++)
            {
                if (_mainArr[i] is not null && !_mainArr[i].Equals(value))
                {
                    if (j < _mainArr.Length - 1)
                    {
                        newArr[j] = _mainArr[i];
                        j++;
                    }
                }
                else
                {
                    numberFoundInList = true;
                    _count--;
                }
            }

            if (numberFoundInList)
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
            T removableValue = Get(index);
            Remove(removableValue);
        }
        public T Get(int index)
        {
            return _mainArr[index];
        }

        public void Insert(int index, T value)
        {
            if (index > _count)
            {
                throw new IndexOutOfRangeException();
            }
            var newArr = new T[_capacity + 1];
            int j = 0;

            for (int i = 0; i <= _capacity; i++)
            {
                if(i == index)
                {
                    continue;
                }

                newArr[i] = _mainArr[j];
                j++;
            }

            newArr[index] = value;
            _mainArr = newArr;
        }


        public void Print(CustomList<T> list)
        {
            for (int i = 0; i < _mainArr.Length; i++)
            {
                Console.WriteLine(list.Get(i));
            }
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
