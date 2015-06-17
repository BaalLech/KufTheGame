using System.Collections.Generic;
using KufTheGame.Models.Game.Models;
using KufTheGame.Models.Interfaces;
using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Character : GameObject, IMoveable, IAttackable, ICharacter
    {
        private const int InitialLives = 1;
        protected const int Speed = 5;

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

        public virtual bool InAttackRange(GameObject target)
        {
            return ((this.Velocity.X + this.Width >= target.Velocity.X) && (this.Velocity.X + this.Width <= target.Velocity.X + 2 * target.Width / 3)) &&
                   ((this.Velocity.Y >= target.Velocity.Y) && (this.Velocity.Y <= target.Velocity.Y + 2 * target.Height / 3));
        }

        public BlockedDirections[] Intersect(GameObject target, BlockedDirections[] directions)
        {

            if (this.Velocity.X >= (target.Velocity.X - this.Width - Speed) && this.Velocity.X <= (target.Velocity.X))
            {
                if (this.Velocity.Y < target.Velocity.Y + target.Height && this.Velocity.Y > target.Velocity.Y - this.Height)
                    directions[0] = BlockedDirections.BlockedRight;

            }
            if (this.Velocity.X <= target.Velocity.X + target.Width + Speed && this.Velocity.X >= target.Velocity.X)
            {
                if (this.Velocity.Y < target.Velocity.Y + target.Height && this.Velocity.Y > target.Velocity.Y - this.Height)
                    directions[1] = BlockedDirections.BlockedLeft;

            }
            if (this.Velocity.Y <= (target.Velocity.Y + target.Height + Speed) && this.Velocity.Y >= (target.Velocity.Y))
            {
                if (this.Velocity.X > (target.Velocity.X - this.Width) && this.Velocity.X < (target.Velocity.X + target.Width))
                    directions[2] = BlockedDirections.BlockedUp;

            }
            if (this.Velocity.Y >= (target.Velocity.Y - this.Height - Speed) && this.Velocity.Y <= (target.Velocity.Y))
            {
                if (this.Velocity.X > (target.Velocity.X - this.Width) && this.Velocity.X < (target.Velocity.X + target.Width))
                    directions[3] = BlockedDirections.BlockedDown;

            }
            return directions;



        }

        public void AddLive()
        {
            this.Lives++;
        }

        public void RemoveLive()
        {
            this.Lives--;
        }


        public virtual void Move() { }

        public abstract BasicAttack Attack();

        public abstract void RespondToAttack(BasicAttack attack);






        public virtual void Move(BlockedDirections[] directions)
        {
            throw new System.NotImplementedException();
        }
    }
}