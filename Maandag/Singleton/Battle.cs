using Maandag.Model;
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
                Npc foeClone = foes[rnd.Next(0, foes.Count)].Clone();
                foeClone.Name += " (id:" + i + ")";
                randomFoes.Add(foeClone);
                //Npc foe = foes[rnd.Next(0, foes.Count)];
                //randomFoes.Add(foe);
            }
                       
            return randomFoes;
        }

        public void Fight() {
            int foeIndex = 0;
            bool winner = false;

            while (!winner) {
                CurrentPlayer.Attack(Foes[foeIndex]);
                if (Foes[foeIndex].CurrentHealth <= 0) {
                    //foeIndex++;
                    Foes.RemoveAt(foeIndex);
                    //Console.WriteLine("foeIndex: " + foeIndex);
                    if (foeIndex >= Foes.Count) {
                        winner = true;
                        break;
                    }
                }

                for (int i = 0; i < Foes.Count; i++) {
                    Foes[i].Attack(CurrentPlayer);
                    if (CurrentPlayer.CurrentHealth <= 0) {
                        winner = true;
                        break;
                    }
                }
            }

            if (CurrentPlayer.CurrentHealth <= 0) {
                //code wordt wel bereikt, maar niet correct uitgevoerd? Zelfde code in CommandHandler wél!
                Game.Instance.CurrentPlayer = null;
            } else {
                Game.Instance.LevelUp();                
            }
        }

        //Wanneer de speler vlucht verliest hij een willekeurige getal aan health tussen 0 en gecombineerde maximale schade van foes.
        //Return getal dat van target health af gaat.
        public int Flee() {
            int combinedDamage = Foes.Sum(foe => foe.MaxDamage);

            Random rnd = new Random();
            int healthLost = rnd.Next(0, combinedDamage + 1);

            CurrentPlayer.TakeDamage(healthLost);

            return healthLost;
        }
    }
}