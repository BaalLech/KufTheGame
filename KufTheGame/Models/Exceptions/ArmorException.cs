using System;

namespace KufTheGame.Models.Exceptions
{
    class ArmorException : Exception
    {
        public ArmorException(string message)
            : base(message)
        {
        }
    }
}
