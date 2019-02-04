using Maandag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    class Game {
        private Player currentPlayer;

        public Player CurrentPlayer {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

    }
}
