using System.Collections.Generic;

namespace KufTheGame.Models.Interfaces
{
    public interface IEnemy
    {
        ICollection<IItem> Drops { get; }
    }
}
