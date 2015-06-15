using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Potion : Consumable
    {
        protected Potion(int x, int y, int width, int height, Rarities rarity)
            : base(x, y, width, height, rarity)
        {
        }
    }
}