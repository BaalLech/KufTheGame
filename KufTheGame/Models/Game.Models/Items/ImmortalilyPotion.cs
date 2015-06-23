using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Properties;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Items
{
    class ImmortalilyPotion : Potion
    {
        public ImmortalilyPotion(int x, int y, int width, int height, Rarities rarity, int duration)
            : base(x, y, width, height, rarity)
        {
            this.Duration = duration;
        }

        public int Duration { get; set; }

        public override void ProduceSound()
        {
            throw new NotImplementedException();
        }

        public override void Use(Player target)
        {
            target.ImmortalDuration += this.Duration;
        }

        public override string GetTexturePath()
        {
            return Resources.Potion_ImmortalityPotionTexture;
        }
    }
}
