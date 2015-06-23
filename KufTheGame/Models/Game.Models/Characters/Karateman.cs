using KufTheGame.Properties;
using KufTheGame.Models.Abstracts;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class Karateman : Enemy
    {
        public Karateman(int x, int y, int width, int height, double attackPoints, double defencePoints, double healthPoints)
            : base(x, y, width, height, attackPoints, defencePoints, healthPoints)
        {

        }

        public override void ProduceSound()
        {
            throw new System.NotImplementedException();
        }

        public override string GetTexturePath()
        {
            return Resources.Character_KarateEnemyTexture;
        }
    }
}