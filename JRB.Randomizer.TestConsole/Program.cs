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
            TestRandomizationSpread();
        }

        static void TestRandomizationSpread()
        {
            Dictionary<char, int> results = new Dictionary<char, int>();
            results.Add('A', 0);
            results.Add('B', 0);
            results.Add('C', 0);
            results.Add('D', 0);
            results.Add('X', 0);

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                char value = Randomizer.GetRandom('A', 'B', 'C', 'D');
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

    }
}
