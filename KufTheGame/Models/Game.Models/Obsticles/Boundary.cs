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
       
        public override void ProduceSound()
        {
            throw new NotImplementedException();
        }

        public override string GetTexturePath()
        {
            return null;
        }
    }
}
