using System.Collections.Generic;

namespace KufTheGame.Models.Interfaces
{
    interface IEnemy
    {
        ICollection<IItem> Drops { get; }
    }
}
