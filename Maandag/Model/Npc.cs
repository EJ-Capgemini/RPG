using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag.Model {
    class Npc:Fightable {
        private Boolean attackable;
              
        public Npc(bool attackable, int health, int accuracy, int maxDamage, string name) {
            this.attackable = attackable;
            maxHealth = health;
            currentHealth = maxHealth;
            this.accuracy = accuracy;
            this.maxDamage = maxDamage;
            this.name = name;
        }

        public Npc Clone() { return (Npc)this.MemberwiseClone(); }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public int MaxDamage {
            get { return maxDamage; }
            set { maxDamage = value; }
        }

        public int Accuracy {
            get { return accuracy; }
            set { accuracy = value; }
        }

        public int MaxHealth {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public int CurrentHealth {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public Boolean Attackable {
            get { return attackable; }
            set { attackable = value; }
        }

    }
}
