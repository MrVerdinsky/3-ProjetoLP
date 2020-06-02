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
            Console.Write("| -P - Player | -M - Minion |  -B - Boss |\n");
            Console.Write("| SP - Small Power-Up | SM - Medium Power-Up |" + 
                                " BP - Big Power-Up |\n");
            Console.Write("| O - Obstacle | E - Exit |\n");
            // foreach(Map square in map)
            // {
            //     Console.WriteLine($"\nEmpty     [{square.Position.Row}, {square.Position.Column}]: {square.Position.Empty}");
            //     Console.WriteLine($"HasPlayer [{square.Position.Row}, {square.Position.Column}]: {square.Position.HasPlayer}");
            // }
            
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
                        Console.Write($"|{i}{j}|");
                    // If the square has a player
                    if (map[i,j].Position.HasPlayer)
                        Console.Write("|-P|");
                    
                    foreach (Enemy enemy in enemies)
                    {
                        if (map[i,j].Position.HasEnemy)
                        {
                            if (enemy.damage == 5) Console.Write($"|M{j}|");
                            if (enemy.damage == 10) Console.Write("|-B|");
                        }
                    }
                    

                    foreach (PowerUp powerUp in powerUps)
                    if (map[i,j].Position.HasPowerUp && powerUp.Picked == false)
                    {
                        if (powerUp.Heal == 4) Console.Write("|SP|");
                        if (powerUp.Heal == 8) Console.Write("|MP|");
                        if (powerUp.Heal == 16) Console.Write("|BP|"); 
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

        public void IntroErrorMessage()
        {
            Console.WriteLine("\nIn order to start the game, please enter" +
                " your choices as it shows in the next example:\n" +
                "dotnet run -p RogueLike -- -r [NUMBER] -c [NUMBER]");
        }

    }
}