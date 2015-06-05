using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDiagram
{
    public interface ICharacter
    {
        int HealthPoints { get; set; }

        int AttackPoints { get; set; }

        int DefencePoints { get; set; }

        ISet<Specialty> Specialties { get; set; }
    }
}
