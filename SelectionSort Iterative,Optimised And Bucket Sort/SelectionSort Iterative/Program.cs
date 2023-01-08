using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace SelectionSort_Iterative
{
    class Program
    {
        static int comps = 0; static int swops = 0; static int Compare = 0; static int Min=0; static int Max=0;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ArrayList L = new ArrayList();

            StreamReader SW = new StreamReader("text.txt");

            //Console.WriteLine("how many integers to populate");
            //int n = int.Parse(Console.ReadLine());
            //Console.WriteLine();
            //populate(L, n);
            while (!SW.EndOfStream)
            {
                string n = SW.ReadLine() ;
                
                L.Add(n);
            }
            SW.Close();
            display(L);
            Console.WriteLine();
            Console.WriteLine("Sorting {0} elements", L.Count);

           //Console.WriteLine("How many Buckets do you want?");
           //int m = int.Parse(Console.ReadLine());
           //BucketSort(L, m);

           //BasicSort(L);

           //OptSelectionSort(L);
            
            
            display(L);
            Console.WriteLine();
           
            Console.ReadLine();

           
        }

        public static void BasicSort(ArrayList list)
        {
            Stopwatch timer = new Stopwatch();
            timer.Reset();
            timer.Start();
           int  swops = 0, comps = 0;

            for (int i = 0; i < list.Count - 1; i++) //iterate through the unsorted list
            {

                int min = i;  //find the mininum/maximum element
                for (int j = i + 1; j < list.Count; j++)
                {
                    comps++;
                    if (Convert.ToInt32(list[j]) > Convert.ToInt32(list[min]))//if the current int is larger than the min int set min to be the larger element 
                        min = j;
                   
                }

                swap(list, min, i);//switch position between the biggest and min element found at each iteration
                swops++;// increment the number of swops after every swap
            }
            timer.Stop();

            Console.WriteLine("--Basic-Iterative Sort has finished running--");
            Console.WriteLine("Swops: {0}, comps: {1}, Time-Lapsed: {2}", swops, comps, timer.ElapsedTicks);

        }


        static public void populate(ArrayList list, int n)
        {
            Random No = new Random();

            for (int i = 1; i <= n; i++)
            {
                int number;
                do
                {
                    number = No.Next(0, 50);
                } while (!(list.BinarySearch(number) < 0));
                list.Add(number);
            }

        }
        public static void display(ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + " ");
            }
        }
        public static void OptSelectionSort(ArrayList List)
        {
            Stopwatch timer = new Stopwatch();
            timer.Reset();
            timer.Start();
            int swaps = 0;
            int i = 0;
            int j = List.Count - 1; Compare = 0;

            while (i < j && i != j - 1) //passes through the array keeping the sorted list on the outside.
            {
                findMinOrMAx(List, i, j);
                int minPos = Min;//gets position of smallest element
                if (minPos != j)//swaps if the small element is not already in the correct postion 
                {
                    swap(List, minPos, j);
                    swaps++;

                }



                int maxPos = Max;//gets position of biggest element

                //Console.WriteLine(maxPos);

                if (maxPos > -1 && maxPos != i)//swaps if the biggest element is not already in the correct postion 
                {
                    swap(List, maxPos, i);
                    swaps++;
                }


                i++;//reduces the unsorted list from the front 
                j--;//reduces the unsorted list from the back

            }

            timer.Stop();

            Console.WriteLine("--Optimized-Iterative Sort has finished running--");
            Console.WriteLine("Swops: {0}, comps: {1}, Time-Lapsed: {2}", swaps, Compare, timer.ElapsedTicks);

        }
        private static void swap(ArrayList list, int i, int j)
        {
            Object temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        private static void findMinOrMAx(ArrayList List, int Start,  int End)//finds and returns the position of smallest integer in an ArrayList
        {
            
          


            for (int i = Start; i <= End; i++)
            {
                Compare++;
                if ((int)List[i] <(int)List[Min])//checks for the smallest element in the list
                {
                    Min = i;
                }

                Compare++;
                if ((int)List[i] >(int)List[Max])//checks for the biggest element in the list
                {
                    Max = i;
                }



            }
            
        }

        public static void BucketSort(ArrayList List, int noBuckets)
        {
            Stopwatch timer = new Stopwatch();
            timer.Reset();
            timer.Start();

            //int comps = 0;int swops = 0;

            ArrayList buckets = new ArrayList();
            for (int i = 0; i < noBuckets; i++)
            {
                buckets.Add(new ArrayList());// creates empty buckets

            }

            for (int x = 0; x <= List.Count-1; x++)
            {
                int buckValue = (int)Math.Floor((Convert.ToDecimal(List[x]) / findBucketRange(List, noBuckets)));//determine the correct bucket to put the item in
                ArrayList listobj = (ArrayList)buckets[buckValue];
                listobj.Add(List[x]);// puts items in the correct buckets 
            }

            List.Clear();
            for (int y = 0; y < buckets.Count; y++)
            {
                ArrayList temp = BasicSortforBucket((ArrayList)buckets[y]);//sorts buckets
                List.AddRange(temp);//inserts sorted buckets into the original array
            }
            timer.Stop();
           
            Console.WriteLine("---Bucket Sort Finished---");
            Console.WriteLine("Number of Comparisons: {0}   Number of Swaps: {1}, Time taken: {2}",comps,swops,timer.ElapsedTicks);
        }
        public static ArrayList BasicSortforBucket(ArrayList list)
        {
            
            

            for (int i = 0; i < list.Count - 1; i++) //iterate through the unsorted list
            {

                int min = i;  //find the mininum/maximum element
                for (int j = i + 1; j < list.Count; j++)
                {
                    comps++;
                    if (Convert.ToInt32(list[j]) < Convert.ToInt32(list[min]))//if the current int is larger than the min int set min to be the larger element 
                        min = j;

                }

                swap(list, min, i);//switch position between the biggest and min element found at each iteration
                swops++;// increment the number of swops after every swap
            }


            return list;

        }

        public static int findBucketRange(ArrayList List, int noBuckets)

        {
            int max = -1;
            for (int x = 0; x < List.Count; x++)//goes through the list
                if ((int)List[x] > max)//finds the biggest item in list
                    max = (int)List[x];// sets max to the biggest item in list

            return (int)Math.Ceiling(((decimal)max / noBuckets));//calculates an returns the range of the list

        }
    }
}
