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
        private const double InitialAttackPoints = 20;
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
            throw new System.NotImplementedException();
        }

        public override void Move(BlockedDirections[] directions)
        {
            
            IList<PressedKey> keys = KeyListener.GetKey();

            switch (keys.Count())
            {
                case 1:
                    switch (keys[0])
                    {
                        case PressedKey.MoveUp:
                            if (!directions.Contains(BlockedDirections.BlockedUp))
                            {
                                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - Speed);
                            }
                            break;

                        case PressedKey.MoveDown:
                            if (!directions.Contains(BlockedDirections.BlockedDown))
                            this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + Speed);
                            break;

                        case PressedKey.MoveLeft:
                            if (!directions.Contains(BlockedDirections.BlockedLeft))
                            {
                                this.Velocity = new Vector2(this.Velocity.X - Speed, this.Velocity.Y);
                            }
                            break;

                        case PressedKey.MoveRight:
                            if (!directions.Contains(BlockedDirections.BlockedRight))

                            {
                                this.Velocity = new Vector2(this.Velocity.X + Speed, this.Velocity.Y);
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
                                    if (!directions.Contains(BlockedDirections.BlockedUp)&&!directions.Contains(BlockedDirections.BlockedLeft))
                                    this.Velocity = new Vector2(this.Velocity.X - Speed, this.Velocity.Y - Speed);
                                    break;

                                case PressedKey.MoveRight:
                                    if (!directions.Contains(BlockedDirections.BlockedUp) && !directions.Contains(BlockedDirections.BlockedRight))

                                    this.Velocity = new Vector2(this.Velocity.X + Speed, this.Velocity.Y - Speed);
                                    break;
                            }

                            break;

                        case PressedKey.MoveDown:
                            
                            switch (keys[1])
                            {
                                case PressedKey.MoveLeft:
                                    if (!directions.Contains(BlockedDirections.BlockedDown) && !directions.Contains(BlockedDirections.BlockedLeft))

                                    this.Velocity = new Vector2(this.Velocity.X - Speed, this.Velocity.Y + Speed);
                                    break;

                                case PressedKey.MoveRight:
                                    if (!directions.Contains(BlockedDirections.BlockedDown) && !directions.Contains(BlockedDirections.BlockedRight))

                                    this.Velocity = new Vector2(this.Velocity.X + Speed, this.Velocity.Y + Speed);
                                    break;
                            }

                            break;

                        case PressedKey.MoveLeft:
                            
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    if (!directions.Contains(BlockedDirections.BlockedLeft) && !directions.Contains(BlockedDirections.BlockedUp))

                                    this.Velocity = new Vector2(this.Velocity.X - Speed, this.Velocity.Y - Speed);
                                    break;

                                case PressedKey.MoveDown:
                                    if (!directions.Contains(BlockedDirections.BlockedLeft) && !directions.Contains(BlockedDirections.BlockedDown))

                                    this.Velocity = new Vector2(this.Velocity.X - Speed, this.Velocity.Y + Speed);
                                    break;
                            }

                            break;

                        case PressedKey.MoveRight:
                            
                            switch (keys[1])
                            {
                                case PressedKey.MoveUp:
                                    if (!directions.Contains(BlockedDirections.BlockedRight) && !directions.Contains(BlockedDirections.BlockedUp))

                                    this.Velocity = new Vector2(this.Velocity.X + Speed, this.Velocity.Y - Speed);
                                    break;

                                case PressedKey.MoveDown:
                                    if (!directions.Contains(BlockedDirections.BlockedRight) && !directions.Contains(BlockedDirections.BlockedDown))

                                    this.Velocity = new Vector2(this.Velocity.X + Speed, this.Velocity.Y + Speed);
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
            var damage = (attack.Damage * 2 / totalDef);
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

    }
}