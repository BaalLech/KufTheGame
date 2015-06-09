using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Interfaces
{
    public interface IItem
    {
        Rarities Rarity { get; set; }

        void Drop();
    }
}