using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using KufTheGame.Core;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Exceptions;
using KufTheGame.Models.Game.Models.Items;
using KufTheGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class Player : Character, IPlayer
    {
        private const int InitialLives = 3;
        private const double InitialAttackPoints = 2;
        private const double InitialDefencePoints = 0;
        private const double InitialHealthPoints = 50;

        public Player(Texture2D playerTexture, int x, int y, int width, int height, string name)
            : base(x, y, width, height, InitialAttackPoints, InitialDefencePoints, InitialHealthPoints)
        {
            this.Name = name;
            this.Lives = InitialLives;
            this.ArmorSet = new List<Armor>();
            this.Texture = playerTexture;
        }

        public Texture2D Texture { get; set; }

        public Weapon Weapon { get; set; }

        public List<Armor> ArmorSet { get; set; }

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
                            this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - 1);
                            break;

                        case PressedKey.MoveDown:
                            this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + 1);
                            break;

                        case PressedKey.MoveLeft:
                            this.Velocity = new Vector2(this.Velocity.X - 1, this.Velocity.Y);
                            break;

                        case PressedKey.MoveRight:
                            this.Velocity = new Vector2(this.Velocity.X + 1, this.Velocity.Y);
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
                                    this.Velocity = new Vector2(this.Velocity.X - 1, this.Velocity.Y - 1);
                                    break;

                                case PressedKey.MoveRight:
                                    this.Velocity = new Vector2(this.Velocity.X + 1, this.Velocity.Y - 1);
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
                                    this.Velocity = new Vector2(this.Velocity.X - 1, this.Velocity.Y - 1);
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


        public override BasicAttack Attack()
        {
            //var attack = new BasicAttack(this.AttackPoints + this.Weapon.AttackPoints);
            var attack = new BasicAttack(50 + 50);

            if (this.IsAttackKeyPressed())
            {

                return attack;
            }
            return null;
        }

        public override void RespondToAttack(BasicAttack attack)
        {
            var totalDef = this.DefencePoints + this.ArmorSet.Sum(a => a.DefencePoints);
            var damage = (attack.Damage * 2 / totalDef);
            this.HealthPoints -= damage;
            if (this.HealthPoints < 0)
            {
                this.HealthPoints = 0;
            }
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
            if (ArmorSet.All(t => t.ArmorType != armor.ArmorType))
            {
                this.ArmorSet.Add(armor);
            }
            else
            {
                throw new ArmorException("You already have this armor type!");
            }
        }

        public void RemoveArmor()
        {
            //this.Armor = null;
        }

        public void UsePotion(Potion potion)
        {
            potion.Use(this);
        }

        public void AddLive()
        {
            this.Lives++;
        }

        public void RemoveLive()
        {
            this.Lives--;
        }


        private bool IsAttackKeyPressed()
        {
            IList<PressedKey> keys = KeyListener.GetKey();

            switch (keys.Count())
            {
                case 1:
                    switch (keys[0])
                    {
                        case PressedKey.Attack:
                            return true;
                    }
                    break;
            }
            return false;
        }

    }
}