using System;

namespace SortingMethods
{
    public class SelectionSort<T>
        where T: IComparable<T>
    {
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

        public T[] Sort(T[] arr)
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
                }
            }

            return arr;
        }
    }
}
