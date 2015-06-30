using System.Collections.Generic;
using KufTheGame.Models.Enums;
using Microsoft.Xna.Framework.Input;

namespace KufTheGame.Core
{
    public static class KeyListener
    {
        public static IList<PressedKey> GetKey()   
        {
            var keyboardState = Keyboard.GetState();

            var keys = new List<PressedKey>();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                var key = PressedKey.Attack;
                keys.Add(key);
                return keys;
            }
            
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                var key = PressedKey.MoveUp;
                keys.Add(key);
                if (keys.Count == 2)
                {
                    return keys;
                }
            }

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                var key = PressedKey.MoveDown;
                keys.Add(key);
                if (keys.Count == 2)
                {
                    return keys;
                }
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                var key = PressedKey.MoveLeft;
                keys.Add(key);
                if (keys.Count == 2)
                {
                    return keys;
                }
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                var key = PressedKey.MoveRight;
                keys.Add(key);
                if (keys.Count == 2)
                {
                    return keys;
                }
            }

            return keys.Count == 1 ? keys : new List<PressedKey>()
            {
                PressedKey.Null
            };
        }
    }
}
