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
        public void Map(Map[,] map, int rows, int columns)
        {
            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {   
                // For FIRST row
                
                for (int j = 0; j < columns; j++)
                    Console.Write(" __ ");
                if (i == 0) Console.Write("\tP - Player");
                if (i == 1) Console.Write("\tB - Boss");
                if (i == 2) Console.Write("\tMP - Medium Power-Up");
                if (i == 3) Console.Write("\tSP - Small Power-Up");
                if (i == 4) Console.Write("\tO - Obstacle");

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


                }
                if(i == 0) Console.Write("\tM - Minion");
                if(i == 1) Console.Write("\tSP - Small Power-Up");
                if(i == 2) Console.Write("\tBP - Big Power-Up");
                if(i == 3) Console.Write("\tE - Exit");
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