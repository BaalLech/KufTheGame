using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Models.Interfaces;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Item : GameObject, IItem
    {
        protected Item(int x, int y, int width, int height, Rarities rarity)
            : base(x, y, width, height)
        {
            this.Rarity = rarity;
        }

        public Rarities Rarity { get; set; }

        public void Drop()
        {
            KufTheGame.Drops.Add(this);
        }

        public abstract void Use(Player target);

        public override abstract string GetTexturePath();
    }
}