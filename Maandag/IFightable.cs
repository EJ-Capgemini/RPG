using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    interface IFightable {
        void Attack(Fightable f);

        void TakeDamage(int i);
    }

    public abstract class Fightable : IFightable {
        protected string name;
        protected int currentHealth;
        protected int maxHealth = 10;
        protected int accuracy = 80;
        protected int maxDamage = 3;

        //Attack een fightable object. Return true als de aanval schade heeft aangebracht.
        public void Attack(Fightable target) {
            Random rnd = new Random();
            //int hitRequirement = rnd.Next(0, 100); //indien 100 (exclusief), moet wel > i.p.v. >= gebruikt worden daarna.
            int hitRequirement = RandomUtil.Instance.GetRandomNumber(0, 100);
            //Console.WriteLine("hitRequirement: " + hitRequirement);
            if (accuracy > hitRequirement) {
                int damageDealt = rnd.Next(1, maxDamage + 1);                
                target.TakeDamage(damageDealt);
                Console.WriteLine("{0} inflicted {1} damage to {2}. (Current health {3}: {4}/{5})", name, damageDealt, target.name,
                    target.name, target.currentHealth, target.maxHealth);
                if(target.currentHealth <= 0) {
                    Console.WriteLine("--- {0} killed {1}! ---", name, target.name);
                }
            } else {
                Console.WriteLine("{0} failed to damage {1}. Accuracy {2}% < required {3}%", name, target.name, accuracy, hitRequirement);
            }
        }

        //Simpele functie die toegekende schade van target health afhaalt.
        public void TakeDamage(int damageTaken) {
            currentHealth = currentHealth > damageTaken ? currentHealth -= damageTaken : 0;
        }
    }
}
