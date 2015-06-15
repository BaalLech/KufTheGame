using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Models.Interfaces;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Items
{
    public class Armor : Item
    {
        public Armor(int x, int y, int width, int height, Rarities rarity,
            Armors armorType, double defencePoints)
            : base(x, y, width, height, rarity)
        {
            this.ArmorType = armorType;
            this.DefencePoints = defencePoints;
        }

        public Armors ArmorType { get; set; }

        //TODO: ADD validation for negative
        public double DefencePoints { get; set; }

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

        public override void Use(Player target)
        {
            target.SetArmor(this);
        }
    }
}