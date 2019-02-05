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
            int hitRequirement = RandomUtil.Instance.GetRandomNumber(0, 100);
            if (accuracy >= hitRequirement) {
                int damageDealt = RandomUtil.Instance.GetRandomNumber(1, maxDamage + 1);                
                target.TakeDamage(damageDealt);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine(@"{0} inflicted {1} damage to {2}    |   Accuracy {3}% vs required {4}%   |   Current health {5}: {6}/{7}"
                , name, damageDealt, target.name, accuracy, hitRequirement, target.name, target.currentHealth, target.maxHealth);
                Console.ResetColor();
                if (target.currentHealth <= 0) {
                    Console.WriteLine("--- {0} killed {1}! ---", name, target.name);
                }
            } else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(@"{0} failed to damage {1}        |   Accuracy {2}% vs required {3}%"
                , name, target.name, accuracy, hitRequirement);
                Console.ResetColor();
            }
        }

        //Simpele functie die toegekende schade van target health afhaalt.
        public void TakeDamage(int damageTaken) {
            currentHealth = currentHealth > damageTaken ? currentHealth -= damageTaken : 0;
        }
    }
}
