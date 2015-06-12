using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Items
{
    class ImmortalilyPotion: Potion
    {
        public ImmortalilyPotion(int x, int y, Rarities rarity, int duration) : base(x, y, rarity)
        {
            this.Duration = duration;
        }

        public int Duration { get; set; }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void ProduceSound()
        {
            throw new NotImplementedException();
        }

        public override int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Visible
        {
            get { throw new NotImplementedException(); }
        }
    }
}
