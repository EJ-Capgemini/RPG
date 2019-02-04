using Maandag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    class Game {        
        private static Game instance;

        public Player CurrentPlayer { get; set; }
        public List<Area> Areas { get; set; }
        public int CurrentAreaIndex { get; set; }
        public Battle CurrentBattle { get; set; }
        public Boolean Active { get; set; } = false;

        private Game() { }

        public static Game Instance {
            get {
                if (instance == null) {
                    instance = new Game();
                    instance.CurrentAreaIndex = 0;
                    instance.Areas = new List<Area>();
                    instance.Areas.Add(new Area("Starting area"));
                }
                return instance;
            }
        }

        public Battle RandomEncounter() {
            //LINQ om lijst te realiseren met alleen aanvalbare npc's.
            List<Npc> foes = Areas[CurrentAreaIndex].Npcs.Where(npc => npc.Attackable).ToList();

            //Controleren of er minstens 1 vijand is in deze area.
            CurrentBattle = foes.Count > 0 ? new Battle(CurrentPlayer, foes) : null;

            return CurrentBattle;
        }

        //Deze 2 verder uitwerken!
        public void FightBattle(Battle battle) {
            Random rnd = new Random();
            int healthLost = rnd.Next(3, 5 + 1);

            CurrentPlayer.CurrentHealth -= healthLost;
        }

        public void FleeFromBattle(Battle battle) {
            Random rnd = new Random();
            int healthLost = rnd.Next(3, 5 + 1);

            CurrentPlayer.CurrentHealth -= healthLost;
        }
    }
}
