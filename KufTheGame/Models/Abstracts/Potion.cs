using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Potion: Consumable
    {
        protected Potion(int x, int y, Rarities rarity) : base(x, y, rarity)
        {
        }
    }
}