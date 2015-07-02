namespace KufTheGame.Models.Abstracts
{
    public abstract class Obsticle : GameObject
    {
        protected Obsticle(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
        }

        public override abstract string GetTexturePath();
    }
}