using System;

namespace RogueLike
{
    /// <summary>
    /// Program Class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Initial method
        /// </summary>
        /// <param name="args">Receives user choices</param>
        static void Main(string[] args)
        {
            //Used to call Renderer Class
            Renderer print = new Renderer();
            // Gets the seed for random, based on system current time
            DateTime currentTime = DateTime.Now;
            //Variable used to save the current game's seed
            int seed = (int)(currentTime.Ticks);

            // Checks if the argument attend the minimal length needed 
            if (args.Length == 4)
            {
                int input1 = 0;
                int input2 = 0;
                // Tries to convert the input to integer
                try
                {
                    input1 = Convert.ToInt16(args[1]);
                    input2 = Convert.ToInt16(args[3]);
                }

                catch (FormatException)
                {
                    // Prints message and ends the application
                    print.IntroErrorMessage();
                    return;
                }

                //Checks if the player wrote columns first
                if (args[0] == "-c" && args[2] == "-r")
                {
                    Game game = new Game(
                        input2, input1, seed);
                }
                //Checks if the player wrote rows first
                if (args[0] == "-r" && args[2] == "-c")
                {
                    Game game = new Game(
                        input1, input2, seed);
                } 
            }
            // Prints the message if the input format doesn't attend
            // the correct format
            else
                print.IntroErrorMessage();
        }
    }
}
