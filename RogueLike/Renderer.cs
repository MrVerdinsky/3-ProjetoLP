using System;
namespace RogueLike
{
    sealed public class Renderer
    {
        /// <summary>
        /// Prints the game map
        /// </summary>
        /// <param name="numberOfRows">Number of rows</param>
        /// <param name="numberOfColumns">Number of columns</param>
        public void Map(Map[,] map, int rows, int columns, 
                            PowerUp[] powerUps,Enemy[] enemies)
        {
            Console.WriteLine();
            Console.Write("| P - Player | M - Minion |  B - Boss |\n");
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
                    if (map[i,j].Position.Playable)
                        Console.Write("|__|");

                    // If the square has a player
                    if (map[i,j].Position.HasPlayer)
                        Console.Write("|P1|");
                    
                    foreach (Enemy enemy in enemies)
                    {
                        if (map[i,j].Position.HasEnemy)
                                Console.Write("|M1|");
                    }
                    foreach (PowerUp powerUp in powerUps)
                    {
                       if (map[i,j].Position.HasPowerUp)
                        {
                            if (powerUp.Heal == 4)
                                Console.Write("|SP|");
                            else if (powerUp.Heal == 8)
                                Console.Write("|MP|");
                            else
                                Console.Write("|BP|");
                        } 
                    }
                    
                }
                Console.WriteLine();
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit");
        }
        public void PrintCredits()
        {
            Console.WriteLine("Developed by:");
            Console.WriteLine("Pedro Marques");
            Console.WriteLine("Luiz Santos");
            Console.WriteLine("Gonçalo Vila Verde");
        }

        public void PrintInputError()
        {
            Console.WriteLine("Option Unkown");
        }
        
        public void PrintExitMsg()
        {
            Console.WriteLine("Thanks for playing.");
        }
    }
}