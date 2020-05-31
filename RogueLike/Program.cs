using System;

namespace RogueLike
{
    class Program
    {
        private static Renderer print;
        private static Input input;
        public Board[,] Board { get; private set; }
        private static int Columns;
        private static int Row;
        private static bool quit;
        private static string temp;
        public Program()
        {
            print = new Renderer();
            input = new Input();
        }
        static void Main(string[] args)
        {
            /*
            if (args[0] == "-c" && args[2] == "-r")
            {
                
                Columns = Convert.ToByte(args[1]);
                Row = Convert.ToByte(args[3]);
            }
            else
            {
                Columns = 0;
                Row = 0;
            }
            quit = false;
            print.PrintMenu();
            do
            {
                temp = Console.ReadLine();
                input.MenuOptions(temp);

            }while (quit == false);
            */
            
        }
    }
}
