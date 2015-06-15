﻿using System;
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
        protected Enemy(int x, int y, int width, int height, double attackPoints, double defencePoints, double healthPoints)
            : base(x, y, width, height, attackPoints, defencePoints, healthPoints)
        {
            this.Drops = new List<IItem>();
        }

        public ICollection<IItem> Drops { get; private set; }

        public override void Move()
        {
            var rngDirection = RandomGenerator.Randomize(0, 1);
            if (rngDirection == 0)
            {
                if (this.Velocity.X > KufTheGame.Player.Velocity.X + KufTheGame.Player.Width / 3)
                {
                    this.Velocity = new Vector2(this.Velocity.X - 1, this.Velocity.Y);
                }

                if (this.Velocity.X < KufTheGame.Player.Velocity.X + KufTheGame.Player.Width / 3)
                {
                    this.Velocity = new Vector2(this.Velocity.X + 1, this.Velocity.Y);
                }
            }
            else
            {
                if (this.Velocity.Y > KufTheGame.Player.Velocity.Y - this.Height / 2)
                {
                    this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - 1);
                }

                if (this.Velocity.Y < KufTheGame.Player.Velocity.Y - this.Height / 2)
                {
                    this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + 1);
                }
            }
        }

        public override abstract BasicAttack Attack();

        public abstract override void RespondToAttack(BasicAttack attack);

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

        public void AddDrops()
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

            return 1;

            return numOfDrop;
        }

        private Item GetItem()
        {
            var rngNum = RandomGenerator.Randomize(1, 3);
            var rarity = Rarity.GetRandomRarity();
            var rarityType = rarity.Keys.First();
            var rarityCoef = rarity.Values.First();
            var dropX = (int)this.Velocity.X + this.Width / 2 - KufTheGame.ItemSize / 2;
            var dropY = (int)this.Velocity.Y + this.Height / 2 - KufTheGame.ItemSize / 2;

            switch (rngNum)
            {
                case 1:
                    var weaponType = RandomGenerator.GetRandomItem<Weapons>();
                    Weapon wep = new Weapon(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize, rarityType,
                        weaponType, 10 * rarityCoef);
                    return wep;
                case 2:
                    var armorType = RandomGenerator.GetRandomItem<Armors>();
                    Armor armor = new Armor(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize, rarityType,
                        armorType, 5 * rarityCoef);
                    return armor;
                case 3:
                    var rngPotionNum = RandomGenerator.Randomize(1, 2);
                    if (rngPotionNum == 1)
                    {
                        Potion potion = new HealthPotion(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize, rarityType,
                            25 * rarityCoef);
                        return potion;
                    }
                    else
                    {
                        Potion potion = new ImmortalilyPotion(dropX, dropY, KufTheGame.ItemSize, KufTheGame.ItemSize, rarityType,
                            2 * (int)rarityCoef + 1);
                        return potion;
                    }
                default:
                    return null;
            }
        }
    }
}