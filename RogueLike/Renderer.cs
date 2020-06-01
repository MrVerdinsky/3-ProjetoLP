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
        public void Map(int numberOfRows, int numberOfColumns)
        {
            Console.WriteLine();
            for (int i = 1; i <= numberOfRows; i++)
            {   
                // For FIRST row
                if (i == 1)
                {
                    for (int j = 1; j <= numberOfColumns; j++)
                        Console.Write(" __ ");
                    Console.WriteLine();
                }
                // For the OTHER rows
                for (int j = 1; j <= numberOfColumns; j++)
                {
                    Console.Write("|__|");
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