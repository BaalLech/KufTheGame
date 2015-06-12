using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Game.Models;

namespace KufTheGame.Models.Interfaces
{
    interface IAttackable
    {
        BasicAttack Attack();

        void RespondToAttack(BasicAttack attack);
    }
}
