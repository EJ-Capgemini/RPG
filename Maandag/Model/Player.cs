using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag.Model {
    class Player {
        private readonly string email;
        private readonly string password;
        private readonly string displayName;

        private int currentHealth;
        private int maxHealth = 10;
        private float accuracy = 80.0f;
        private int maxDamage = 3;

        public Player(string email, string password, string displayName) {
            this.email = email;
            this.password = password;
            this.displayName = displayName;
            currentHealth = maxHealth;
        }

        public Player(string displayName) {
            this.displayName = displayName;
            currentHealth = maxHealth;
        }

        public int MaxHealth {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public int CurrentHealth {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public float Accuracy {
            get { return accuracy; }
            set { accuracy = value; }
        }

        public int MaxDamage {
            get { return maxDamage; }
            set { maxDamage = value; }
        }

        public string DisplayName {
            get { return displayName; }
        }
    }
}
