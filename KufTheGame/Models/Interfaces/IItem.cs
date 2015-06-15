using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;

namespace KufTheGame.Models.Interfaces
{
    public interface IItem
    {
        Rarities Rarity { get; set; }

        void Drop();

        void Use(Player target);
    }
}