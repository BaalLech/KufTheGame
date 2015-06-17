using System;
using System.Collections.Generic;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models;

namespace KufTheGame.Models.Interfaces
{
    interface ICharacter
    {
        double AttackPoints { get; set; }
        double DefencePoints { get; set; }
        int Lives { get; }
        double HealthPoints { get; set; }
        double BaseHealthPoints { get; }

        void RemoveHp(double hp);

        bool IsAlive();
        BlockedDirections[] Intersect(GameObject target, BlockedDirections[] directions);
        bool InAttackRange(GameObject target);
        void AddLive();
        void RemoveLive();
    }
}
