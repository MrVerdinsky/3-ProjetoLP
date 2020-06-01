using System;
namespace RogueLike
{
    sealed public class Input
    {

        string playerInput;
        private static Renderer print;
        public Input()
        {
            print = new Renderer();
        }
        //Controls all Menu Options until players chooses new game
        public string MenuOptions()
        {
            //Keeps running until players starts new game
            do
            {
                playerInput = Console.ReadLine();
                
                switch(playerInput)
                    {
                case "1":
                    Console.WriteLine("New Game");
                    break;
                case "2":
                    Console.WriteLine("HighScores");
                    break;
                case "3":
                    Console.WriteLine("Instructions");
                    break;
                case "4":
                    print.PrintCredits();
                    break;
                case "5":
                    print.PrintExitMsg();
                    return playerInput;
                
                //Returns the input here so the players goes back to main menu
                case "":
                    print.PrintMenu();
                    MenuOptions();
                    return playerInput;
                default:
                    print.PrintInputError();
                    break;
                    }

            }while(playerInput != "1");

            //returns player's input here if he chooses new game or quits.
            return playerInput;
        }
        /// <summary>
        /// Gets a map with all positions updated
        /// </summary>
        /// <param name="player"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public Map[,] GetPosition(Player player, Map[,] map)
        {
            // players input
            char playerInput; 
            // Frees the player position
            map[player.Position.Row, player.Position.Column].Position.PlayerFree();
            // Gets player input
            playerInput = Console.ReadKey().KeyChar;
            // Moves player to new position
            player.Move(playerInput);
            // Occupies inserted position with player
            map[player.Position.Row,player.Position.Column].Position.PlayerOccupy();

            return map;
        }
    }
}