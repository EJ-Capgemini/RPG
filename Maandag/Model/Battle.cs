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

        public Battle(List<Npc> foes) {
            this.player = Game.Instance.CurrentPlayer;
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
            int numberOfFoes = RandomUtil.Instance.GetRandomNumber(1, MAX_FOES + 1);

            //Uit de lijst een willekeurige vijand halen voor elk numberOfFoes.
            for(int i = 0; i < numberOfFoes; i++) {
                Npc foeClone = foes[RandomUtil.Instance.GetRandomNumber(0, foes.Count)].Clone();
                foeClone.Name += "[" + i + "]";
                randomFoes.Add(foeClone);
            }
                       
            return randomFoes;
        }

        public void Fight() {
            int foeIndex = 0;
            bool winner = false;

            while (!winner) {
                CurrentPlayer.Attack(Foes[foeIndex]);
                if (Foes[foeIndex].CurrentHealth <= 0) {
                    Foes.RemoveAt(foeIndex);
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
                LootItems();
            }

            Game.Instance.CurrentBattle = null;
        }

        //Wanneer de speler vlucht verliest hij een willekeurige getal aan health tussen 0 en gecombineerde maximale schade van foes.
        //Return getal dat van target health af gaat.
        public int Flee() {
            int combinedDamage = Foes.Sum(foe => foe.MaxDamage);
            int healthLost = RandomUtil.Instance.GetRandomNumber(0, combinedDamage - foes.Count + 1);

            CurrentPlayer.TakeDamage(healthLost);

            return healthLost;
        }

        public void LootItems() {
            int number = RandomUtil.Instance.GetRandomNumber(0, 4);
            switch (number) {
                case 0:
                    int healValue = 8;
                    int difference = Game.Instance.CurrentPlayer.MaxHealth - Game.Instance.CurrentPlayer.CurrentHealth;
                    if(difference <= healValue) {
                        Game.Instance.CurrentPlayer.CurrentHealth = Game.Instance.CurrentPlayer.MaxHealth;
                        Console.WriteLine("Reward: {0}'s health was restored by {1}, back to full.", Game.Instance.CurrentPlayer.DisplayName, difference);
                    } else {
                        Game.Instance.CurrentPlayer.CurrentHealth += healValue;
                        Console.WriteLine("Reward: {0}'s health was restored by {1}.", Game.Instance.CurrentPlayer.DisplayName, healValue);
                    }
                    break;
                case 1:
                    int damagencrease = 2;
                    Game.Instance.CurrentPlayer.MaxDamage += damagencrease;
                    Console.WriteLine("Reward: {0}'s maximum damage increased by {1}.", Game.Instance.CurrentPlayer.DisplayName, damagencrease);
                    break;
                case 2:
                    int healthIncrease = 4;
                    Game.Instance.CurrentPlayer.MaxHealth += healthIncrease;
                    Game.Instance.CurrentPlayer.CurrentHealth += healthIncrease;                    
                    Console.WriteLine("Reward: {0}'s health has increased by {1}.", Game.Instance.CurrentPlayer.DisplayName, healthIncrease);
                    break;
                case 3:
                default:
                    Console.WriteLine("Reward: {0} received a thumbs-up from the crowd!", Game.Instance.CurrentPlayer.DisplayName);
                    break;

            }
        }
    }
}