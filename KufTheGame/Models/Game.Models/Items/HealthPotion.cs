﻿using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Properties;

namespace KufTheGame.Models.Game.Models.Items
{
    public class HealthPotion : Potion
    {
        private const string ImagePath = "";

        public HealthPotion(int x, int y, int width, int height, Rarities rarity, double health)
            : base(x, y, width, height, rarity)
        {
            this.Health = health;
        }

        public double Health { get; set; }

        public override void Use(Player target)
        {
            target.HealthPoints += this.Health;
            if (target.HealthPoints > target.BaseHealthPoints)
            {
                target.HealthPoints = target.BaseHealthPoints;
                target.AddLive();
            }
        }

        public override string GetTexturePath()
        {
            return Resources.Potion_HealthPotionTexture;
        }
    }
}