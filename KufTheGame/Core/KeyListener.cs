using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KufTheGame.Models.Enums;
using Microsoft.Xna.Framework.Input;

namespace KufTheGame.Core
{
    public static class KeyListener
    {
        public static PressedKey GetKey()
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                return PressedKey.MoveUp;
            }

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                return PressedKey.MoveDown;
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                return PressedKey.MoveLeft;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                return PressedKey.MoveRight;
            }

            return PressedKey.Null;
        }
    }
}
