using System;

namespace KufTheGame.Models.Exceptions
{
    class WeaponException : Exception
    {
        public WeaponException(string message)
            : base(message)
        {
        }
    }
}
