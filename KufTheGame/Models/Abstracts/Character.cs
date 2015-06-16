using System.Collections.Generic;
using KufTheGame.Models.Game.Models;
using KufTheGame.Models.Interfaces;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Character : GameObject, IMoveable, IAttackable, ICharacter
    {
        private const int InitialLives = 1;

        protected Character(int x, int y, int width, int height, double attackPoints, double defencePoints,
            double healthPoints)
            : base(x, y, width, height)
        {
            this.AttackPoints = attackPoints;
            this.DefencePoints = defencePoints;
            this.HealthPoints = healthPoints;
            this.BaseHealthPoints = healthPoints;
            this.Lives = InitialLives;
        }

        public double AttackPoints { get; set; }

        public double DefencePoints { get; set; }

        public int Lives { get; protected set; }

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
            if (this.Lives > 1)
            {
                return true;
            }

            return this.HealthPoints > 0;
        }

        public void AddLive()
        {
            this.Lives++;
        }

        public void RemoveLive()
        {
            this.Lives--;
        }


        public abstract void Move();

        public abstract BasicAttack Attack();

        public abstract void RespondToAttack(BasicAttack attack);
    }
}