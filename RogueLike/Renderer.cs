using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace RogueLike
{
    /// <summary>
    /// Renders every element of the game to the consolo
    /// </summary>
    sealed public class Renderer
    {
        List<string> actions;

        /// <summary>
        /// Renderer Constructor
        /// </summary>
        public Renderer()
        {
            actions = new List<string>();
        }
        

        public void Map(Map[,] map, PowerUp[] powerUps,Enemy[] enemies,
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
                    if (map[i,j].Position.Empty)
                        Console.Write($"__|");

                    // If the square has a wall   
                    if (map[i,j].Position.HasWall)
                        Console.Write($"🚧|");

                    // If the square has a player
                    if (map[i,j].Position.HasPlayer)
                        Console.Write("🧙|");
                    
                    // Prints all enemies in the list
                    if (map[i,j].Position.HasEnemy)
                        foreach (Enemy enemy in enemies)
                        {
                            if(map[i,j].Position.Equals(enemy.Position))
                            {
                                Console.Write(enemy.Symbol);
                            }
                        }
                    
                    //Prints all power Ups in the list
                    else if (map[i,j].Position.HasPowerUp)
                    {   
                        foreach (PowerUp powerUp in powerUps)
                        {
                            if ((map[i,j].Position.Equals(powerUp.Position)) &&
                                powerUp.Picked == false)
                            {
                                Console.Write(powerUp.Symbol);
                            }
                        }  
                    }
                    else if (map[i,j].Position.HasExit)
                        Console.Write("🎌|");
                    
                }
                Console.WriteLine();
                
            }
        }

        /// <summary>
        /// Prints the Main Menu Options
        /// </summary>
        public void PrintMenu()
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
        public void PrintCredits()
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
        public void Introduction()
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
        public void BlankLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Prints no moves message
        /// </summary>
        public void NoMoves()
        {
            Console.WriteLine("\nYou feel weak and powerless. You can't move.");
        }

        /// <summary>
        /// Prints Menu Options Input Error
        /// </summary>
        public void PrintInputError()
        {
            Console.WriteLine("Option Unkown");
        }
        
        /// <summary>
        /// Prints a message before exiting the game
        /// </summary>
        public void PrintExitMsg()
        {
            Console.WriteLine("Thanks for playing.");
        }

        /// <summary>
        /// Prints a message to shorten the name
        /// </summary>
        public void InsertShorterName()
        {
            Console.WriteLine("\nPlease insert a shorter name.");
        }

        /// <summary>
        /// Prints an error message when theres an error starting the game
        /// </summary>
        public void IntroErrorMessage()
        {
            Console.WriteLine("\nIn order to start the game, please enter" +
                " your choices as it shows in the next example:\n" +
                "dotnet run -p RogueLike -- -r [NUMBER] -c [NUMBER]");
        }

        /// <summary>
        /// Prints no high scores message
        /// </summary>
        public void NoHighScores()
        {
            Console.WriteLine(
                    "\nThere are no high scores for this level yet :<");
        }

        public void InsertHighScore()
        {
            Console.WriteLine(
                    "\nCongratulations mighty warrior, you are now in the" +
                    " high score table.\nPlease insert your name below.");
        }

        /// <summary>
        /// Prints a dot
        /// </summary>
        public void Dot()
        {
            Console.Write('.');
        }

        /// <summary>
        /// Prints goodbye message for when the player dies
        /// </summary>
        public void GoodBye()
        {
            Console.WriteLine("\nBetter luck next time, adventurer.");
        }

        /// <summary>
        /// Prints goodbye message for when the player leave the game
        /// </summary>
        public void LeftBye()
        {
            Console.WriteLine("\nSo long, adventurer.");
        }

        


        /// <summary>
        /// Gets userInput action, adds to actions list as player movement
        /// </summary>
        /// <param name="str">String to check which turn is it</param>
        public void GetGameActions(char input)
        {
            // Removes first element when the list is size 5
            if (actions.Count > 5)
                actions.RemoveAt(0);

            if (input == 'w') actions.Add("\nYou walk North.");
            else if (input == 's') actions.Add("\nYou walk South.");
            else if (input == 'd') actions.Add("\nYou walk East.");
            else if (input == 'a') actions.Add("\nYou walk West.");
        }

        /// <summary>
        /// Gets power up and adds a message to actions list
        /// </summary>
        /// <param name="pu">Power up picked</param>
        public void GetGameActions(PowerUp pu)
        {
            // Removes first element when the list is size 5
            if (actions.Count > 5)
                actions.RemoveAt(0);

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
        public void GetGameActions(Enemy enemy)
        {
            // Removes first element when the list is size 5
            if (actions.Count > 5)
                actions.RemoveAt(0);

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
        public void GetGameActions()
        {
            if (actions.Count > 5)
                actions.RemoveAt(0);
            
            actions.Add("\nYou fend off the dangers in the cave, and" + 
                " venture forth below...");
        }
        /// <summary>
        /// Prints actions list
        /// </summary>
        public void PrintGameActions()
        {
            if (actions.Count == 0)
                actions.Add("\nAs you enter the Troll Lord cave, you start " +
                    $"walking.");

            // Removes first element when the list is size 5
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
        public void PrintInstructions()
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