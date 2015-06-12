using System.Collections.Generic;
using KufTheGame.Core;

namespace KufTheGame.Models.Structures
{
    public struct Rarity
    {
        public static Dictionary<int, Dictionary<string, double>> rarities = new Dictionary<int, Dictionary<string, double>>()
        {
           { 0, new Dictionary<string, double>() {{"Common", 0.5}}},
           { 1, new Dictionary<string, double>() {{"Magic", 1}}},
           { 2, new Dictionary<string, double>() {{"Rare", 1.5}}},
           { 3, new Dictionary<string, double>() {{"Epic", 2.5}}},
           { 4, new Dictionary<string, double>() {{"Legendary", 5}}}
        };

        public static Dictionary<string, double> GetRandomRarity()
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