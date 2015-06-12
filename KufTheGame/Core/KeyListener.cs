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
        public static IList<PressedKey> GetKey()   
        {
            var keyboardState = Keyboard.GetState();
            
            var keys = new List<PressedKey>();
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                var key1= PressedKey.MoveUp;
                keys.Add(key1);
                if (keys.Count == 2)
                {
                    return keys;
                }

                
            }

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                var key1 = PressedKey.MoveDown;
                keys.Add(key1);
                if (keys.Count == 2)
                {
                    return keys;
                }
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                var key1 = PressedKey.MoveLeft;
                keys.Add(key1);
                if (keys.Count == 2)
                {
                    return keys;
                }
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                var key1 = PressedKey.MoveRight;
                keys.Add(key1);
                if (keys.Count == 2)
                {
                    return keys;
                }
            }
            if (keys.Count == 1)
            {
                return keys;
            }
            return new List<PressedKey>(){PressedKey.Null};
        }
    }
}
