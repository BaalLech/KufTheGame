using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
