using System;
namespace RogueLike
{
    sealed public class Input
    {

        string playerInput;
        private static Renderer print;
        /// <summary>
        /// Class Constructor
        /// </summary>
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
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
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
        /// <param name="player">Player's position</param>
        /// <param name="map">All map Positions</param>
        /// <returns>Returns a map position for the player</returns>
        public Map[,] GetPosition(Player player, Map[,] map)
        {
            // players input
            char playerInput;
                // Frees the player position
                map[player.Position.Row, player.Position.Column].Position.
                    PlayerFree();

                // Gets player input
                playerInput = Console.ReadLine()[0];

                // Moves player to new free position
                if(player.Move(map, playerInput) == false)
                {
                   Console.WriteLine("An enemy blocks the way!");
                }
                    
                else
                    map[player.Position.Row, player.Position.Column].Position.
                    PlayerFree();
                    // Occupies inserted position with player
                    map[player.Position.Row,player.Position.Column].Position.
                        PlayerOccupy();
               

            return map;
        }
    }
}