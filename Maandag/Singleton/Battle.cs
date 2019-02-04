using Maandag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    class Battle {
        private Player player;
        private Npc[] foes;

        public Battle(Player player, Npc[] foes) {
            this.player = player;
            this.foes = foes;
        }

        public Player CurrentPlayer {
            get { return player; }
            set { player = value; }
        }

        public Npc[] Foes {
            get { return foes; }
        }
    }
}