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
        
        /// <summary>
        /// Controls all Menu Options until players chooses new game
        /// </summary>
        /// <returns>Returns the user's input</returns>
        public string MenuOptions()
        {
            //Keeps running until players starts new game
            playerInput = Console.ReadLine();
            switch(playerInput)
            {
                //Starts new game
                case "1":
                    return playerInput;
                
                //Prints the Highscore Screen
                case "2":
                    break;

                //Prints the game's Instructions
                case "3":
                    print.PrintInstructions();
                    break;

                //Prints the game's developers
                case "4":
                    print.PrintCredits();
                    break;
                
                //Prints Exit message and closes the game
                case "5":
                    print.PrintExitMsg();
                    return playerInput;
                //Returns the input here so the players goes back to main menu
                default:
                    print.PrintInputError();
                    break;
            }
            //Asks the user for an input to leave the option screen
            Console.ReadLine();
            return playerInput;
        }
        
        /// <summary>
        /// Gets a map with all positions updated
        /// </summary>
        /// <param name="player">Player's position</param>
        /// <param name="map">All map Positions</param>
        /// <returns>Returns a map position for the player</returns>
        public Map[,] GetPosition(Level level , Map[,] map, Renderer print)
        {
            // players input
            char playerInput;
            // Frees the player position
            map[level.player.Position.Row, level.player.Position.Column].Position.
                PlayerFree();

            // Gets player input
            playerInput = Console.ReadLine()[0];

            // Moves player to new free position    
            if(level.player.Move(map, playerInput, print))
                map[level.player.Position.Row, level.player.Position.Column].Position.
                PlayerFree();
                // Occupies inserted position with player
                map[level.player.Position.Row,level.player.Position.Column].Position.
                    PlayerOccupy();
                
            return map;
        }
    }
}