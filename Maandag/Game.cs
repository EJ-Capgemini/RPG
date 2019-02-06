using Maandag.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public int PlayTime { get; set; } = 0;

        private static string LOAD_FILE_PATH = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\save_files\";

        private Game() { }

        public static Game Instance {
            get {
                if (instance == null) {
                    instance = new Game {
                        CurrentAreaIndex = 0,
                        Areas = new List<Area> {
                        new Area("Starting area", GetDummyData())                        
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
            CurrentBattle = foes.Count > 0 ? new Battle(foes) : null;

            return CurrentBattle;
        }    
        
        public void LevelUp() {
            CurrentPlayer.Level++;
            if (CurrentPlayer.Accuracy < 100) {
                CurrentPlayer.Accuracy++;
            }
            CurrentPlayer.CurrentHealth++;
            CurrentPlayer.MaxHealth++;
            Console.WriteLine("Congratulations, {0} has reached level {1}!", CurrentPlayer.DisplayName, CurrentPlayer.Level);
        }

        private static List<Npc> GetDummyData() {
            List<Npc> npcs = new List<Npc> {
                new Npc(true, 5, 30, 1, "Chicken"),
                new Npc(false, 1_000, 100, 50, "Bartender"),
                new Npc(true, 10, 40, 2, "Goblin"),
                new Npc(true, 5, 20, 2, "Rat"),
                new Npc(true, 10, 15, 4, "Ogre")
            };

            return npcs;
        }

        public Boolean Save(string filename = null) {            
            if (filename == null) {
                filename = "1";
            }


            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(LOAD_FILE_PATH + filename + ".txt"))
            using (JsonWriter writer = new JsonTextWriter(sw)) {
                serializer.Serialize(writer, instance);
            }

            return true;
        }

        public Boolean Load(string filename = null) {
            if (filename == null) {
                filename = "1";
            }

            using (StreamReader r = new StreamReader(LOAD_FILE_PATH + filename + ".txt")) {
                string json = r.ReadToEnd();
                Game loadedGame = JsonConvert.DeserializeObject<Game>(json);
                //Console.WriteLine("Test: " + loadedGame.CurrentPlayer.DisplayName);
                instance = loadedGame;
            }

            return true;
        }
    }
}
