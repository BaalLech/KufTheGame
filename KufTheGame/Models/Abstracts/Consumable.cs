using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Consumable : Item
    {
        protected Consumable(int x, int y, int width, int height, Rarities rarity)
            : base(x, y, width, height, rarity)
        {
        }

        public override abstract string GetTexturePath();
    }
}