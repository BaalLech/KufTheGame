using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Game.Models.Characters;

namespace KufTheGame.Models.Game.Models
{
    public class BasicAttack
    {
        public BasicAttack(double damage)
        {
            this.Damage = damage;
        }
        public double Damage { get; set; }

        public void Hit(Character target)
        {
            var player = target as Player;
            var armor = player != null ? player.GetTotalArmor() : target.DefencePoints;

            var damage = (this.Damage) / armor;
            if (damage < this.Damage / 100d)
            {
                damage = this.Damage / 100d;
            }

            target.HealthPoints -= damage;

            if (target.HealthPoints <= 0)
            {
                target.RemoveLive();
                target.HealthPoints = target.BaseHealthPoints;
            }
        }
    }
}
