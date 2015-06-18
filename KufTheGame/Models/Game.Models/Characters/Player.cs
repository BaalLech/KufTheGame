using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using KufTheGame.Core;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Exceptions;
using KufTheGame.Models.Game.Models.Items;
using KufTheGame.Models.Interfaces;
using KufTheGame.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class Player : Character, IPlayer
    {
        private const int InitialLives = 3;
        private const double InitialAttackPoints = 20;
        private const double InitialDefencePoints = 100;
        private const double InitialHealthPoints = 50;
        private const float PlayerSpeed = 3;

        

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


        public string Name { get; set; }

        public int ImmortalDuration { get; set; }

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

            this.State = (keys[0] == PressedKey.Null) ? State.Idle : State.Moving;

            switch (keys.Count())
            {
                case 1:
                    switch (keys[0])
                    {
                        case PressedKey.MoveUp:
                            if (!this.Directions.Contains(BlockedDirections.BlockedUp))
                            {
                                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - PlayerSpeed);
                            }
                            break;

                        case PressedKey.MoveDown:
                            if (!this.Directions.Contains(BlockedDirections.BlockedDown))
                            this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + PlayerSpeed);
                            break;

                        case PressedKey.MoveLeft:
                            this.SpriteRotation = 1;
                            if (!this.Directions.Contains(BlockedDirections.BlockedLeft))
                            {
                                this.Velocity = new Vector2(this.Velocity.X - PlayerSpeed, this.Velocity.Y);
                            }
                            break;

                        case PressedKey.MoveRight:
                            this.SpriteRotation = 0;
                            if (!this.Directions.Contains(BlockedDirections.BlockedRight))

                            {
                                this.Velocity = new Vector2(this.Velocity.X + PlayerSpeed, this.Velocity.Y);
                            }
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
                                    this.SpriteRotation = 1;
                                    if (!this.Directions.Contains(BlockedDirections.BlockedUp)&&!this.Directions.Contains(BlockedDirections.BlockedLeft))
                                    this.Velocity = new Vector2(this.Velocity.X - PlayerSpeed, this.Velocity.Y - PlayerSpeed);
                                    break;

                                case PressedKey.MoveRight:
                                    this.SpriteRotation = 0;
                                    if (!this.Directions.Contains(BlockedDirections.BlockedUp) && !this.Directions.Contains(BlockedDirections.BlockedRight))

                                    this.Velocity = new Vector2(this.Velocity.X + PlayerSpeed, this.Velocity.Y - PlayerSpeed);
                                    break;
                            }

                            break;

                        case PressedKey.MoveDown:
                            
                            switch (keys[1])
                            {
                                case PressedKey.MoveLeft:
                                    this.SpriteRotation = 1;
                                    if (!this.Directions.Contains(BlockedDirections.BlockedDown) && !this.Directions.Contains(BlockedDirections.BlockedLeft))

                                        this.Velocity = new Vector2(this.Velocity.X - PlayerSpeed, this.Velocity.Y + PlayerSpeed);
                                    break;

                                case PressedKey.MoveRight:
                                    this.SpriteRotation = 0;
                                    if (!this.Directions.Contains(BlockedDirections.BlockedDown) && !this.Directions.Contains(BlockedDirections.BlockedRight))

                                        this.Velocity = new Vector2(this.Velocity.X + PlayerSpeed, this.Velocity.Y + PlayerSpeed);
                                    break;
                            }

                            break;

                        case PressedKey.MoveLeft:
                            this.SpriteRotation = 1;
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    if (!this.Directions.Contains(BlockedDirections.BlockedLeft) && !this.Directions.Contains(BlockedDirections.BlockedUp))

                                        this.Velocity = new Vector2(this.Velocity.X - PlayerSpeed, this.Velocity.Y - PlayerSpeed);
                                    break;

                                case PressedKey.MoveDown:
                                    if (!this.Directions.Contains(BlockedDirections.BlockedLeft) && !this.Directions.Contains(BlockedDirections.BlockedDown))

                                        this.Velocity = new Vector2(this.Velocity.X - PlayerSpeed, this.Velocity.Y + PlayerSpeed);
                                    break;
                            }

                            break;

                        case PressedKey.MoveRight:
                            this.SpriteRotation = 0;
                            
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    if (!this.Directions.Contains(BlockedDirections.BlockedRight) && !this.Directions.Contains(BlockedDirections.BlockedUp))

                                        this.Velocity = new Vector2(this.Velocity.X + PlayerSpeed, this.Velocity.Y - PlayerSpeed);
                                    break;

                                case PressedKey.MoveDown:
                                    if (!this.Directions.Contains(BlockedDirections.BlockedRight) && !this.Directions.Contains(BlockedDirections.BlockedDown))

                                        this.Velocity = new Vector2(this.Velocity.X + PlayerSpeed, this.Velocity.Y + PlayerSpeed);
                                    break;
                            }

                            break;
                    }
                    break;

            }
        }

        public override BasicAttack Attack()
        {
            var wepDmg = 0d;
            if (this.Weapon != null)
            {
                wepDmg = this.Weapon.AttackPoints;
            }
            var attack = new BasicAttack(this.AttackPoints + wepDmg);

            if (this.IsAttackKeyPressed())
            {

                return attack;
            }

            return null;
        }

        public override void RespondToAttack(BasicAttack attack)
        {
            if (ImmortalDuration > 0)
            {
                ImmortalDuration--;
                return;
            }

            var totalDef = this.DefencePoints + this.ArmorSet.Sum(a => a.DefencePoints);
            double damage = (attack.Damage * 2 / totalDef);
            this.HealthPoints -= damage;
            if (this.HealthPoints <= 0)
            {
                this.RemoveLive();
                this.HealthPoints = this.BaseHealthPoints;
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

        public override string GetTexturePath()
        {
            return Resources.Character_PlayerTexture;
        }
    }
}