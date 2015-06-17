using KufTheGame.Models.Enums;

namespace KufTheGame.Models.Interfaces
{
    interface IMoveable
    {
        void Move();
        void Move(BlockedDirections[] bdirections);
    }
}
