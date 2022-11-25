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
            Console.WriteLine("Bubble: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);

            sorting = ControlledBobbleSort(randomArray);
            Console.WriteLine("Controller bubble: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);
            sorting = ExchangeSort(randomArray);
            Console.WriteLine("Exchange: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);
            sorting = MergeSort(randomArray);
            Console.WriteLine("Merge: compare: {0}, move: {1}, Insert: {2}", sorting.Compare, sorting.Move,
                sorting.Insert);
            sorting = InsertionSort(randomArray);
            Console.WriteLine("Insertion: compare: {0}, move: {1} ", sorting.Compare, sorting.Move);
            sorting = ListSort(randomArray);
            Console.WriteLine("List: compare: {0}, insert: {1} ", sorting.Compare, sorting.Insert);
        }

        private static void PrintArray(int[] arr)
        {
            var n = arr.Length;
            for (var i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
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

        public static Sorting ControlledBobbleSort(int[] mainArray)
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

        public static Sorting ExchangeSort(int[] mainArray)
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

        public static Sorting SelectionSort(int[] mainArray)
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

        public static Sorting MergeSort(int[] mainArray)
        {
            var array01 = new int[mainArray.Length / 2];
            var array02 = new int[mainArray.Length / 2];
            var array = new int[mainArray.Length];
            for (var i = 0; i < mainArray.Length; i++)
                if (i < mainArray.Length / 2)
                    array01[i] = mainArray[i];
                else
                    array02[i - mainArray.Length / 2] = mainArray[i];
            // Sort split array
            var sorting = new Sorting();
            var a1 = SelectionSort(array01);
            a1.Array.CopyTo(array01, 0);
            sorting.Compare = a1.Compare;
            sorting.Move = a1.Move;
            var a2 = SelectionSort(array02);
            a2.Array.CopyTo(array02, 0);
            sorting.Compare += a2.Compare;
            sorting.Move += a2.Move;
            var counter = 0;
            for (var i = 0; i < array.Length / 2; i++)
            for (var j = counter - i; j < array.Length / 2; j++)
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