using System;
using System.Text;
using System.Collections.Generic;
namespace RogueLike
{
    sealed public class Renderer
    {
        List<string> actions;

        public Renderer()
        {
            actions = new List<string>();
        }
        
        /// <summary>
        /// Prints the map
        /// </summary>
        /// <param name="map">Map Positions</param>
        /// <param name="rows">Number of Rows</param>
        /// <param name="columns">Number of Columns</param>
        /// <param name="powerUps">List of PowerUps</param>
        /// <param name="enemies">List of enemies</param>
        public void Map(Map[,] map, int rows, int columns, 
                            PowerUp[] powerUps,Enemy[] enemies,
                            Player player, string turn, int level)
        {            
            Console.OutputEncoding = Encoding.UTF8;
            
            // Prints the meaning of each symbol in the map
            Console.WriteLine();
            Console.Write(" _________________________________________________" +
                            "________\n");
            Console.Write("|\u2654 - Player||\u2749 - Small Power-Up | \u2716" +
                            " - Obstacle            |\n");
            Console.Write("|\u265F - Minion||\u273E - Medium Power-Up| \u2FA8" + 
                            "- Exit                |\n");
            Console.Write("|\u265A - Boss  ||\u2740 - Large Power-Up |   ____" +
                            "__________________|\n");
            Console.Write("|                                   |A/W/S/D or   " +
                            "  *KEYS*|\n");
            Console.Write($"|Moves Left: {player.Movement}"+
                            $"       Level: {level}" +
                            "       |Arrow Keys - to move |\n");
            Console.Write($"|Player HP : {player.HP,-5} - {turn} Turn -   |   " +
                            " Escape - to leave|\n");
            Console.Write("|___________________________________|_____________" +
                            "________|\n\n");


            for (int i = 0; i < rows; i++)
            {   
                // For FIRST row
                
                for (int j = 0; j < columns; j++)
                    Console.Write(" __ ");
                Console.WriteLine();
        
                // For the OTHER rows
                // A magia acontece aqui V
                for (int j = 0; j < columns; j++)
                {
                    // If the square is empty   
                    if (map[i,j].Position.Empty)
                        Console.Write($"|__|");

                    // If the square has a wall   
                    if (map[i,j].Position.HasWall)
                        Console.Write($"|\u2716 |");

                    // If the square has a player
                    if (map[i,j].Position.HasPlayer)
                        Console.Write("|\u2654 |");
                    
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
                        Console.Write("|\u2FA8|");
                    
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
        
        //Prints a message before exiting the game
        public void PrintExitMsg()
        {
            Console.WriteLine("Thanks for playing.");
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
                actions.Add($"\nHurray, you ate a piece of cheese and healed " +
                    $"yourself for {pu.Heal} HP!!");
            }
            else if (pu.Heal == 8)
            {
                actions.Add($"\nYou found a Broiled coyote heart with basil " +
                    $"and healed yourself for {pu.Heal} HP!! ");
            }
            else
                actions.Add($"\nYou killed a snake with your bare hands and " +
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
                actions.Add("\nA small goblin attacked you and damaged you " +
                    $"for {enemy.Damage} hp !!");
            }
            else
            {
                actions.Add("\nA Giant Troll attacked you and damaged you " +
                    $"for {enemy.Damage} hp !!");
            }  
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
            if (actions.Count == 5)
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
        /// Prints goodbye message
        /// </summary>
        public void GoodBye()
        {
            Console.WriteLine("\nBetter luck next time adventurer.");
        }

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
            " side of the room, in a random     |\n|   position.\t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a Your objective, if you choose to accept" +
             " it, is to reach the exit gate, |\n|  positioned in the far" +
             " end of the room.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a You can and will have to, move up to 2 " +
            "Squares per turn in 4 directions|\n|  (⭠ ⭢ ⭡ ⭣), but beware moving" + 
            " will deplete your HP by 1 point each time |\n|   you move." + 
            " \t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a To move you can use the keys WASD," + 
            " representing ⭡, ⭠, ⭣, ⭢ .\t\t  |");
            Console.WriteLine("|\u2b9a As you move through the room, you will" + 
            " find enemies, either big or     |\n|  small. You can't fight them so you" + 
            " better use your noggin and figure   |\n|  out a way  to go around" + 
            " them.\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a They aren't blind either, as you move," + 
            " they will try to get you, moving|\n|  1 square per turn, chasing"+ 
            " after you.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a If you fail to evade them, it will cost" +
            " ya, losing 5 HP to the little  |\n|  fellas and 10 Hp to big" +  
            " ones.\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a But don't worry there's a few Power-Ups" + 
            " laying around, with 4, 8 or if |\n|  you're lucky, 16 HP points for" + 
            " you to grab.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a There might be some rubbles in the way" + 
            " so you have to go around them   |\n|  aswell, but I think you're smart" + 
            " enough to do that.\t\t\t  |");
            Console.WriteLine("|\u2b9a Goodluck and Have fun, if you live....\t\t\t\t  |");
            Console.WriteLine("|_____________________________________________"+
            "____________________________|");
        }
    }
}