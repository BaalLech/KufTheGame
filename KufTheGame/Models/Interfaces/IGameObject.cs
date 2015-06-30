using KufTheGame.Models.Abstracts;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Interfaces
{
    public interface IGameObject
    {
        Vector2 Velocity { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        bool Contains(GameObject target);

        string GetTexturePath();
    }
}
