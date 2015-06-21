using System;
using KufTheGame.Models.Interfaces;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Abstracts
{
    public abstract class GameObject : IGameObject, ISoundable
    {
        protected GameObject(int x, int y, int width, int height)
        {
            this.Velocity = new Vector2(x, y);
            this.Width = width;
            this.Height = height;
        }

        public Vector2 Velocity { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public abstract void ProduceSound();

        public abstract string GetTexturePath();

        public virtual bool Contains(GameObject target)
        {
            return target.Velocity.X >= this.Velocity.X &&
                   target.Velocity.X <= this.Velocity.X + this.Width &&
                   target.Velocity.Y >= this.Velocity.Y &&
                   target.Velocity.Y <= this.Velocity.Y + this.Height;
        }
    }
}