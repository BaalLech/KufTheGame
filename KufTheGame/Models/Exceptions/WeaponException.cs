using System;

namespace KufTheGame.Models.Exceptions
{
    public class WeaponException : Exception
    {
        public WeaponException(string message)
            : base(message)
        {
        }
    }
}
