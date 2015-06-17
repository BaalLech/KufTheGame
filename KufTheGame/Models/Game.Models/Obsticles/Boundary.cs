using KufTheGame.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KufTheGame.Models.Game.Models.Obsticles
{
    class Boundary:Obsticle
    {
        public Boundary(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
        }
        public override int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Visible
        {
            get { throw new NotImplementedException(); }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void ProduceSound()
        {
            throw new NotImplementedException();
        }
    }
}
