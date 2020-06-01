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
            if (args[0] == "-c" && args[2] == "-r")
            {
                Game game = new Game(
                    Convert.ToInt16(args[1]),
                    Convert.ToInt16(args[3]));
            }
            if (args[0] == "-r" && args[2] == "-c")
            {
                Game game = new Game(
                    Convert.ToInt16(args[3]),
                    Convert.ToInt16(args[1]));
            }
        }
    }
}
