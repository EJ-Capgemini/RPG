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
            Thread.Sleep(2000); //spannend...
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
            Console.WriteLine("Type !commands to see the full list of commands. Good luck and have fun.");
        }

        static void AskAndHandleInput() {
            string input = Console.ReadLine();
            
            switch (input) {
                case "!new":
                case "!create":
                    Console.WriteLine("### Creating a new account ### ");
                    //Console.WriteLine("We will need the following information: ");
                    //Console.WriteLine("Email: ");
                    //string email = Console.ReadLine();
                    //Console.WriteLine("Password: ");
                    //string password = Console.ReadLine();
                    Console.WriteLine("Display Name (What other players will see): ");
                    string displayName = Console.ReadLine();
                    Player player = new Player(email, password, displayName);
                    Console.WriteLine(string.Format("Welcome {0}, your account has been created.", player.DisplayName));
                    break;
                case "!start":
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
                !new => create a new account.
                !commands => See a list of all available commands.
            ";

            return list;
        }
    }
}
