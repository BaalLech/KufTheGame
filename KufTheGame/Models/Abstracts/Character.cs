using System.Collections.Generic;
using KufTheGame.Models.Game.Models;
using KufTheGame.Models.Interfaces;
using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Character : GameObject, IMoveable, IAttackable, ICharacter
    {
        private const int InitialLives = 1;
        protected const int BasicCharacterSpeed = 5;
        public int SpriteRotation { get; set; }


        protected Character(int x, int y, int width, int height, double attackPoints, double defencePoints,
            double healthPoints)
            : base(x, y, width, height)
        {

            this.AttackPoints = attackPoints;
            this.DefencePoints = defencePoints;
            this.HealthPoints = healthPoints;
            this.BaseHealthPoints = healthPoints;
            this.Lives = InitialLives;
            this.Directions = new[] { BlockedDirections.None, BlockedDirections.None, BlockedDirections.None, BlockedDirections.None };
            this.SpriteRotation = 0;
        }

        public BlockedDirections[] Directions { get; set; }

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
            if (this.SpriteRotation == 0)
            {
                return ((this.Velocity.X + this.Width >= target.Velocity.X) &&
                        (this.Velocity.X + this.Width <= target.Velocity.X + target.Width)) &&
                       ((this.Velocity.Y >= target.Velocity.Y) && (this.Velocity.Y <= target.Velocity.Y + target.Height));
            }
            else
            {
                return false;
            }
        }

        public void Intersect(GameObject target)
        {

            if (this.Velocity.X >= (target.Velocity.X - this.Width - BasicCharacterSpeed) && this.Velocity.X <= (target.Velocity.X))
            {
                if (this.Velocity.Y < target.Velocity.Y + target.Height && this.Velocity.Y > target.Velocity.Y - this.Height)
                    this.Directions[0] = BlockedDirections.BlockedRight;

            }
            if (this.Velocity.X <= target.Velocity.X + target.Width + BasicCharacterSpeed && this.Velocity.X >= target.Velocity.X)
            {
                if (this.Velocity.Y < target.Velocity.Y + target.Height && this.Velocity.Y > target.Velocity.Y - this.Height)
                    this.Directions[1] = BlockedDirections.BlockedLeft;

            }
            if (this.Velocity.Y <= (target.Velocity.Y + target.Height + BasicCharacterSpeed) && this.Velocity.Y >= (target.Velocity.Y))
            {
                if (this.Velocity.X > (target.Velocity.X - this.Width) && this.Velocity.X < (target.Velocity.X + target.Width))
                    this.Directions[2] = BlockedDirections.BlockedUp;

            }
            if (this.Velocity.Y >= (target.Velocity.Y - this.Height - BasicCharacterSpeed) && this.Velocity.Y <= (target.Velocity.Y))
            {
                if (this.Velocity.X > (target.Velocity.X - this.Width) && this.Velocity.X < (target.Velocity.X + target.Width))
                    this.Directions[3] = BlockedDirections.BlockedDown;

            }
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



        public void ResetDirections()
        {
            this.Directions = new []{BlockedDirections.None, BlockedDirections.None,BlockedDirections.None, BlockedDirections.None};
        }
    }
}