using KufTheGame.Models.Abstracts;

namespace KufTheGame.Models.Game.Models.Obsticles
{
    class Boundary:Obsticle
    {
        public Boundary(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
        }

        public override string GetTexturePath()
        {
            return null;
        }
    }
}
