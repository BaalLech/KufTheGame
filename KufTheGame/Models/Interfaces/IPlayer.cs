using System.Collections.Generic;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Items;

namespace KufTheGame.Models.Interfaces
{
    public interface IPlayer
    {
        Weapon Weapon { get; set; }

        List<Armor> ArmorSet { get; set; }
    }
}