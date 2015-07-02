using KufTheGame.Models.Abstracts;
using KufTheGame.Properties;

namespace KufTheGame.Models.Game.Models.Characters
{
    public class StickmanNinja : Enemy
    {
        public StickmanNinja(int x, int y, int width, int height, double attackPoints, double defencePoints, double healthPoints)
            : base(x, y, width, height, attackPoints, defencePoints, healthPoints)
        {
        }

        public override string GetTexturePath()
        {
            return Resources.Character_StickEnemyTexture;
        }
    }
}