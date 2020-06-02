using System;
namespace RogueLike
{
    sealed public class Renderer
    {
        /// <summary>
        /// Prints the map
        /// </summary>
        /// <param name="map">Map Positions</param>
        /// <param name="rows">Number of Rows</param>
        /// <param name="columns">Number of Columns</param>
        /// <param name="powerUps">List of PowerUps</param>
        /// <param name="enemies">List of enemies</param>
        public void Map(Map[,] map, int rows, int columns, 
                            PowerUp[] powerUps,Enemy[] enemies)
        {
            // Prints the meaning of each symbol in the map
            Console.WriteLine();
            Console.Write("| -P - Player | -M - Minion |  -B - Boss |\n");
            Console.Write("| SP - Small Power-Up | SM - Medium Power-Up |" + 
                                " BP - Big Power-Up |\n");
            Console.Write("| O - Obstacle | E - Exit |\n");

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

                    // If the square has a player
                    if (map[i,j].Position.HasPlayer)
                        Console.Write("|-P|");
                    
                    // Prints all enemies in the list
                    if (map[i,j].Position.HasEnemy)
                        foreach (Enemy enemy in enemies)
                        {
                            if (enemy.damage == 5) Console.Write($"|-M|");
                            else Console.Write("|-B|");
                        }
                    
                    //Prints all power Ups in the list
                    if (map[i,j].Position.HasPowerUp)
                        foreach (PowerUp powerUp in powerUps)
                            if (powerUp.Picked == false)
                            {
                                if (powerUp.Heal == 4) Console.Write("|SP|");
                                else if (powerUp.Heal == 8) 
                                            Console.Write("|MP|");
                                else Console.Write("|BP|"); 
                            }
                    
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
            Console.WriteLine("Developed by:");
            Console.WriteLine("Pedro Marques");
            Console.WriteLine("Luiz Santos");
            Console.WriteLine("Gonçalo Vila Verde");
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

        public void PlayerHP(Player p1)
        {
            Console.WriteLine("\nHP --------- " + p1.HP);
        }

        public void EnemyTurn()
        {
            Console.WriteLine("\nEnemy Turn");
        }

        public void PlayerTurn()
        {
            Console.WriteLine("\nPlayer Turn");
        }
    }
}