using System.Collections.Generic;
using KufTheGame.Models.Game.Models;
using KufTheGame.Models.Interfaces;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Character : GameObject, IMoveable, IAttackable, ICharacter
    {
        protected Character(int x, int y, double attackPoints, double defencePoints,
            double healthPoints)
            : base(x, y)
        {
            this.AttackPoints = attackPoints;
            this.DefencePoints = defencePoints;
            this.HealthPoints = healthPoints;
            this.BaseHealthPoints = healthPoints;
        }

        // TODO : Validate for negative values;
        public double AttackPoints { get; set; }

        public double DefencePoints { get; set; }

        public double HealthPoints { get; set; }

        public double BaseHealthPoints { get; private set; }

        public void RemoveHp(double hp)
        {
            this.HealthPoints -= hp;
            if (this.HealthPoints < 0)
            {
                this.HealthPoints = 0;
            }
        }

        public bool IsAlive()
        {
            return this.HealthPoints >= 0;
        }

        public abstract void Move();

        public abstract BasicAttack Attack();

        public abstract void RespondToAttack(BasicAttack attack);
    }
}