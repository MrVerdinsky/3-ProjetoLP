using System;
namespace RogueLike
{
    sealed public class Input
    {
        string playerInput;
        private Renderer print;
        
        /// <summary>
        /// Class Constructor
        /// </summary>
        public Input()
        {
            print = new Renderer();
        }
        //Controls all Menu Options until players chooses new game
        public string MenuOptions(int rows, int columns)
        {
            //Keeps running until players starts new game
            playerInput = Console.ReadLine();
            switch(playerInput)
            {
                case "1":
                    return playerInput;
                case "2":
                    print.PrintScore(rows, columns);
                    break;
                case "3":
                    print.PrintInstructions();
                    break;
                case "4":
                    print.PrintCredits();
                    break;
                case "5":
                    print.PrintExitMsg();
                    return playerInput;
                //Returns the input here so the players goes back to main menu
                default:
                    print.PrintInputError();
                    break;
            }
            Console.ReadLine();
            return playerInput;
        }
        
        /// <summary>
        /// Gets a map with all positions updated
        /// </summary>
        /// <param name="player">Player's position</param>
        /// <param name="map">All map Positions</param>
        /// <returns>Returns a map position for the player</returns>
        public Map[,] GetPosition(Player player, Map[,] map, Renderer print)
        {
            // players input
            char playerInput;
            // Frees the player position
            map[player.Position.Row, player.Position.Column].Position.
                PlayerFree();

            // Gets player input
            playerInput = Console.ReadLine()[0];

            // Moves player to new free position    
            if(player.Move(map, playerInput, print))
                map[player.Position.Row, player.Position.Column].Position.
                PlayerFree();
                // Occupies inserted position with player
                map[player.Position.Row,player.Position.Column].Position.
                    PlayerOccupy();
                
            return map;
        }

        /// <summary>
        /// Asks for user name for high score
        /// </summary>
        /// <returns>User name</returns>
        public String InsertName()
        {
            string trim = "";
            bool leave = false;
            while (leave == false)
            {
                string name = Console.ReadLine();
                trim = name.Trim();
                trim = trim.Replace( " ", "_");
                if (trim.Length < 12) leave = true;
                else print.InsertShorterName();
            }
            return trim;
        }
    }
}