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
            Renderer print = new Renderer();
            // Gets the seed for random, based on system current time
            DateTime currentTime = DateTime.Now;
            long seed = currentTime.Ticks;

            try
            {
               if (args[0] == "-c" && args[2] == "-r")
                {
                    Game game = new Game(
                        Convert.ToInt16(args[3]),
                        Convert.ToInt16(args[1]),
                        seed);
                }
                if (args[0] == "-r" && args[2] == "-c")
                {
                    Game game = new Game(
                        Convert.ToInt16(args[1]),
                        Convert.ToInt16(args[3]),
                        seed);
                } 
            }
            catch (IndexOutOfRangeException)
           {
                print.IntroErrorMessage();
           }
               

        }
    }
}
