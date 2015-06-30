using System.Collections.Generic;
using System.Linq;

using KufTheGame.Core;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models;
using KufTheGame.Models.Game.Models.Items;
using KufTheGame.Models.Interfaces;
using KufTheGame.Models.Structures;

using Microsoft.Xna.Framework;


namespace KufTheGame.Models.Abstracts
{
    public abstract class Enemy : Character, IEnemy
    {
        protected const int EnemySpeed = 2;

        protected Enemy(int x, int y, int width, int height, double attackPoints, double defencePoints, double healthPoints)
            : base(x, y, width, height, attackPoints, defencePoints, healthPoints)
        {
            this.Drops = new List<IItem>();
            this.State = State.Moving;
        }

        public ICollection<IItem> Drops { get; private set; }

        public override void Move()
        {
            var rngDirection = RandomGenerator.Randomize(0, 1);
            if (rngDirection == 0)
            {
                if (this.Velocity.X > KufTheGame.Player.Velocity.X + KufTheGame.Player.Width)
                {
                    this.SpriteRotation = 0;
                    if (!Directions.Contains(BlockedDirections.BlockedLeft))
                    {
                        this.Velocity = new Vector2(this.Velocity.X - EnemySpeed, this.Velocity.Y);
                    }
                }

                if (this.Velocity.X < KufTheGame.Player.Velocity.X + KufTheGame.Player.Width)
                {
                    this.SpriteRotation = 1;
                    if (!Directions.Contains(BlockedDirections.BlockedRight))
                    {
                        this.Velocity = new Vector2(this.Velocity.X + EnemySpeed, this.Velocity.Y);
                    }
                }
            }
            else
            {
                if (this.Velocity.Y > KufTheGame.Player.Velocity.Y )
                {
                    if (!this.Directions.Contains(BlockedDirections.BlockedUp))
                    {
                        this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - EnemySpeed);
                    }
                }

                if (this.Velocity.Y < KufTheGame.Player.Velocity.Y )
                {
                    if (!this.Directions.Contains(BlockedDirections.BlockedDown))
                    {
                        this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + EnemySpeed);
                    }
                }
            }
        }

        public override BasicAttack Attack()
        {
            this.State = (this.State != State.Moving) ? this.State : (State) RandomGenerator.Randomize(2, 4);

            return new BasicAttack(this.AttackPoints);
        }

        public override void RespondToAttack(BasicAttack attack)
        {
            attack.Hit(this);
        }

        public void AddDrops()
        {
            var rngNum = GetNumOfDrops();

            for (var i = 0; i < rngNum; i++)
            {
                var item = GetItem(i);
                this.Drops.Add(item);
            }
        }

        private int GetNumOfDrops()
        {
            var rngNum = RandomGenerator.Randomize(0, 100);

            var numOfDrop = 0;
            if (rngNum > 20 && rngNum <= 90)
            {
                numOfDrop = 1;
            }

            if (rngNum > 90)
            {
                numOfDrop = 2;
            }

            return numOfDrop;
        }

        private Item GetItem(int numOfItems)
        {
            var rngNum = RandomGenerator.Randomize(1, 100);
            var rarity = Rarity.GetRandomRarity();
            var rarityType = rarity.Keys.First();
            var rarityCoef = rarity.Values.First();
            var dropX = (int)this.Velocity.X + this.Width / (2 - numOfItems) - KufTheGame.ItemSize / 2;
            var dropY = (int) this.Velocity.Y + this.Height/2 - KufTheGame.ItemSize/2;

            if (rngNum <= 25)
            {
                var weaponType = RandomGenerator.GetRandomItem<Weapons>();
                var wep = new Weapon(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize, rarityType,
                    weaponType, 10*rarityCoef);
                return wep;
            }

            if (rngNum > 25 && rngNum <= 65)
            {
                var armorType = RandomGenerator.GetRandomItem<Armors>();
                var armor = new Armor(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize, rarityType,
                    armorType, 5*rarityCoef);
                return armor;
            }

            if (rngNum > 65)
            {
                var rngPotionNum = RandomGenerator.Randomize(1, 100);
                if (rngPotionNum <= 40)
                {
                    var potion = new ImmortalilyPotion(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize,
                        rarityType,
                        (int)(150 * rarityCoef));
                    return potion;
                }

                if (rngPotionNum > 40)
                {
                    var potion = new HealthPotion(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize, rarityType,
                        10*rarityCoef);
                    return potion;
                }
            }

            return null;
        }

        public override abstract string GetTexturePath();
    }
}