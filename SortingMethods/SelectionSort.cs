using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SortingMethods
{
    public interface ISortingObserver<T>
    {
        void Swapped(T[] arr);
    }

    public abstract class SortingMethod<T>
        where T: IComparable<T>
    {
        private readonly IList<ISortingObserver<T>> observers = new List<ISortingObserver<T>>();

        public static void Swap(T[] arr, int index1, int index2)
        {
            var temp = arr[index1];

            arr[index1] = arr[index2];

            arr[index2] = temp;
        }

        public static T[] Shuffle(T[] arr, int numIterations = 0)
        {
            var random = new Random((int)DateTime.Now.ToBinary());

            if (numIterations < 1)
                numIterations = arr.Length;

            for (int i = 0; i < numIterations; i++)
            {
                int j = i % arr.Length;

                int k = random.Next(arr.Length);

                Swap(arr, j, k);
            }

            return arr;
        }

        public abstract T[] Sort(T[] arr);

        public void AddObserver(ISortingObserver<T> observer)
            => observers.Add(observer);

        protected void NotifySwapped(T[] arr)
            => (observers as List<ISortingObserver<T>>)
                .ForEach(obj => obj?.Swapped(arr));
    }

    public class SelectionSort<T> : SortingMethod<T>
        where T: IComparable<T>
    {
        public override T[] Sort(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                (int MinIndex, T MinValue) tuple = (i, arr[i]);

                for (int j = i + 1; j < arr.Length; j++)
                    if (arr[j].CompareTo(tuple.MinValue) < 0)
                        tuple = (j, arr[j]);

                if (i != tuple.MinIndex)
                {
                    var temp = arr[i];

                    arr[i] = tuple.MinValue;

                    arr[tuple.MinIndex] = temp;

                    NotifySwapped(arr);
                }

                Task.Delay(8).Wait();
            }

            return arr;
        }
    }

    public class InsertionSort<T> : SortingMethod<T>
        where T : IComparable<T>
    {
        public override T[] Sort(T[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                T obj = arr[i];

                int j = i - 1;

                //int? index = null;

                int? index = null;

                bool isGreaterThan = false;

                while (!isGreaterThan && j >= 0)
                {
                    isGreaterThan = obj.CompareTo(arr[j]) > 0;
                    
                    if (!isGreaterThan)
                        index = j;

                    j--;
                }

                if (index.HasValue)
                {
                    for (j = i; j > index.Value; j--)
                        arr[j] = arr[j - 1];

                    arr[index.Value] = obj;

                    NotifySwapped(arr);
                }

                Task.Delay(8).Wait();
            }

            return arr;
        }
    }

    public class BubbleSort<T> : SortingMethod<T>
        where T : IComparable<T>
    {
        public override T[] Sort(T[] arr)
        {
            bool isSorted = false;

            for (int i = 0; !isSorted && i < arr.Length; i++)
            {
                bool hasSwapped = false;

                for (int j = 1; !isSorted && j < arr.Length - i; j++)
                {
                    T lower = arr[j - 1];
                    T higher = arr[j];

                    if (lower.CompareTo(higher) > 0)
                    {
                        Swap(arr, j, j - 1);

                        hasSwapped = true;
                    }
                }

                isSorted = !hasSwapped;

                Task.Delay(8).Wait();
            }

            return arr;
        }
    }
}
