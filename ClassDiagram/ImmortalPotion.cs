using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDiagram
{
    public class ImmortalPotion : Potion
    {
        public int Duration
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
