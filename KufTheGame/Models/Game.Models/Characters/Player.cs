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
            KeyListener.GetKey();
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