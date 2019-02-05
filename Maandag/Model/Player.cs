using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag.Model {
    class Player : Fightable {
        readonly int BASE_HEALTH = 10;
        readonly int BASE_ACCURACY = 80;
        readonly int BASE_MAX_DAMAGE = 3;

        public int Level { get; set; } = 1;

        public Player(string displayName) {
            this.name = displayName;
            maxHealth = BASE_HEALTH;
            currentHealth = maxHealth;
            accuracy = BASE_ACCURACY;
            maxDamage = BASE_MAX_DAMAGE;
        }

        public int MaxHealth {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public int CurrentHealth {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public int Accuracy {
            get { return accuracy; }
            set { accuracy = value; }
        }

        public int MaxDamage {
            get { return maxDamage; }
            set { maxDamage = value; }
        }

        public string DisplayName {
            get { return name; }
        }
    }
}
