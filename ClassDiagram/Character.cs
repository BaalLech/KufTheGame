using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDiagram
{
    public abstract class Character : GameObject, ICharacter, IMoveable, IAttackable
    {
        public int HealthPoints
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int AttackPoints
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int DefencePoints
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public ISet<Specialty> Specialties
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void AddSpecialty(Specialty specialty)
        {
            this.Specialties.Add(specialty);
        }

        public void RemoveSpecialty(Specialty specialty)
        {
            this.Specialties.Remove(specialty);
        }
    }
}
