using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Consumable: Item
    {
        protected Consumable(int x, int y, Rarities rarity) : base(x, y, rarity)
        {
        }
    }
}