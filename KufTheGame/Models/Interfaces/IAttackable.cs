using KufTheGame.Models.Abstracts;

namespace KufTheGame.Models.Interfaces
{
    interface IAttackable
    {
        void Attack(Character target);

        void RespondToAttack();
    }
}
