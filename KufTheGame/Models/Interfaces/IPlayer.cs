using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Items;

namespace KufTheGame.Models.Interfaces
{
    public interface IPlayer
    {
        Weapon Weapon { get; set; }

        Armor Armor { get; set; }
    }
}