using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

using JRB.Randomizer;

namespace JRB.Randomizer.TestConsole
{
    public static class Program
    {

        static void Main(string[] args)
        {
            //TestRandomizationSpread();
            TestRandomizedOrder();
        }

        static void TestRandomizationSpread()
        {
            Random random = Random.Shared;

            Dictionary<char, int> results = new Dictionary<char, int>
            {
                { 'A', 0 },
                { 'B', 0 },
                { 'C', 0 },
                { 'D', 0 },
                { 'X', 0 }
            };

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                char value = random.GetRandom('A', 'B', 'C', 'D');
                if (results.ContainsKey(value))
                {
                    results[value]++;
                }
                else
                {
                    results['X']++;
                }
            }
            sw.Stop();

            Console.WriteLine($"Elapsed time:  {sw.Elapsed}");
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Key}:  {item.Value}");
            }
        }

        static void TestRandomizedOrder()
        {
            Random random = Random.Shared;

            char[] allKeys = new char[] { 'A', 'B', 'C', 'D', 'E' };
            Dictionary<char, int[]> results = new Dictionary<char, int[]>();
            foreach (char key in allKeys)
            {
                int[] array = new int[allKeys.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = 0;
                }
                results.Add(key, array);
            }

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                char[] randomizedList = random.Randomize(allKeys).ToArray();

                if (!OriginalUnchanged())
                {
                    return;
                }

                for (int index = 0;  index < randomizedList.Length; index++)
                {
                    char key = randomizedList[index];
                    results[key][index] += 1;
                }
            }
            sw.Stop();

            Console.WriteLine($"Elapsed time:  {sw.Elapsed}");
            Console.WriteLine("\t" + string.Join("\t", Enumerable.Range(1, allKeys.Length).Select(i => $"{i}:")));
            foreach (KeyValuePair<char, int[]> item in results)
            {
                Console.Write($"{item.Key}:\t");
                for (int index = 0; index < item.Value.Length; index++)
                {
                    Console.Write($"{item.Value[index]}\t");
                }
                Console.WriteLine();
            }

            bool OriginalUnchanged()
            {
                if (allKeys[0] != 'A')
                {
                    WriteError($"First item should have been 'A', but was '{allKeys[0]}'.");
                    return false;
                }
                else if (allKeys[1] != 'B')
                {
                    WriteError($"Second item should have been 'B', but was '{allKeys[1]}'.");
                    return false;
                }
                else if (allKeys[2] != 'C')
                {
                    WriteError($"Third item should have been 'C', but was '{allKeys[2]}'.");
                    return false;
                }
                else if (allKeys[3] != 'D')
                {
                    WriteError($"Fourth item should have been 'D', but was '{allKeys[3]}'.");
                    return false;
                }
                else if (allKeys[4] != 'E')
                {
                    WriteError($"Fifth item should have been 'E', but was '{allKeys[4]}'.");
                    return false;
                }

                return true;
            }

            void WriteError(string message)
            {
                ConsoleColor original = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = original;
            }
        }

    }
}
