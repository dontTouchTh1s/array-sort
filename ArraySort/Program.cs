using System;
using System.Collections.Generic;

namespace ArraySort
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var randomArray = new int[10000];
            var randomNumbers = new Random(1);
            for (var i = 0; i < 10000; i++) randomArray[i] = randomNumbers.Next();

            var sorting = BobbleSort(randomArray);
            Console.WriteLine("Bubble sort: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);

            sorting = ControlledBobbleSort(randomArray);
            Console.WriteLine("Controller bubble sort: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);

            sorting = ExchangeSort(randomArray);
            Console.WriteLine("Exchange sort: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);

            sorting = SelectionSort(randomArray);
            Console.WriteLine("Selection sort: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);

            sorting = MergeSort(randomArray);
            Console.WriteLine("Merge sort: compare: {0}, move: {1}, Insert: {2}", sorting.Compare, sorting.Move,
                sorting.Insert);

            sorting = InsertionSort(randomArray);
            Console.WriteLine("Insertion sort: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);

            sorting = ListSort(randomArray);
            Console.WriteLine("List sort: compare: {0}, insert: {1} ", sorting.Compare, sorting.Insert);

            sorting = QuickSort(randomArray, 0, randomArray.Length - 1);
            Console.WriteLine("Quick sort: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);
        }

        private static Sorting BobbleSort(int[] mainArray)
        {
            var array = new int[mainArray.Length];
            mainArray.CopyTo(array, 0);
            var sorting = new Sorting();
            for (var i = 0; i < array.Length - 1; i++)
            for (var j = 0; j < array.Length - i - 1; j++)
            {
                sorting.Compare++;
                if (array[j] > array[j + 1])
                {
                    sorting.Move++;
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }

            return sorting;
        }

        private static Sorting ControlledBobbleSort(int[] mainArray)
        {
            var array = new int[mainArray.Length];
            mainArray.CopyTo(array, 0);
            var sorting = new Sorting();
            for (var i = 0; i < array.Length - 1; i++)
            {
                var sorted = true;
                for (var j = 0; j < array.Length - i - 1; j++)
                {
                    sorting.Compare++;
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        sorted = false;
                        sorting.Move++;
                    }
                }

                if (sorted) break;
            }

            return sorting;
        }

        private static Sorting ExchangeSort(int[] mainArray)
        {
            var array = new int[mainArray.Length];
            mainArray.CopyTo(array, 0);
            var sorting = new Sorting();
            for (var i = 0; i < array.Length - 1; i++)
            for (var j = i + 1; j < array.Length; j++)
            {
                sorting.Compare++;
                if (array[i] > array[j])
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    sorting.Move++;
                }
            }

            return sorting;
        }

        private static Sorting SelectionSort(int[] mainArray)
        {
            var array = new int[mainArray.Length];
            mainArray.CopyTo(array, 0);
            var sorting = new Sorting();
            for (var i = 0; i < array.Length - 1; i++)
            {
                var min = i;
                for (var j = i + 1; j < array.Length; j++)
                {
                    sorting.Compare++;
                    if (array[j] < array[min])
                        min = j;
                }

                sorting.Move++;
                (array[i], array[min]) = (array[min], array[i]);
            }

            sorting.Array = array;
            return sorting;
        }

        private static Sorting MergeSort(int[] mainArray)
        {
            if (mainArray.Length == 1) return new Sorting { Array = mainArray };
            var array01 = new int[mainArray.Length / 2];
            var array02 = new int[mainArray.Length - array01.Length];
            var array = new int[mainArray.Length];
            for (var i = 0; i < mainArray.Length; i++)
                if (i < array01.Length)
                    array01[i] = mainArray[i];
                else
                    array02[i - array01.Length] = mainArray[i];
            // Sort split array
            var sorting = new Sorting();
            var a1 = MergeSort(array01);
            a1.Array.CopyTo(array01, 0);
            sorting.Compare = a1.Compare;
            sorting.Move = a1.Move;
            var a2 = MergeSort(array02);
            a2.Array.CopyTo(array02, 0);
            sorting.Compare += a2.Compare;
            sorting.Move += a2.Move;
            var counter = 0;
            for (var i = 0; i < array01.Length; i++)
            for (var j = counter - i; j < array02.Length; j++)
            {
                sorting.Compare++;
                sorting.Insert++;
                if (array01[i] < array02[j])
                {
                    array[counter] = array01[i];
                    counter++;
                    if (counter == array.Length - 1) array[counter] = array02[j];
                    break;
                }

                array[counter] = array02[j];
                counter++;
                if (counter == array.Length - 1) array[counter] = array01[i];
            }

            sorting.Array = array;
            return sorting;
        }

        private static Sorting InsertionSort(int[] mainArray)
        {
            var array = new int[mainArray.Length];
            mainArray.CopyTo(array, 0);
            var sorting = new Sorting();
            for (var i = 0; i < mainArray.Length - 1; i++)
            for (var j = i + 1; j > 0; j--)
            {
                sorting.Compare++;
                if (array[j - 1] > array[j])
                {
                    (array[j], array[j - 1]) = (array[j - 1], array[j]);
                    sorting.Move++;
                }
            }

            return sorting;
        }

        private static Sorting ListSort(int[] mainArray)
        {
            var list = new List<int>();
            var sorting = new Sorting
            {
                List = list
            };
            for (var i = 0; i < mainArray.Length; i++)
            {
                var index = 0;
                for (var j = 0; j < list.Count; j++)
                {
                    sorting.Compare++;
                    if (mainArray[i] > list[j])
                    {
                        index = j + 1;
                        continue;
                    }

                    index = j;
                    break;
                }

                sorting.Insert++;
                list.Insert(index, mainArray[i]);
            }

            return sorting;
        }

        private static Sorting QuickSort(int[] mainArray, int leftIndex, int rightIndex)
        {
            var array = new int[mainArray.Length];
            mainArray.CopyTo(array, 0);
            var sorting = new Sorting();
            var pivot = array[leftIndex];
            var i = leftIndex;
            var j = rightIndex;
            while (i <= j)
            {
                sorting.Compare++;
                while (array[i] < pivot) i++;
                sorting.Compare++;
                while (array[j] > pivot) j--;
                if (i <= j)
                {
                    sorting.Move++;
                    (array[i], array[j]) = (array[j], array[i]);
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSort(array, leftIndex, j);
            if (i < rightIndex)
                QuickSort(array, i, rightIndex);
            sorting.Array = array;
            return sorting;
        }
    }

    public class Sorting
    {
        public int[] Array;
        public int Compare;
        public int Insert;
        public List<int> List;
        public int Move;
    }
}