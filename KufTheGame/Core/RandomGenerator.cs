using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KufTheGame.Models.Enums;

namespace KufTheGame.Core
{
    public static class RandomGenerator
    {
        public static int Randomize(int lower, int upper)
        {
            var rngJesus = new Random();
            return rngJesus.Next(lower, upper + 1);
        }

        public static T GetRandomItem<T>()
        {
            Random rng = new Random();
            Array values = Enum.GetValues(typeof(T));
            T randomItem = (T)values.GetValue(rng.Next(values.Length));

            return randomItem;
        }
    }
}
