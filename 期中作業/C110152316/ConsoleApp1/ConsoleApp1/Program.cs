using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApplication5
{
    struct Data
    {
        public int Number, Date, SumLoad;
        public string Week, Update;

        public Data(int number, int date, int sumLoad, string week, string update)
        {
            Number = number;
            Date = date;
            SumLoad = sumLoad;
            Week = week;
            Update = update;
        }
    }

    class Program
    {
        static void Swap(int a, int b, ref List<long> s)
        {
            long temp = s[a];
            s[a] = s[b];
            s[b] = temp;
        }

        static void Partition(int low, int high, ref int pivotPoint, ref List<long> s)
        {
            long pivotItem = s[low];
            int j = low;

            for (int i = low + 1; i <= high; i++)
            {
                if (s[i] < pivotItem)
                {
                    j++;
                    Swap(i, j, ref s);
                }
            }

            pivotPoint = j;
            Swap(low, pivotPoint, ref s);
        }

        static void QuickSort(int low, int high, ref List<long> s)
        {
            if (low < high)
            {
                int pivotPoint = 0;
                Partition(low, high, ref pivotPoint, ref s);
                QuickSort(low, pivotPoint - 1, ref s);
                QuickSort(pivotPoint + 1, high, ref s);
            }
        }

        public static void Main()
        {
            try
            {
                string filePath = "/mnt/data/gsa001yac.csv";

                using StreamReader reader = new StreamReader(filePath, Encoding.UTF8);

                string line;
                List<Data> dataList = new List<Data>();

                reader.ReadLine(); // Skip header row

                while ((line = reader.ReadLine()) != null)
                {
                    string[] items = line.Split(',');

                    if (items.Length >= 5)
                    {
                        Data data = new Data(
                            number: int.Parse(items[0]),
                            date: int.Parse(items[1]),
                            sumLoad: int.Parse(items[3]),
                            week: items[2],
                            update: items[4]
                        );

                        dataList.Add(data);
                    }
                }

                List<long> sumLoadList = new List<long>();
                int totalSumLoad = 0;

                foreach (var data in dataList)
                {
                    totalSumLoad += data.SumLoad;
                    sumLoadList.Add(data.SumLoad);
                }

                QuickSort(0, sumLoadList.Count - 1, ref sumLoadList);

                Console.WriteLine("Sorted Data (Low to High):");
                for (int i = 0; i < sumLoadList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {sumLoadList[i]}");
                }

                Console.WriteLine($"Total Entries: {sumLoadList.Count}");
                Console.WriteLine($"Total SumLoad: {totalSumLoad}");
                Console.WriteLine($"Maximum SumLoad: {sumLoadList[^1]}");
                Console.WriteLine($"Minimum SumLoad: {sumLoadList[0]}");
                Console.WriteLine($"Median SumLoad: {sumLoadList[sumLoadList.Count / 2]}");
                Console.WriteLine($"Average SumLoad: {(double)totalSumLoad / sumLoadList.Count:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
