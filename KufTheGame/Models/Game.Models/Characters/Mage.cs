using KufTheGame.Models.Abstracts;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class Mage: Enemy
    {
        private const string ImagePath = "";

        public Mage(int x, int y, double attackPoints, double defencePoints, double healthPoints) : base(x, y, attackPoints, defencePoints, healthPoints)
        {
 
        }

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

        public override BasicAttack Attack()
        {
            var attack = new BasicAttack(this.AttackPoints);

            return attack;
        }

        public override void RespondToAttack(BasicAttack attack)
        {
            this.HealthPoints -= (this.DefencePoints - attack.Damage * 2);
        }
    }
}