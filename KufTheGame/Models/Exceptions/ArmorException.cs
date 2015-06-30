using System;

namespace KufTheGame.Models.Exceptions
{
    public class ArmorException : Exception
    {
        public ArmorException(string message)
            : base(message)
        {
        }
    }
}
