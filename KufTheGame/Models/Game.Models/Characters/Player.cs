using System;
using System.Linq;
using System.Collections.Generic;

using KufTheGame.Core;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Exceptions;
using KufTheGame.Models.Game.Models.Items;
using KufTheGame.Models.Interfaces;
using KufTheGame.Properties;

using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class Player : Character, IPlayer
    {
        private const int InitialLives = 3;
        private const double InitialAttackPoints = 20;
        private const double InitialDefencePoints = 100;
        private const double InitialHealthPoints = 50;
        private const float PlayerSpeed = 3;

        

        public Player(int x, int y, int width, int height, string name)
            : base(x, y, width, height, InitialAttackPoints, InitialDefencePoints, InitialHealthPoints)
        {
            this.Name = name;
            this.Lives = InitialLives;
            this.ArmorSet = new List<Armor>();
        }

        public Weapon Weapon { get; set; }

        public List<Armor> ArmorSet { get; set; }

        public string Name { get; set; }

        public int ImmortalDuration { get; set; }

        public override void ProduceSound()
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            IList<PressedKey> keys = KeyListener.GetKey();

            this.State = ((keys[0] != PressedKey.Null) && (keys[0] != PressedKey.Attack)) ? State.Moving :
                ((keys[0] == PressedKey.Null && this.State == State.Moving) ? State.Idle : this.State);

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

            if (!IsAttackKeyPressed())
            {
                return null;
            }

            this.State = (this.State == State.Idle || this.State == State.Moving) ? (State)(RandomGenerator.Randomize(2, 4)) :  this.State;

            return attack;
        }

        public override void RespondToAttack(BasicAttack attack)
        {
            if (ImmortalDuration > 0)
            {
                ImmortalDuration--;
                return;
            }

            attack.Hit(this);
        }

        public void SetWeapon(Weapon weapon)
        {
            try
            {
                if (this.Weapon == null)
                {
                    this.Weapon = weapon;
                }
                else
                {
                    throw new WeaponException("You already have weapon");
                }
            }
            catch (WeaponException)
            {
                if (this.Weapon.AttackPoints < weapon.AttackPoints)
                {
                    this.Weapon = weapon;
                }
            }
        }

        public void RemoveWeapon()
        {
            this.Weapon = null;
        }

        public void SetArmor(Armor armor)
        {
            try
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
            catch (ArmorException)
            {
                var currArmor = ArmorSet.Find(t => t.ArmorType == armor.ArmorType);
                if (currArmor.DefencePoints < armor.DefencePoints)
                {
                    this.ArmorSet.Remove(currArmor);
                    this.ArmorSet.Add(armor);
                }
            }
        }

        public double GetTotalArmor()
        {
            var armor = this.DefencePoints;
            this.ArmorSet.ForEach(a => armor += a.DefencePoints);

            return armor;
        }

        public override string GetTexturePath()
        {
            return Resources.Character_PlayerTexture;
        }

        public void RemoveArmor()
        {
            //this.Armor = null;
        }

        private static bool IsAttackKeyPressed()
        {
            var keys = KeyListener.GetKey();

            return (keys.Count() == 1) && (keys[0] == PressedKey.Attack);
        }
    }
}