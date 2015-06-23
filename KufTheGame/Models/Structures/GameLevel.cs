using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KufTheGame.Core;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Game.Models.Characters;

namespace KufTheGame.Models.Structures
{
    public struct GameLevel
    {
        public const int StartingEnemies = 3;
        private static int level = 1;

        public static int Level
        {
            get
            {
                return level;
            }
        }

        public static List<Enemy> InitializeEnemies()
        {
            var enemies = new List<Enemy>();
            int enemyCoef = (Level - 1) * RandomGenerator.Randomize(1, 3);

            for (int i = 0; i < StartingEnemies + enemyCoef; i++)
            {
                double levelCoef = Level * (RandomGenerator.Randomize(25, 120) / 100) + 1;
                var enemyType = RandomGenerator.Randomize(0, 1);
                var attackPoints = RandomGenerator.Randomize(5, 10) * levelCoef;
                var defPoints = RandomGenerator.Randomize(5, 10) * levelCoef;
                var healthPoints = RandomGenerator.Randomize(50, 150) * levelCoef;
                switch (enemyType)
                {
                    case 0:
                        enemies.Add(new StickmanNinja(
                            KufTheGame.EnemyStartX, KufTheGame.EnemyStartY, KufTheGame.EnemyWidth, KufTheGame.EnemyHeight,
                            attackPoints, defPoints, healthPoints));
                        break;
                    case 1:
                        enemies.Add(new Karateman(
                            KufTheGame.EnemyStartX, KufTheGame.EnemyStartY, KufTheGame.EnemyWidth, KufTheGame.EnemyHeight,
                            attackPoints, defPoints, healthPoints));
                        break;
                }
            }

            level++;

            return enemies;
        }
    }
}