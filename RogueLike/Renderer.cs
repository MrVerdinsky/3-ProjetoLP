using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace RogueLike
{
    /// <summary>
    /// Renders every element of the game to the consolo
    /// </summary>
    sealed internal class Renderer
    {
        /// <summary>
        /// List of strings used to print flavor text according to a specific
        /// action by the user
        /// </summary>
        List<string> actions;

        /// <summary>
        /// Renderer Constructor
        /// </summary>
        internal Renderer()
        {
            actions = new List<string>();
        }

        /// <summary>
        /// Prints the whole game map and information for the player
        /// </summary>
        /// <param name="map">All of the game's positions</param>
        /// <param name="powerUps">Array of PowerUps for the current level</param>
        /// <param name="enemies">Array of Enemies for the current level</param>
        /// <param name="player">Player's position and HP</param>
        /// <param name="turn">Whose turn to play</param>
        /// <param name="level">level's number</param>
        /// <param name="firstTurn">Checks if its the first turn</param>
        internal void Map(Map[,] map, PowerUp[] powerUps,Enemy[] enemies,
                        Player player, string turn, int level, bool firstTurn)
        {            
            Console.OutputEncoding = Encoding.UTF8;
            
            // Prints the meaning of each symbol in the map
            Console.WriteLine();
            Console.Write(" _________________________________________________" +
                            "________\n");
            Console.Write("|🧙- Player||🍙- Small Power-Up | 🚧" +
                            "- Obstacle            |\n");
            Console.Write("|🐀- Minion||🧀- Medium Power-Up| 🎌" + 
                            "- Exit                |\n");
            Console.Write("|🐉- Boss  ||🍖- Large Power-Up |   ____" +
                            "__________________|\n");
            Console.Write("|                                   |A/W/S/D or   " +
                            "  *KEYS*|\n");
            Console.Write($"|Moves Left: {player.Movement}"+
                            $"       Level: {level,-1}" +
                            "       |Arrow Keys - to move |\n");
            Console.Write($"|Player HP : {Player.HP,-5} - {turn,-6} Turn -  |  " 
                           + "  Escape - to leave|\n");
            Console.Write("|___________________________________|_____________" +
                            "________|\n\n");

            for (int j = 0; j < Game.columns; j++)
                Console.Write(" __");
                Console.WriteLine();
            for (int i = 0; i < Game.rows; i++)
            {    
                // For first turn
                if (firstTurn) Thread.Sleep(250);
                
                // For the OTHER rows
                // A magia acontece aqui V
                for (int j = 0; j < Game.columns; j++)
                {
                    if (j == 0)
                        Console.Write($"|");
                    // If the square is empty   
                    if (map[i,j].Empty)
                        Console.Write($"__|");

                    // If the square has a wall   
                    if (map[i,j].IsWall)
                        Console.Write($"🚧|");

                    // If the square has a player
                    if (map[i,j].IsPlayer && player.IsAlive)
                        Console.Write("🧙|");

                    if (map[i,j].IsPlayer && player.IsAlive == false)
                        Console.Write("💀|");

                    // Prints all enemies in the list
                    if (map[i,j].IsEnemy)
                        foreach (Enemy enemy in enemies)
                        {
                            if(map[i,j].Row == enemy.Row &&
                                map[i,j].Column == enemy.Column)
                            {
                                Console.Write(enemy.Symbol);
                            }
                        }
                    
                    //Prints all power Ups in the list
                    else if (map[i,j].IsPowerUp)
                    {   
                        foreach (PowerUp powerUp in powerUps)
                        {
                            if ((map[i,j].Row == powerUp.Row && 
                                map[i,j].Column == powerUp.Column) &&
                                powerUp.Picked == false)
                            {
                                Console.Write(powerUp.Symbol);
                            }
                        }  
                    }
                    else if (map[i,j].IsExit)
                        Console.Write("🎌|");
                    
                }
                Console.WriteLine();
                
            }
        }

        /// <summary>
        /// Prints the Main Menu Options
        /// </summary>
        internal void PrintMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit");
        }
        
        /// <summary>
        /// Prints Game Developers
        /// </summary>
        internal void PrintCredits()
        {
            Console.WriteLine("\n __________________");
            Console.WriteLine("|Developed by:     |");
            Console.WriteLine("|Pedro Marques     |");
            Console.WriteLine("|Luiz Santos       |");
            Console.WriteLine("|Goncalo Vila Verde|");
            Console.WriteLine("|__________________|");
        }

        /// <summary>
        /// Prints the Title Text
        /// </summary>
        internal void Introduction()
        {
            Console.WriteLine();
            Console.WriteLine(@"_____  ___   ___            ___  __         __"+
                "_ ");
            Console.WriteLine(@"  |   |___| |   | |   |    |    |__| \   / |_ "+
                "  ");
            Console.WriteLine(@"  |   |   \ |___| |__ |__  |___ |  |  \_/  |__"+
                "_ \n");
            Console.WriteLine(@"                       ...a roguelike adventur"+
                "e");

        }

        /// <summary>
        /// Prints a blank line
        /// </summary>
        internal void BlankLine() => Console.WriteLine();

        /// <summary>
        /// Prints no moves message
        /// </summary>
        internal void NoMoves()
        {
            Console.WriteLine("\nYou feel weak and powerless. You can't move.");
        }

        /// <summary>
        /// Prints Menu Options Input Error
        /// </summary>
        internal void PrintInputError() => Console.WriteLine("Option Unkown");
        
        /// <summary>
        /// Prints a message before exiting the game
        /// </summary>
        internal void PrintExitMsg() => Console.WriteLine("Thanks for playing.");

        /// <summary>
        /// Prints a message to shorten the name
        /// </summary>
        internal void InsertShorterName()
        {
            Console.WriteLine("\nPlease insert a shorter name.");
        }

        /// <summary>
        /// Prints an error message when theres an error starting the game
        /// </summary>
        internal void IntroErrorMessage()
        {
            Console.WriteLine("\nIn order to start the game, please enter" +
                " your choices as it shows in the next example:\n" +
                "dotnet run -p RogueLike -- -r [NUMBER] -c [NUMBER]");
        }

        /// <summary>
        /// Prints no high scores message
        /// </summary>
        internal void NoHighScores()
        {
            Console.WriteLine(
                    "\nThere are no high scores for this level yet :<");
        }

        /// <summary>
        /// Prints message asking if the player wants to save the game
        /// </summary>
        internal void GetSaveIntention()
        {
            Console.Write(
                "\nDo you want to save your progress so far?"+
                "\nPress 'y' for Yes and 'n' for No: ");
        }
        /// <summary>
        /// Prints message asking if the player wants to save the game before
        /// leaving
        /// </summary>
        internal void SaveBeforeLeave()
        {
            Console.Write(
                "\nDo you want to save your progress before you leave?"+
                "\nPress 'y' for Yes and 'n' for No: ");   
        }
        /// <summary>
        /// Print a welcome back message when the game is loaded from a save
        /// file
        /// </summary>
        internal void WelcomeBack()
        {
            Console.WriteLine(" _________________________________________________________");
            Console.WriteLine("|                                                         |");
            Console.WriteLine(
                              "|             ⚔️ Welcome back, adventurer! ⚔️             |"); 
            Console.WriteLine("|_________________________________________________________|");
        }

        /// <summary>
        /// Print error message as result of a invalid given file name
        /// </summary>
        internal void InvalidFileName()
        {
            Console.WriteLine(
                "\nPlease choose a valid file name."+
                "\nTry runing the program as the example bellow:"+
                "\ndotnet run -p RogueLike/ -- -l <file name>.sav\n"
            );
        }

        /// <summary>
        /// Prints message asking for the save file name
        /// </summary>
        internal void InsertFileName()
        {
            Console.Write(
                "\nPlease insert a name for your save file: "
            );
        }

        /// <summary>
        /// Prints message to insert high score
        /// </summary>
        internal void InsertHighScore()
        {
            Console.WriteLine(
                    "\nCongratulations mighty warrior, you are now in the" +
                    " high score table.\nPlease insert your name below.");
        }

        /// <summary>
        /// Prints a dot
        /// </summary>
        internal void Dot()
        {
            Console.Write('.');
        }

        /// <summary>
        /// Prints goodbye message for when the player dies
        /// </summary>
        internal void GoodBye()
        {
            Console.WriteLine("\nBetter luck next time, adventurer.");
        }

        /// <summary>
        /// Prints goodbye message for when the player leave the game
        /// </summary>
        internal void LeftBye()
        {
            Console.WriteLine("\nSo long, adventurer.");
        }

        /// <summary>
        /// Gets userInput action, adds to actions list as player movement
        /// </summary>
        /// <param name="input"> Gets player movement key</param>
        internal void GetGameActions(ConsoleKeyInfo input)
        {
            // Removes first element when the list is size 5
            if (actions.Count > 5)
                actions.RemoveAt(0);

            switch (input.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    actions.Add("\nYou walk North.");
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    actions.Add("\nYou walk South.");
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    actions.Add("\nYou walk East.");
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    actions.Add("\nYou walk West.");
                    break;
                default:    
                    break;
            }
        }

        /// <summary>
        /// Gets power up and adds a message to actions list
        /// </summary>
        /// <param name="pu">Power up picked</param>
        internal void GetGameActions(PowerUp pu)
        {
            if (pu.Heal == 4)
            {
                actions.Add($"\nYou found an untouched rice cake and healed " +
                    $"yourself for {pu.Heal} HP!!");
            }
            else if (pu.Heal == 8)
            {
                actions.Add($"\nHurray, you ate a piece of cheese and healed " +
                    $"yourself for {pu.Heal} HP!!");
            }
            else
                actions.Add($"\nYou found a huge piece of meat and " +
                    $"ate it. You heal yourself for {pu.Heal} HP!! ");
        }

        /// <summary>
        /// Gets enemy damage and adds a message to actions list
        /// </summary>
        /// <param name="enemy">Gets damage from this enemy</param>
        internal void GetGameActions(Enemy enemy)
        {
            if (enemy.Damage == 5)
            {
                actions.Add("\nA small Rat attacked you and damaged you " +
                    $"for {enemy.Damage} hp !!");
            }
            else
            {
                actions.Add("\nA Giant Dragon attacked you and damaged you " +
                    $"for {enemy.Damage} hp !!");
            }  
        }

        /// <summary>
        /// Prints Next level message 
        /// </summary>
        internal void GetGameActions()
        {
            actions.Add("\nYou fend off the dangers in the cave, and" + 
                " venture forth below...");
        }

        /// <summary>
        /// Prints actions list
        /// </summary>
        internal void PrintGameActions()
        {
            if (actions.Count == 0)
                actions.Add("\nAs you enter the Troll Lord cave, you start " +
                    $"walking.");

            // Removes first element when the list has more than 5 elements
            if (actions.Count > 5)
                actions.RemoveAt(0);

            // Prints the list
            Console.WriteLine("\nJournal");
            Console.WriteLine("______________________________________________" +
                "____________");
            foreach (String action in actions)
            {
                Console.WriteLine(action);
            }
        }

        /// <summary>
        /// Prints the game's rules and controls
        /// </summary>
        internal void PrintInstructions()
        {
            Console.WriteLine(" ______________________________________________"+
            "___________________________");
            Console.WriteLine("|\t\t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\t\t\t      Rules and Controls\t\t\t  |");
            Console.WriteLine("|_____________________________________________"+
            "____________________________|");
            Console.WriteLine("|\t\t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a You will always start in the closest" + 
            " side of the room, in a random     |\n|   position."+
            "\t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a Your objective, if you choose to" + 
            "accept it, is to reach the exit gate,  |\n|  positioned in the far"
            +" end of the room.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a You can and will have to, move up to" + 
            "2 Squares per turn in 4 directions |\n|  (⭠ ⭢ ⭡ ⭣), but beware" + 
            "moving will deplete your HP by 1 point each time  |\n|   you move." 
            + " \t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a To move you can use the keys WASD," + 
            " representing ⭡, ⭠, ⭣, ⭢ .\t\t  |");
            Console.WriteLine("|\u2b9a As you move through the room, you will" + 
            " find enemies, either big or     |\n|  small. You can't fight" +
            " them so you better use your noggin and figure   |\n|  out a way"+ 
            "to go around them.\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a They aren't blind either, as you move," + 
            " they will try to get you, moving|\n|  1 square per turn, chasing"+ 
            " after you.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a If you fail to evade them, it will cost"+
            " ya, losing 5 HP to the little  |\n|  fellas and 10 Hp to big" +  
            " ones.\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a But don't worry there's a few Power-Ups"+ 
            " laying around, with 4, 8 or if |\n|  you're lucky, 16 HP points"+ 
            " for you to grab.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a There might be some rubbles in the way" + 
            " so you have to go around them   |\n|  aswell, but I think you're"+
            " smart enough to do that.\t\t\t  |");
            Console.WriteLine("|\u2b9a Goodluck and Have fun," + 
            " if you live....\t\t\t\t  |");
            Console.WriteLine("|_____________________________________________"+
            "____________________________|");
        }
    }
}