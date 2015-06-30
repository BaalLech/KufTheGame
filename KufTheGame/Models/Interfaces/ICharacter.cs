using KufTheGame.Models.Abstracts;

namespace KufTheGame.Models.Interfaces
{
    public interface ICharacter
    {
        double AttackPoints { get; set; }

        double DefencePoints { get; set; }

        int Lives { get; }

        double HealthPoints { get; set; }

        double BaseHealthPoints { get; }

        void RemoveHp(double hp);

        bool IsAlive();

        void Intersect(GameObject target);

        void ResetDirections();

        bool InAttackRange(GameObject target);

        void AddLive();

        void RemoveLive();
    }
}
