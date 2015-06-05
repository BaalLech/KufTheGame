using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDiagram
{
    public abstract class Item : GameObject
    {
        public int Rarity
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public abstract double Drop();
    }
}
