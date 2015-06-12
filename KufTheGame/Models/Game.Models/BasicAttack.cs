using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KufTheGame.Models.Abstracts;

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
            var damage = this.Damage * 2 - target.DefencePoints;
            if (damage < this.Damage / 10d)
            {
                damage = this.Damage / 10d;
            }

            target.HealthPoints -= damage;
        }
    }
}
