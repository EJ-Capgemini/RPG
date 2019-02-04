using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag.Model {
    class Npc {
        private Boolean attackable;
        private int health;
        private float accuracy;
        private int maxDamage;
        private string name;

        public Npc(bool attackable, int health, float accuracy, int maxDamage, string name) {
            this.attackable = attackable;
            this.health = health;
            this.accuracy = accuracy;
            this.maxDamage = maxDamage;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public int MaxDamage {
            get { return maxDamage; }
            set { maxDamage = value; }
        }

        public float Accuracy {
            get { return accuracy; }
            set { accuracy = value; }
        }

        public int Health {
            get { return health; }
            set { health = value; }
        }

        public Boolean Attackable {
            get { return attackable; }
            set { attackable = value; }
        }

    }
}
