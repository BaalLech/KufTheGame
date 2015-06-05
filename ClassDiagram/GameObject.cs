using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDiagram
{
    public abstract class GameObject : IDrawable, ISoundable
    {
        public int X
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Y
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Image
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
