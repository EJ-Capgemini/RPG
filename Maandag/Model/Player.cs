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

        private int health;

        public Player(string email, string password, string displayName) {
            this.email = email;
            this.password = password;
            this.displayName = displayName;
        }

        public Player(string displayName) {
            this.displayName = displayName;
        }

        public int Health {
            get { return health; }
            set { health = value; }
        }

        public string DisplayName {
            get { return displayName; }
        }
    }
}
