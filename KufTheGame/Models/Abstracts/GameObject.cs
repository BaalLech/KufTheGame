﻿using System;
using KufTheGame.Models.Interfaces;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Abstracts
{
    public abstract class GameObject : IDrawable, ISoundable
    {
        private Vector2 velocity;
        protected GameObject(int x, int y)
        {
            this.Velocity = new Vector2(x, y);
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public abstract int DrawOrder
        {
            get;
        }

        public abstract bool Visible
        {
            get;

        }


        // Image

        public abstract void Draw(GameTime gameTime);

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public abstract void ProduceSound();
    }
}