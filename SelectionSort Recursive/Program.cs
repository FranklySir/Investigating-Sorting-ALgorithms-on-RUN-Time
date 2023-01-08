using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace SelectionSort_Recursive
{
    internal static class Program
    {
        private static int Compare;
        private static int Swaps;

        private static void Main()
        {
            var list = new ArrayList();
            using (var sw = new StreamReader("text.txt"))
            {
                while (!sw.EndOfStream)
                {
                    if (int.TryParse(sw.ReadLine(), out var n))
                    {
                        list.Add(n);
                    }
                }
            }

            Console.WriteLine("List before Sorting");
            Display(list);
            Console.WriteLine("Sorting {0} elements", list.Count);

            SelectionSort(list, list.Count);
            Console.WriteLine("Sorted List");
            Display(list);
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void SelectionSort(ArrayList list, int n)
        {
            var timer = new Stopwatch();
            timer.Reset();
            timer.Start();
            ReSelectionSort(list, n, 0);
            timer.Stop();

            Console.WriteLine("--Recursive Sort has finished running--");
            Console.WriteLine("Swops: {0}, comps: {1}, Time-Lapsed: {2}", Swaps, Compare, timer.ElapsedTicks);
        }

        private static void ReSelectionSort(ArrayList list, int n, int index)
        {
            if (index == n)
            {
                return;
            }

            var k = MinIndex(list, index, n - 1);

            if (k != index)
            {
                Swap(list, index, k);
                Swaps++;
            }

            ReSelectionSort(list, n, index + 1);
        }

        private static int MinIndex(ArrayList list, int i, int j)
        {
            if (i == j)
            {
                return i;
            }

            var k = MinIndex(list, i + 1, j);

            if (Convert.ToInt32(list[i]) > Convert.ToInt32(list[k]))
            {
                Compare++;
                return i;
            }

            Compare++;
            return k;
        }

        private static void Swap(ArrayList list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void Populate(ArrayList list, int n)
        {
            var no = new Random();

            for (var i = 1; i <= n; i++)
            {
                int number;
                do
                {
                    number = no.Next(0, 50);
                } while (list.BinarySearch(number) >= 0);
                list.Add(number);
            }
        }

        public static void Display(ArrayList list)
        {
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
        }
    }
}