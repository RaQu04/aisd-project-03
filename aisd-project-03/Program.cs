using System;
using System.Diagnostics;
using System.Threading;

namespace aisd_project_03
{
    class Program
    {
        static void InsertionSort(int[] t)
        {
            for (uint i = 1; i < t.Length; i++)
            {
                uint j = i; // elementy 0 .. i-1 są już posortowane
                int Buf = t[j]; // bierzemy i-ty (j-ty) element
                while ((j > 0) && (t[j - 1] > Buf))
                { // przesuwamy elementy
                    t[j] = t[j - 1];
                    j--;
                }
                t[j] = Buf; // i wpisujemy na docelowe miejsce
            }
        }

        static void SelectionSort(int[] t)
        {
            uint k;
            for (uint i = 0; i < (t.Length - 1); i++)
            {
                int Buf = t[i]; // bierzemy i-ty element
                k = i; // i jego indeks
                for (uint j = i + 1; j < t.Length; j++)
                    if (t[j] < Buf) // szukamy najmniejszego z prawej
                    {
                        k = j;
                        Buf = t[j];
                    }
                t[k] = t[i]; // zamieniamy i-ty z k-tym
                t[i] = Buf;
            }
        }

        static void CocktailSort(int[] t)
        {
            int Left = 1, Right = t.Length - 1, k = t.Length - 1;
            do
            {
                for (int j = Right; j >= Left; j--) // przesiewanie od dołu
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j; // zamiana elementów i zapamiętanie indeksu
                    }
                Left = k + 1; // zacieśnienie lewej granicy
                for (int j = Left; j <= Right; j++) // przesiewanie od góry
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j; // zamiana elementów i zapamiętanie indeksu
                    }
                Right = k - 1; // zacieśnienie prawej granicy
            }
            while (Left <= Right);
        }

        static void Heapify(int[] t, uint left, uint right)
        { // procedura budowania/naprawiania kopca
            uint i = left,
            j = 2 * i + 1;
            int buf = t[i]; // ojciec
            while (j <= right) // przesiewamy do dna stogu
            {
                if (j < right) // wybieramy większego syna
                    if (t[j] < t[j + 1]) j++;
                if (buf >= t[j]) break;
                t[i] = t[j];
                i = j;
                j = 2 * i + 1; // przechodzimy do dzieci syna
            }
            t[i] = buf;
        }

        static void HeapSort(int[] t)
        {
            uint left = ((uint)t.Length / 2),
            right = (uint)t.Length - 1;
            while (left > 0) // budujemy kopiec idąc od połowy tablicy
            {
                left--;
                Heapify(t, left, right);
            }
            while (right > 0) // rozbieramy kopiec
            {
                int buf = t[left];
                t[left] = t[right];
                t[right] = buf; // największy element
                right--; // kopiec jest mniejszy
                Heapify(t, left, right); // ale trzeba go naprawić
            }
        }

        static void GenerateAscendingArray(int[] t)
        {
            for (int i = 0; i < t.Length; ++i) t[i] = i;
        }
        static void GenerateDescendingArray(int[] t)
        {
            for (int i = 0; i < t.Length; ++i) t[i] = t.Length - i - 1;
        }
        static void GenerateRandomArray(int[] t, Random rnd, int maxValue = int.MaxValue)
        {
            for (int i = 0; i < t.Length; ++i)
                t[i] = rnd.Next(maxValue);
        }
        static void GenerateConstatntArray(int[] t, int value)
        {
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = value; 
            }
        }
        static void GenerateVshapeArray(int[] t)
        {
            int count = 0;

            for (int i = t.Length / 2; i > 0; i--)
            {
                t[count++] = i;
            }

            for (int i = 0; i < t.Length / 2; i++)
            {
                t[count++] = i;
            }
        }

        static void Tester()
        {
            // tu testy
        }

        static void Main(string[] args)
        {

            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Console.WriteLine("SelectionSort");
            Console.WriteLine("Wartość\tConstatnt array\tAscending array\tDescending Array\tRandom array\tV-shape array");

            for (int i = 50000; i <= 200000; i += 10000)
            {
                Int32[] tab = new Int32[i];
                var watch = new Stopwatch();
                SelectionSortTest(rnd, i, tab, watch);
                Console.WriteLine();
            }
            
            Console.WriteLine("InsertionSort");
            Console.WriteLine("Wartość\tConstatnt array\tAscending array\tDescending Array\tRandom array\tV-shape array");

            for (int i = 50000; i <= 200000; i += 10000)
            {
                Int32[] tab = new Int32[i];
                var watch = new Stopwatch();

                InsertionSortTest(rnd, i, tab, watch);
                Console.WriteLine();
            }

            Console.WriteLine("CocktailSort");
            Console.WriteLine("Wartość\tConstatnt array\tAscending array\tDescending Array\tRandom array\tV-shape array");

            for (int i = 50000; i <= 200000; i += 10000)
            {
                Int32[] tab = new Int32[i];
                var watch = new Stopwatch();

                CocktailSortTest(rnd, i, tab, watch);
                Console.WriteLine();
            }

            Console.WriteLine("HeapSort");
            Console.WriteLine("Wartość\tConstatnt array\tAscending array\tDescending Array\tRandom array\tV-shape array");

            for (int i = 50000; i <= 200000; i += 10000)
            {
                Int32[] tab = new Int32[i];
                var watch = new Stopwatch();

                HeapSortTest(rnd, i, tab, watch);
                Console.WriteLine();
            }


        }

        private static void SelectionSortTest(Random rnd, int i, int[] tab, Stopwatch watch)
        {
            Console.Write(i + "\t");
            GenerateConstatntArray(tab, 100);
            watch.Start();
            SelectionSort(tab);
            watch.Stop();
           
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);
            watch.Reset();
            GenerateAscendingArray(tab);
            watch.Start();
            SelectionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateDescendingArray(tab);
            watch.Reset();
            watch.Start();
            SelectionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateRandomArray(tab, rnd, int.MaxValue);
            watch.Reset();
            watch.Start();
            SelectionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateVshapeArray(tab);
            watch.Reset();
            watch.Start();
            SelectionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);
        }

        private static void InsertionSortTest(Random rnd, int i, int[] tab, Stopwatch watch)
        {
            Console.Write(i + "\t");
            GenerateConstatntArray(tab, 100);
            watch.Reset();
            watch.Start();
            InsertionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateAscendingArray(tab);
            watch.Reset();
            watch.Start();
            InsertionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateDescendingArray(tab);
            watch.Reset();
            watch.Start();
            InsertionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateRandomArray(tab, rnd, int.MaxValue);
            watch.Reset();
            watch.Start();
            InsertionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateVshapeArray(tab);
            watch.Reset();
            watch.Start();
            InsertionSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);
        }

        private static void CocktailSortTest(Random rnd, int i, int[] tab, Stopwatch watch)
        {
            Console.Write(i + "\t");
            GenerateConstatntArray(tab, 100);
            watch.Reset();
            watch.Start();
            CocktailSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateAscendingArray(tab);
            watch.Reset();
            watch.Start();
            CocktailSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateDescendingArray(tab);
            watch.Reset();
            watch.Start();
            CocktailSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateRandomArray(tab, rnd, int.MaxValue);
            watch.Reset();
            watch.Start();
            CocktailSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateVshapeArray(tab);
            watch.Reset();
            watch.Start();
            CocktailSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);
        }

        private static void HeapSortTest(Random rnd, int i, int[] tab, Stopwatch watch)
        {
            Console.Write(i + "\t");
            GenerateConstatntArray(tab, 100);
            watch.Reset();
            watch.Start();
            HeapSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateAscendingArray(tab);
            watch.Reset();
            watch.Start();
            HeapSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateDescendingArray(tab);
            watch.Reset();
            watch.Start();
            HeapSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateRandomArray(tab, rnd, int.MaxValue);
            watch.Reset();
            watch.Start();
            HeapSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);

            GenerateVshapeArray(tab);
            watch.Reset();
            watch.Start();
            HeapSort(tab);
            watch.Stop();
            Console.Write("{0} ms" + "\t", 1000.0 * watch.ElapsedTicks / Stopwatch.Frequency);
        }

    }
}
