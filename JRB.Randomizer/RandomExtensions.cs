using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.Randomizer
{
    public static class RandomExtensions
    {

        public static IEnumerable<T> Randomize<T>(this Random random, IEnumerable<T> original)
        {
            T[] array = [.. original];
            random.Shuffle(array);
            foreach (T item in array)
                yield return item;
        }

        public static T GetRandomExcludingOne<T>(this Random random, IEnumerable<T> list, Func<T, bool> testEquality)
        {
            IList<T> excluded = list.Where(x => !testEquality(x)).ToList();
            return GetRandom(random, excluded);
        }

        public static T GetRandom<T>(this Random random, IList<T> list)
        {
            int index = random.Next(0, list.Count);
            return list[index];
        }

        public static T GetRandom<T>(this Random random, params T[] items)
        {
            return GetRandom(random, items as IList<T>);
        }

        public static T GetRandom<T>(this Random random, params Func<T>[] methods)
        {
            int index = random.Next(0, methods.Length);
            return methods[index]();
        }

    }
}
