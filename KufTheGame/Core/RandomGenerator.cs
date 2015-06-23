using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Models.Structures;

namespace KufTheGame.Core
{
    public static class RandomGenerator
    {
        private static readonly Random rngJesus = new Random();

        public static int Randomize(int lower, int upper)
        {
            return rngJesus.Next(lower, upper + 1);
        }

        public static T GetRandomItem<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            var randomItem = (T)values.GetValue(Randomize(0, values.Length - 1));

            return randomItem;
        }
    }
}
