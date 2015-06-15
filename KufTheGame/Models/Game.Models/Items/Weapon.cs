using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Items
{
    public class Weapon: Item
    {
        public const string ImagePath = "";

        public Weapon(int x, int y, int width, int height, Rarities rarity,
            Weapons weaponType, double attackPoints) : base(x, y,width,height, rarity)
        {
            this.WeaponType = weaponType;
            this.AttackPoints = attackPoints;
        }

        public Weapons WeaponType { get; set; }

        //TODO: ADD validation for negative
        public double AttackPoints { get; set; }

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
    }
}