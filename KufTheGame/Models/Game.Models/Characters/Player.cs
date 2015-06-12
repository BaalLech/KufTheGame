using System.Collections.Generic;
using System.Linq;
using KufTheGame.Core;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Items;
using KufTheGame.Models.Interfaces;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class Player: Character, IPlayer
    {
        public Player(int x, int y, string name, int lives,
            double attackPoints, double defencePoints, double healthPoints) : base(x, y, attackPoints, defencePoints, healthPoints)
        {
            this.Name = name;
            this.Lives = lives;
        }

        public Weapon Weapon { get; set; }

        public Armor Armor { get; set; }

        public int Lives { get; set; }

        public string Name { get; set; }

        public override void Draw(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void ProduceSound()
        {
            throw new System.NotImplementedException();
        }

        public override int DrawOrder
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool Visible
        {
            get { throw new System.NotImplementedException(); }
        }

        public override void Move()
        {
            IList<PressedKey> keys = KeyListener.GetKey();

            switch (keys.Count())
            {
                case 1:
                    switch (keys[0])
                    {
                        case PressedKey.MoveUp:
                            this.Velocity.Y--;
                            
                            break;

                        case PressedKey.MoveDown:
                            this.Velocity.Y++;
                            break;

                        case PressedKey.MoveLeft:
                            this.Velocity.X--;
                            break;

                        case PressedKey.MoveRight:
                            this.Velocity.X++;
                            break;
                    }
                    break;
                case 2:
                    switch (keys[0])
                    {
                        case PressedKey.MoveUp:
                            switch (keys[1])
                            {
                                case PressedKey.MoveLeft:
                                    this.Velocity.X--;
                                    break;

                                case PressedKey.MoveRight:
                                    this.Velocity.X++;
                                    break;
                            }
                            this.Velocity.Y--;
                            break;

                        case PressedKey.MoveDown:
                            switch (keys[1])
                            {
                                case PressedKey.MoveLeft:
                                    this.Velocity.X--;
                                    break;

                                case PressedKey.MoveRight:
                                    this.Velocity.X++;
                                    break;
                            }
                            this.Velocity.Y++;
                            break;

                        case PressedKey.MoveLeft:
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    this.Velocity.Y--;
                                    break;

                                case PressedKey.MoveDown:
                                    this.Velocity.Y++;
                                    break;
                            }
                            this.Velocity.X--;
                            break;

                        case PressedKey.MoveRight:
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    this.Velocity.Y--;
                                    break;

                                case PressedKey.MoveDown:
                                    this.Velocity.Y++;
                                    break;
                            }
                            this.Velocity.X++;
                            break;
                    }
                    break;
                    
            }
        }

        public override void Attack(Character target)
        {
            throw new System.NotImplementedException();
        }

        public override void RespondToAttack()
        {
            throw new System.NotImplementedException();
        }

        public void SetWeapon(Weapon weapon)
        {
            this.Weapon = weapon;
        }

        public void RemoveWeapon()
        {
            this.Weapon = null;
        }

        public void SetArmor(Armor armor)
        {
            this.Armor = armor;
        }

        public void RemoveArmor()
        {
            this.Armor = null;
        }

        public void AddLive()
        {
            this.Lives++;
        }

        public void RemoveLive()
        {
            this.Lives--;
        }
    }
}