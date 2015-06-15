using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Items
{
    public class HealthPotion : Potion
    {
        private const string ImagePath = "";

        public HealthPotion(int x, int y, int width, int height, Rarities rarity, double health)
            : base(x, y, width, height, rarity)
        {
            this.Health = health;
        }

        public double Health { get; set; }

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

        public override void Use(Character target)
        {
            target.HealthPoints += this.Health;
            this.Health = 0;
        }

        // TODO: Remember to remove if health is 0
    }
}