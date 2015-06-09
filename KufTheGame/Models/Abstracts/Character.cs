﻿using System.Collections.Generic;
using KufTheGame.Models.Game.Models;
using KufTheGame.Models.Interfaces;

namespace KufTheGame.Models.Abstracts
{
    public abstract class Character : GameObject, IMoveable, IAttackable, ICharacter
    {
        protected Character(int x, int y, double attackPoints, double defencePoints,
            double healthPoints)
            : base(x, y)
        {
            this.AttackPoints = attackPoints;
            this.DefencePoints = defencePoints;
            this.HealthPoints = healthPoints;
        }

        // TODO : Validate for negative values;
        public double AttackPoints { get; set; }

        public double DefencePoints { get; set; }

        public double HealthPoints { get; set; }

        public IList<Specialty> Specialties { get; set; }
        public void RemoveHp(double hp)
        {
            this.HealthPoints -= hp;
            if (this.HealthPoints < 0)
            {
                this.HealthPoints = 0;
            }
        }

        public bool IsAlive()
        {
            if (this.HealthPoints == 0)
            {
                return false;
            }

            return true;
        }

        public abstract void Move();

        public abstract void Attack(Character target);

        public abstract void RespondToAttack();

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