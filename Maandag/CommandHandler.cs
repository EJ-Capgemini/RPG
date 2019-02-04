using Maandag.Model;
using System;
using System.Threading;

namespace Maandag {
    class CommandHandler {
        static void Main(string[] args) {
            showIntro();
            AskAndHandleInput();
        }

        static void showIntro() {
            Console.WriteLine("WELCOME TO....");
            Thread.Sleep(1500); //spannend...
            Console.WriteLine(@"

             ███▄ ▄███▓ ▄▄▄      ▄▄▄       ███▄    █ ▓█████▄  ▄▄▄        ▄████ 
            ▓██▒▀█▀ ██▒▒████▄   ▒████▄     ██ ▀█   █ ▒██▀ ██▌▒████▄     ██▒ ▀█▒
            ▓██    ▓██░▒██  ▀█▄ ▒██  ▀█▄  ▓██  ▀█ ██▒░██   █▌▒██  ▀█▄  ▒██░▄▄▄░
            ▒██    ▒██ ░██▄▄▄▄██░██▄▄▄▄██ ▓██▒  ▐▌██▒░▓█▄   ▌░██▄▄▄▄██ ░▓█  ██▓
            ▒██▒   ░██▒ ▓█   ▓██▒▓█   ▓██▒▒██░   ▓██░░▒████▓  ▓█   ▓██▒░▒▓███▀▒
            ░ ▒░   ░  ░ ▒▒   ▓▒█░▒▒   ▓▒█░░ ▒░   ▒ ▒  ▒▒▓  ▒  ▒▒   ▓▒█░ ░▒   ▒ 
            ░  ░      ░  ▒   ▒▒ ░ ▒   ▒▒ ░░ ░░   ░ ▒░ ░ ▒  ▒   ▒   ▒▒ ░  ░   ░ 
            ░      ░     ░   ▒    ░   ▒      ░   ░ ░  ░ ░  ░   ░   ▒   ░ ░   ░ 
                   ░         ░  ░     ░  ░         ░    ░          ░  ░      ░ 
                                                      ░                        
            ");
            Thread.Sleep(500);
            Console.WriteLine("Type !commands to see the full list of commands. Good luck and have fun.");
        }

        static void AskAndHandleInput(string input = null) {
            if(input == null) {
                input = Console.ReadLine();
            }
            
            switch (input.ToLower()) {
                case "!create":
                    if (Game.Instance.CurrentPlayer == null) {
                        Console.WriteLine("### Creating a new account ### ");
                        //Console.WriteLine("We will need the following information: ");
                        //Console.WriteLine("Email: ");
                        //string email = Console.ReadLine();
                        //Console.WriteLine("Password: ");
                        //string password = Console.ReadLine();
                        Console.WriteLine("Display Name (What other players will see): ");
                        string displayName = Console.ReadLine();
                        //Player player = new Player(email, password, displayName);
                        Game.Instance.CurrentPlayer = new Player(displayName);
                        Console.WriteLine(string.Format("Welcome {0}, your account has been created.", Game.Instance.CurrentPlayer.DisplayName));
                    } else {
                        Console.WriteLine("There's already an account active!");
                    }
                    break;
                case "!play":
                    if(Game.Instance.CurrentPlayer != null) {
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
                    if (Game.Instance.Active) {
                        Console.WriteLine("### BATTLE ###");
                        Battle battle = Game.Instance.RandomEncounter();
                        if (battle != null) {
                            Console.WriteLine("Foes encountered consisting of the following npc's: {0}", battle.ToString());
                            Console.WriteLine("You have {0} health. Fight or run? (!fight or !run)", Game.Instance.CurrentPlayer.CurrentHealth);
                            string answer;
                            do {
                                answer = Console.ReadLine();
                                var answerLower = answer?.ToLower();
                                if ((answerLower == "!fight") || (answerLower == "!run"))
                                    if (answer == "!fight") {
                                        Game.Instance.FightBattle(battle);
                                        if (Game.Instance.CurrentPlayer.CurrentHealth > 0) {
                                            Console.WriteLine("You are victorious!");
                                            Console.WriteLine("Remaining health: {0}/{1}", Game.Instance.CurrentPlayer.CurrentHealth,
                                                Game.Instance.CurrentPlayer.MaxHealth);
                                        } else {
                                            Console.WriteLine("You died..");
                                            PlayerDied();
                                        }
                                    } else if (answer == "!run") {
                                        Game.Instance.FleeFromBattle(battle);
                                        if (Game.Instance.CurrentPlayer.CurrentHealth > 0) {
                                            Console.WriteLine("Well that was humiliating.., but at least you survived!");
                                            Console.WriteLine("Remaining health: {0}/{1}", Game.Instance.CurrentPlayer.CurrentHealth,
                                                Game.Instance.CurrentPlayer.MaxHealth);
                                        } else {
                                            Console.WriteLine("You failed to escape. R.I.P.");
                                            PlayerDied();
                                        }
                                    }
                                break;
                            } while (true);
                        } else {
                            Console.WriteLine("There are no foes in this area.");
                        }
                    } else {
                        Console.WriteLine("No active game found. Type !play to start.");
                    }
                    break;
                case "!fight":
                case "!run":
                    if (Game.Instance.Active) {
                        Console.WriteLine("You are not in a battle. There's no need to fight or run!");
                    } else {
                        Console.WriteLine("No active game found. Type !play to start.");
                    }
                    break;
                default:
                    Console.WriteLine("Command not found. Please try again.");
                    break;
            }
            AskAndHandleInput();
        }

        static string GetCommandList() {
            string list = @"
### Available commands ###
!create     => create a new account.
!play       => start your adventure.
!battle     => look for a battle.
!fight      => fight the encoutered battle.
!run        => flee from the encountered battle.
!commands   => See a list of all available commands.
            ";

            return list;
        }

        static void PlayerDied() {
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
