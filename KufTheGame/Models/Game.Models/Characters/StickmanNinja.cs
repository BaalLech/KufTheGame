using KufTheGame.Models.Abstracts;
using KufTheGame.Properties;
using Microsoft.Xna.Framework;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class StickmanNinja : Enemy
    {
        public StickmanNinja(int x, int y, int width, int height, double attackPoints, double defencePoints, double healthPoints)
            : base(x, y, width, height, attackPoints, defencePoints, healthPoints)
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

        public override BasicAttack Attack()
        {
            var attack = new BasicAttack(this.AttackPoints);

            return attack;
        }

        public override void RespondToAttack(BasicAttack attack)
        {
            this.HealthPoints -= (attack.Damage * 2) / this.DefencePoints;
        }

        public override string GetTexturePath()
        {
            return Resources.Character_StickEnemyTexture;
        }
    }
}