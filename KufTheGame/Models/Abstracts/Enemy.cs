namespace KufTheGame.Models.Abstracts
{
    public abstract class Enemy : Character
    {
        protected Enemy(int x, int y, double attackPoints, double defencePoints, double healthPoints)
            : base(x, y, attackPoints, defencePoints, healthPoints)
        {
        }

        public override void Move()
        {
            
        }

        public override void Attack(Character target)
        {
            throw new System.NotImplementedException();
        }

        public override void RespondToAttack()
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

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void ProduceSound()
        {
            throw new System.NotImplementedException();
        }
    }
}