using System;
namespace RogueLike
{
    public class Input
    {

        int playerInput;
        string temp;
        private static Renderer print;
        public Input()
        {
            print = new Renderer();
        }
        public void MenuOptions(string input)
        {
            playerInput = Convert.ToByte(input);
            switch(playerInput)
            {
                case 1:
                    Console.WriteLine("New Game");
                    break;
                case 2:
                    Console.WriteLine("HighScores");
                    break;
                case 3:
                    Console.WriteLine("HighScores");
                    break;
                case 4:
                    print.PrintCredits();
                    break;
                case 5:
                    Console.WriteLine("Goodbye");
                    break;
                default:
                    Console.WriteLine("Unknown Option");
                    break;
            }
        }
    }
}