using System.Collections.Generic;
using System.Linq;
using KufTheGame.Core;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Items;
using KufTheGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class Player: Character, IPlayer
    {
        private const double InitialAttackPoints = 2;
        private const double InitialDefencePoints = 0;
        private const double InitialHealthPoints = 100;

        public Player(Texture2D playerTexture, int x, int y, string name, int lives)
            : base(x, y, InitialAttackPoints, InitialDefencePoints, InitialHealthPoints)
        {
            this.Name = name;
            this.Lives = lives;
            this.Texture = playerTexture;
        }

        public Texture2D Texture { get; set; }

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
                            this.Velocity=new Vector2(this.Velocity.X,this.Velocity.Y-1);
                            break;

                        case PressedKey.MoveDown:
                            this.Velocity=new Vector2(this.Velocity.X,this.Velocity.Y+1);
                            break;

                        case PressedKey.MoveLeft:
                            this.Velocity = new Vector2(this.Velocity.X-1, this.Velocity.Y);
                            break;

                        case PressedKey.MoveRight:
                            this.Velocity=new Vector2(this.Velocity.X+1,this.Velocity.Y);
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
                                    this.Velocity = new Vector2(this.Velocity.X-1, this.Velocity.Y-1);
                                    break;

                                case PressedKey.MoveRight:
                                    this.Velocity = new Vector2(this.Velocity.X+1, this.Velocity.Y-1);
                                    break;
                            }
                            
                            break;

                        case PressedKey.MoveDown:
                            switch (keys[1])
                            {
                                case PressedKey.MoveLeft:
                                    this.Velocity = new Vector2(this.Velocity.X - 1, this.Velocity.Y + 1);
                                    break;

                                case PressedKey.MoveRight:
                                    this.Velocity = new Vector2(this.Velocity.X + 1, this.Velocity.Y + 1);
                                    break;
                            }
                           
                            break;

                        case PressedKey.MoveLeft:
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    this.Velocity= new Vector2(this.Velocity.X - 1, this.Velocity.Y - 1);
                                    break;

                                case PressedKey.MoveDown:
                                    this.Velocity = new Vector2(this.Velocity.X - 1, this.Velocity.Y + 1);
                                    break;
                            }
                            
                            break;

                        case PressedKey.MoveRight:
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    this.Velocity = new Vector2(this.Velocity.X + 1, this.Velocity.Y - 1);
                                    break;

                                case PressedKey.MoveDown:
                                    this.Velocity = new Vector2(this.Velocity.X + 1, this.Velocity.Y + 1);
                                    break;
                            }
                            
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