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

            //Tries to start game and printing an error message if it fails
             /* try
            {  */  
                //Checks if the player wrote columns first
               if (args[0] == "-c" && args[2] == "-r")
                {
                    //Converts the arguments to ints
                    Game game = new Game(
                        Convert.ToInt16(args[3]),
                        Convert.ToInt16(args[1]),
                        seed);
                }

                //Checks if the player wrote columns first
                if (args[0] == "-r" && args[2] == "-c")
                {
                    //Converts the arguments to ints
                    Game game = new Game(
                        Convert.ToInt16(args[1]),
                        Convert.ToInt16(args[3]),
                        seed);
                } 
             /* } */
            //Prints an Error message if the user inputs the wrong parameters 
            /* catch (IndexOutOfRangeException)
            {
                print.IntroErrorMessage();
            } */
               

        }
    }
}
