﻿using Maandag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    class Battle {
        private Player player;
        private List<Npc> foes;

        private static int MAX_FOES = 3;

        public Battle(Player player, List<Npc> foes) {
            this.player = player;
            this.foes = RandomFoes(foes);
        }

        public Player CurrentPlayer {
            get { return player; }
            set { player = value; }
        }

        public List<Npc> Foes {
            get { return foes; }
        }

        override
        public string ToString() {
            List<String> names = foes.Select(foe => foe.Name).ToList();
            return String.Join(", ", names);
        }

        private List<Npc> RandomFoes(List<Npc> foes) {
            List<Npc> randomFoes = new List<Npc>();

            //hoeveel vijanden
            Random rnd = new Random();
            int numberOfFoes = rnd.Next(1, MAX_FOES + 1);

            //Uit de lijst een willekeurige vijand halen voor elk numberOfFoes.
            for(int i = 0; i < numberOfFoes; i++) {
                randomFoes.Add(foes[rnd.Next(0, foes.Count)]);
            }
                       
            return randomFoes;
        }
    }
}