namespace JRB.Randomizer
{
    public static class Randomizer
    {

        private static readonly Random _random;

        static Randomizer()
        {
            _random = new Random();
        }

        public static int Next(int maxValue) => _random.Next(maxValue);

        public static int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);

        public static T GetRandom<T>(params T[] items)
        {
            return GetRandom(items as IList<T>);
        }

        public static T GetRandom<T>(params Func<T>[] methods)
        {
            int index = Next(0, methods.Length);
            return methods[index]();
        }

        public static T GetRandom<T>() where T : struct, Enum
        {
            var v = Enum.GetValues(typeof(T));
            object? obj = v.GetValue(_random.Next(v.Length));
            return (obj == null) ? default : (T)obj;
        }

        public static T GetRandom<T>(IList<T> list)
        {
            int index = Next(0, list.Count);
            return list[index];
        }

        public static T GetRandom<T>(List<T> list)
        {
            return GetRandom(list as IList<T>);
        }

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandomExcludingOne<T>(IEnumerable<T> list, Func<T, bool> testEquality)
        {
            List<T> ex = list.Where(x => !testEquality(x)).ToList();
            return GetRandom(ex);
        }

    }
}
