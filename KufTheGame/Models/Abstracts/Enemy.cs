namespace KufTheGame.Models.Abstracts
{
    public abstract class Enemy: Character
    {
        protected Enemy(int x, int y, double attackPoints, double defencePoints, double healthPoints) : base(x, y, attackPoints, defencePoints, healthPoints)
        {
        }
    }
}