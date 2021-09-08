//Archie Cooke
//08/09/2021
//Battle Boats
//Version 1

using System;

namespace BattleBoats
{
    class Program
    {

        private static Board _board;

        static void Main(string[] args)
        {
            
            MainMenu();
        }

        static void MainMenu()
        {
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. How to play");
            Console.WriteLine("3. Quit");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                    NewGame();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("How to play");
                    Console.WriteLine("Battle Boats is a turn-based strategy game.");
                    Console.WriteLine("Pick coordinates to attack and try to eliminate all enemy boats.");
                    Console.WriteLine("The winner is whoever hits all opposing boats first.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Welcome to BattleBoats - Please make a selection");
                    MainMenu();
                    break;
                case "3":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid Selection - Please select a selection");
                    MainMenu();
                    break;
            }
        }

        static void NewGame()
        {
            Console.Clear();
            _board = new Board();

            int boats = 1;
            while (boats != 6)
            {
                _board.DisplayFleetGrid();
                Console.WriteLine("Please enter co-ordinates for your boats - " + boats + "/5");
                Console.Write(">");

                string coords = Console.ReadLine();

                // Check if input is actually a pair of coordinates;
                if (!_board.ValidateCoords(coords))
                {
                    Console.Clear();
                    Console.WriteLine("Those coordinates don't exist! Try again...");
                    continue;
                }

                // Set boat location - if the boat already exists then ask for new input
                if (!_board.SetPlayerBoat(coords))
                {
                    Console.Clear();
                    Console.WriteLine("There is already a boat in that location! Try again...");
                    continue;
                }

                // Boat has been added, ask for the next one or move on
                boats++;
                Console.Clear();
                continue;
            }

            bool playing = true;
            bool playerTurn = true;
            while (playing)
            {
                if (playerTurn)
                {
                    _board.DisplayTargetTracker();
                    Console.WriteLine("Please enter co-ordinates to attack:");
                    Console.Write(">");

                    string coords = Console.ReadLine();

                    // Check if input is actually a pair of coordinates 
                    if (!_board.ValidateCoords(coords))
                    {
                        Console.Clear();
                        Console.WriteLine("Those coordinates don't exist! Try again...");
                        continue;
                    }

                    if (!_board.AddHitCoordinate(coords))
                    {
                        Console.Clear();
                        Console.WriteLine("You've already hit that location! Try again...");
                        continue;
                    }

                    Console.Clear();
                    if (_board.IsHit(coords))
                    {
                        Console.WriteLine("You hit an enemy boat!");
                    }
                    else
                    {
                        Console.WriteLine("You didn't hit anything...");
                    }

                    _board.DisplayTargetTracker();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    string coords = _board.ComputerAttack();
                    _board.DisplayFleetGrid();
                    Console.WriteLine("The computer has attacked " + coords + "!");
                    if (_board.IsComHit(coords))
                        Console.WriteLine("Hit!");
                    else
                        Console.WriteLine("Miss!");

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();                    
                }


                Console.Clear();
                playerTurn = !playerTurn;
                if (_board.CheckWinConditions() == "Player") {
                    Console.WriteLine("You win! :)");
                    MainMenu();
                } else if (_board.CheckWinConditions() == "Computer") {
                    Console.WriteLine("You lost! :(");
                    MainMenu();
                }
            }
        }
    }
}
