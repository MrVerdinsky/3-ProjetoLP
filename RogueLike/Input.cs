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
            bool inMenu = false;
            //Keeps running until players starts new game
            do
            {
                playerInput = Console.ReadLine();
                if (!inMenu)
                {
                    switch(playerInput)
                    {
                        case "1":
                            break;
                        case "2":
                            inMenu = true;
                            break;
                        case "3":
                            print.PrintInstructions();
                            inMenu = true;
                            break;
                        case "4":
                            print.PrintCredits();
                            inMenu = true;
                            break;
                        case "5":
                            print.PrintExitMsg();
                            return playerInput;
                        //Returns the input here so the players goes back to main menu
                            
                        default:
                            print.PrintInputError();
                            break;
                    }
                }
                else
                {
                    if (playerInput == "" || playerInput != "1" ||
                        playerInput != "2" || playerInput != "3" || 
                        playerInput != "4" || playerInput != "5")
                    {
                        print.PrintMenu();
                        MenuOptions();
                    }
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
                if(player.Move(map, playerInput) == true)
                    map[player.Position.Row, player.Position.Column].Position.
                    PlayerFree();
                    // Occupies inserted position with player
                    map[player.Position.Row,player.Position.Column].Position.
                        PlayerOccupy();
               

            return map;
        }
    }
}