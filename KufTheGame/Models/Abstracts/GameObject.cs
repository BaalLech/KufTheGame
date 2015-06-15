﻿using System;
using KufTheGame.Models.Interfaces;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Abstracts
{
    public abstract class GameObject : IGameObject, IDrawable, ISoundable
    {
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        protected GameObject(int x, int y, int width, int height)
        {
            this.Velocity = new Vector2(x, y);
            this.Width = width;
            this.Height = height;
        }

        public Vector2 Velocity { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public abstract int DrawOrder { get; }

        public abstract bool Visible { get; }

        // Image

        public abstract void Draw(GameTime gameTime);

        public abstract void ProduceSound();

        public virtual bool Intersects(GameObject target)
        {
            //if (this.contains(shape.x - offset, shape.y - offset) ||
            //this.contains(shape.x + shape.width - offset, shape.y - offset) ||
            //this.contains(shape.x - offset, shape.y + shape.height - offset) ||
            //this.contains(shape.x + shape.width - offset, shape.y + shape.height - offset))
            //{
            //    return true;
            //}
            //else if (shape.contains(this.x - offset, this.y - offset) ||
            //  shape.contains(this.x + this.width - offset, this.y - offset) ||
            //  shape.contains(this.x - offset, this.y + this.height - offset) ||
            //  shape.contains(this.x + this.width - offset, this.y + this.height - offset))
            //{
            //    return true;
            //}
            //return false;

            return true;
        }

        public virtual bool Contains(GameObject target)
        {
            if (target.Velocity.X >= this.Velocity.X && 
                target.Velocity.X <= this.Velocity.X + this.Width &&
                target.Velocity.Y >= this.Velocity.Y &&
                target.Velocity.Y <= this.Velocity.Y + this.Height)
            {
                return true;
            }

            return false;
        }
    }
}