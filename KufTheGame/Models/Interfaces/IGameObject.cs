using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KufTheGame.Models.Abstracts;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Interfaces
{
    interface IGameObject
    {
        Vector2 Velocity { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        bool Contains(GameObject target);

        string GetTexturePath();
    }
}
