using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
