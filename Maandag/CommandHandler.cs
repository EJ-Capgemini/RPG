using Maandag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Maandag {
    class CommandHandler {
        private static CommandHandler instance;

        private CommandHandler() { }

        public static CommandHandler Instance {
            get {
                if (instance == null) {
                    instance = new CommandHandler();
                }
                return instance;
            }
        }

        public void Start() {
            ShowIntro();
            AskAndHandleInput();
        }

        void ShowIntro() {
            Console.WriteLine("WELCOME TO....");
            //Thread.Sleep(1500); //spannend...
            Console.WriteLine(@"
            
             █     █░ ▒█████  ▓█████  ███▄    █   ██████ ▓█████▄  ▄▄▄        ▄████ 
            ▓█░ █ ░█░▒██▒  ██▒▓█   ▀  ██ ▀█   █ ▒██    ▒ ▒██▀ ██▌▒████▄     ██▒ ▀█▒
            ▒█░ █ ░█ ▒██░  ██▒▒███   ▓██  ▀█ ██▒░ ▓██▄   ░██   █▌▒██  ▀█▄  ▒██░▄▄▄░
            ░█░ █ ░█ ▒██   ██░▒▓█  ▄ ▓██▒  ▐▌██▒  ▒   ██▒░▓█▄   ▌░██▄▄▄▄██ ░▓█  ██▓
            ░░██▒██▓ ░ ████▓▒░░▒████▒▒██░   ▓██░▒██████▒▒░▒████▓  ▓█   ▓██▒░▒▓███▀▒
            ░ ▓░▒ ▒  ░ ▒░▒░▒░ ░░ ▒░ ░░ ▒░   ▒ ▒ ▒ ▒▓▒ ▒ ░ ▒▒▓  ▒  ▒▒   ▓▒█░ ░▒   ▒ 
              ▒ ░ ░    ░ ▒ ▒░  ░ ░  ░░ ░░   ░ ▒░░ ░▒  ░ ░ ░ ▒  ▒   ▒   ▒▒ ░  ░   ░ 
              ░   ░  ░ ░ ░ ▒     ░      ░   ░ ░ ░  ░  ░   ░ ░  ░   ░   ▒   ░ ░   ░ 
                ░        ░ ░     ░  ░         ░       ░     ░          ░  ░      ░ 
                                                          ░                        
       
            ");
            //Thread.Sleep(500);
            Console.WriteLine("Type !commands to see the full list of commands. Good luck and have fun.");
        }

        void AskAndHandleInput(string input = null) {
            if(input == null) {
                input = Console.ReadLine();
            }

            //Dit oogt omslachtig... maar werkt wel.
            input = Regex.Replace(input, @"\s+", " "); //meerdere spaties achter elkaar, tabs etc vervangen door 1 spatie
            List<String> extraParameters = input.Split(' ').ToList(); //op spatie splitsen naar lijst
            input = extraParameters[0];
            extraParameters.RemoveAt(0);
            if(extraParameters.Count == 0) {
                extraParameters = null;
            }

            switch (input.ToLower()) {
                case "!create":
                    if (Game.Instance.CurrentPlayer == null) {
                        Game.Instance.CurrentBattle = null;
                        Game.Instance.Active = false;
                        Console.WriteLine("### Creating a new account ### ");
                        Console.WriteLine("Display Name (What other players will see): ");
                        string displayName = Console.ReadLine();
                        Game.Instance.CurrentPlayer = new Player(displayName);
                        Console.WriteLine(string.Format("Welcome {0}, your account has been created.", Game.Instance.CurrentPlayer.DisplayName));
                    } else {
                        Console.WriteLine("There's already an account active!");
                    }
                    break;
                case "!play":
                    if(Game.Instance.CurrentPlayer != null) {
                        Game.Instance.CurrentBattle = null;
                        Game.Instance.Active = true;
                        Console.WriteLine("### Game started ###");
                        Console.WriteLine("{0} is currently standing in \"{1}\" with {2}/{3} health remaining.",
                            Game.Instance.CurrentPlayer.DisplayName,
                            Game.Instance.Areas[Game.Instance.CurrentAreaIndex].Name,
                            Game.Instance.CurrentPlayer.CurrentHealth,
                            Game.Instance.CurrentPlayer.MaxHealth
                        );
                    } else {
                        Console.WriteLine("Useraccount not found. Please create one first.");
                    }
                    break;
                case "!commands":
                    Console.WriteLine(GetCommandList());
                    break;
                case "!test":
                    if (Game.Instance.CurrentPlayer == null) {
                        Game.Instance.CurrentPlayer = new Player("Erwin");
                        AskAndHandleInput("!play");
                    } else {
                        Console.WriteLine("There's already an account active!");
                    }
                    break;
                case "!battle":
                    if (Game.Instance.CurrentPlayer != null && Game.Instance.Active) {
                        Console.WriteLine("### BATTLE ###");
                        Game.Instance.CurrentBattle = Game.Instance.RandomEncounter();
                        if (Game.Instance.CurrentBattle != null) {
                            Console.WriteLine("Foes encountered consisting of the following npc's: {0}", Game.Instance.CurrentBattle.ToString());
                            Console.WriteLine("You have {0} health. Fight or run? (!fight or !run)", Game.Instance.CurrentPlayer.CurrentHealth);
                            AskAndHandleInput();
                        } else {
                            Console.WriteLine("There are no foes in this area.");
                        }
                    } else if(Game.Instance.CurrentPlayer == null) {
                        Console.WriteLine("No character found (dead ones don't count..). Type !create to make one.");
                    } else if (!Game.Instance.Active) {
                        Console.WriteLine("No active game found. Type !play to begin.");
                    }
                    break;
                case "!fight":
                    if (Game.Instance.CurrentBattle != null) {
                        Game.Instance.CurrentBattle.Fight();
                        if (Game.Instance.CurrentPlayer != null && Game.Instance.CurrentPlayer.CurrentHealth > 0) {
                            Console.WriteLine("You are victorious!");
                            Console.WriteLine("Remaining health: {0}/{1}", Game.Instance.CurrentPlayer.CurrentHealth,
                                Game.Instance.CurrentPlayer.MaxHealth);
                        } else {
                            Console.WriteLine("You died..");
                            PlayerDied();
                        }
                    } else if (Game.Instance.Active) {
                        Console.WriteLine("You are not in a battle. There's no need to fight or run!");
                    } else {
                        Console.WriteLine("No active game found. Type !play to start.");
                    }
                    break;
                case "!run":
                    if (Game.Instance.CurrentBattle != null) {
                        int damageTaken = Game.Instance.CurrentBattle.Flee();
                        if (Game.Instance.CurrentPlayer.CurrentHealth > 0) {
                            Console.WriteLine("You managed to escape, taking {0} damage", damageTaken);
                            Console.WriteLine("Remaining health: {0}/{1}", Game.Instance.CurrentPlayer.CurrentHealth,
                                Game.Instance.CurrentPlayer.MaxHealth);
                        } else {
                            Console.WriteLine("Whilst trying to escape you were dealt {0} damage, which was is " +
                                "enough to kill you.", damageTaken);
                            PlayerDied();
                        }
                    } else if (Game.Instance.Active) {
                        Console.WriteLine("You are not in a battle. There's no need to fight or run!");
                    } else {
                        Console.WriteLine("No active game found. Type !play to start.");
                    }
                    break;
                case "!stats":
                    if(Game.Instance.CurrentPlayer != null) {
                        Console.WriteLine("Name: {0}    | Level: {1}   | Health: {2}/{3}   | Maximum Damage: {4}   | Accuracy: {5}%",
                            Game.Instance.CurrentPlayer.DisplayName, Game.Instance.CurrentPlayer.Level, Game.Instance.CurrentPlayer.CurrentHealth, 
                            Game.Instance.CurrentPlayer.MaxHealth, Game.Instance.CurrentPlayer.MaxDamage, Game.Instance.CurrentPlayer.Accuracy);
                    } else {
                        Console.WriteLine("No character found (dead ones don't count..). Type !create to make one.");
                    }
                    break;
                case "!save":
                    Game.Instance.Save(extraParameters);
                    break;
                case "!load":
                    if (Game.Instance.Load(extraParameters)) {
                        AskAndHandleInput("!stats");
                    }
                    break;
                case "!files":
                    List<string> files = FileManager.Instance.getSaveFiles();
                    Console.WriteLine("List of files found. Use !load <filename> to load one.");
                    foreach(string file in files) {
                        Console.WriteLine(@" -   {0}", file);
                    }
                    break;
                case "!exit":
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Command not found. Please try again.");
                    break;
            }
            AskAndHandleInput();
        }

        string GetCommandList() {
            string list = @"
### Available commands ###
!create     => create a new account.
!play       => start your adventure.
!battle     => look for a battle.
!fight      => fight the encoutered battle.
!run        => flee from the encountered battle.
!commands   => See a list of all available commands.
!stats      => See current statistics of your character.
!save       => Save your progress. IMPORTANT: Overwrites previous progress!
!files      => See a list of existing files you can load.
!load       => Load from save file.
!exit       => Exit the application.
            ";

            return list;
        }

        void PlayerDied() {
            Game.Instance.CurrentPlayer = null;
            Console.WriteLine(@"
            
         ██▀███        ██▓      ██▓███       
        ▓██ ▒ ██▒     ▓██▒     ▓██░  ██▒     
        ▓██ ░▄█ ▒     ▒██▒     ▓██░ ██▓▒     
        ▒██▀▀█▄       ░██░     ▒██▄█▓▒ ▒     
        ░██▓ ▒██▒ ██▓ ░██░ ██▓ ▒██▒ ░  ░ ██▓ 
        ░ ▒▓ ░▒▓░ ▒▓▒ ░▓   ▒▓▒ ▒▓▒░ ░  ░ ▒▓▒ 
          ░▒ ░ ▒░ ░▒   ▒ ░ ░▒  ░▒ ░      ░▒  
          ░░   ░  ░    ▒ ░ ░   ░░        ░   
           ░       ░   ░    ░             ░  
                   ░        ░             ░  

            ");
        }
    }
}
