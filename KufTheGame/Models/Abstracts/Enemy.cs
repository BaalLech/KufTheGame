using System;
using System.Collections.Generic;
using System.Linq;
using KufTheGame.Core;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Items;
using KufTheGame.Models.Interfaces;
using KufTheGame.Models.Structures;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Enemy : Character, IEnemy
    {
        protected Enemy(int x, int y, double attackPoints, double defencePoints, double healthPoints)
            : base(x, y, attackPoints, defencePoints, healthPoints)
        {
            this.AddDrops();
        }

        public IList<IItem> Drops { get; private set; }
        
        public override void Move()
        {
            
        }

        public override void Attack(Character target)
        {
            throw new System.NotImplementedException();
        }

        public override void RespondToAttack()
        {
            throw new System.NotImplementedException();
        }

        public override int DrawOrder
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool Visible
        {
            get { throw new System.NotImplementedException(); }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void ProduceSound()
        {
            throw new System.NotImplementedException();
        }

        private void AddDrops()
        {
            var rngNum = GetNumOfDrops();

            for (int i = 0; i < rngNum; i++)
            {
                var item = GetItem();
                this.Drops.Add(item);
            }

        }

        private int GetNumOfDrops()
        {
            var rngNum = RandomGenerator.Randomize(0, 100);

            var numOfDrop = 0;
            if (rngNum > 71 && rngNum <= 90)
            {
                numOfDrop = 1;
            }

            if (rngNum > 90)
            {
                numOfDrop = 2;
            }

            return numOfDrop;
        }

        private Item GetItem()
        {
            var rngNum = RandomGenerator.Randomize(0, 3);
            var rarity = Rarity.GetRandomRarity();
            var rarityType = (Rarities)Enum.Parse(typeof(Rarities), rarity.Keys.First(), true);
            var rarityCoef = rarity.Values.First();
            Item item = null;

            switch (rngNum)
            {
                case 1:
                    var weaponType = RandomGenerator.GetRandomItem<Weapons>();
                    item = new Weapon((int)this.Velocity.X, (int)this.Velocity.Y, rarityType,
                        weaponType, 10 * rarityCoef);
                    break;
                case 2:
                    var armorType = RandomGenerator.GetRandomItem<Armors>();
                    item = new Armor((int)this.Velocity.X, (int)this.Velocity.Y, rarityType,
                        armorType, 10 * rarityCoef);
                    break;
                case 3:
                    item = new HealthPotion((int)this.Velocity.X, (int)this.Velocity.Y, rarityType,
                         100 * rarityCoef);
                    break;
            }

            return item;
        }
    }
}