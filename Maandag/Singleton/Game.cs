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
                    instance = new Game {
                        CurrentAreaIndex = 0,
                        Areas = new List<Area> {
                        new Area("Starting area") //dummy test area
                    }
                    };
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
        
        public void LevelUp() {
            CurrentPlayer.Level++;
            CurrentPlayer.Accuracy++;
            CurrentPlayer.CurrentHealth++;
            CurrentPlayer.MaxHealth++;
            Console.WriteLine("Congratulations, {0} has reached level {1}!", CurrentPlayer.DisplayName, CurrentPlayer.Level);
        }
    }
}
