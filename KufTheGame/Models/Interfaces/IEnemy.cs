using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KufTheGame.Models.Interfaces
{
    interface IEnemy
    {
        ICollection<IItem> Drops { get; }
    }
}
