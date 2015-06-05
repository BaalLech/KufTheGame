using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDiagram
{
    public class HealthPotion : Potion
    {
        public int Health
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public override double Drop()
        {
            throw new NotImplementedException();
        }
    }
}
