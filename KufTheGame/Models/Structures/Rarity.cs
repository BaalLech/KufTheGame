using System.Collections.Generic;
using KufTheGame.Core;
using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Structures
{
    public struct Rarity
    {
        public static readonly Dictionary<int, Dictionary<Rarities, double>> rarities = new Dictionary<int, Dictionary<Rarities, double>>()
        {
           { 0, new Dictionary<Rarities, double>() { { Rarities.Common, 0.5 } } },
           { 1, new Dictionary<Rarities, double>() { { Rarities.Magic, 1 } } },
           { 2, new Dictionary<Rarities, double>() { { Rarities.Rare, 1.5 } } },
           { 3, new Dictionary<Rarities, double>() { { Rarities.Epic, 2.5 } } },
           { 4, new Dictionary<Rarities, double>() { { Rarities.Legendary, 5 } } }
        };

        public static Dictionary<Rarities, double> GetRandomRarity()
        {
            var rngNum = RandomGenerator.Randomize(0, 100);
            var rarity = rarities[0];
            if (rngNum > 50 && rngNum <= 75)
            {
                rarity = rarities[1];
            }

            if (rngNum > 75 && rngNum <= 85)
            {
                rarity = rarities[2];
            }

            if (rngNum > 85 && rngNum <= 95)
            {
                rarity = rarities[3];
            }

            if (rngNum > 95)
            {
                rarity = rarities[4];
            }

            return rarity;
        }
    }
}