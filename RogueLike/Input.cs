using System;
namespace RogueLike
{
    /// <summary>
    /// Responsible for every user input on the console
    /// </summary>
    sealed internal class Input
    {
        /// <summary>
        /// Creates options menu
        /// </summary>
        /// <returns>Returns player's option</returns>
        internal string MenuOptions()
        {
            Renderer print = new Renderer();
            HighScoreManager highScore = new HighScoreManager();
            string playerInput;

            //Keeps running until players starts new game
            playerInput = Console.ReadLine();
            switch(playerInput)
            {
                //Starts new game
                case "1":
                    return playerInput;
                
                //Prints the Highscore Screen
                case "2":
                    highScore.PrintScore();
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
        /// <param name="level">Gets level to get the player</param>
        /// <param name="map">Gets map to get a position</param>
        /// <param name="print">Gets renderer</param>
        internal void GetPosition(Level level , Map[,] map, Renderer print)
        {
            Save save = new Save();
            // players input
            ConsoleKeyInfo playerInput;
            // Frees the player position
            map[level.player.Row, level.player.Column].
                Free("player");

            // Gets player input
            playerInput = Console.ReadKey();
            
            // Checks if the player pressed the ESC key and quits the game
            if (playerInput.Key == ConsoleKey.Escape)
            {
                save.GetSave(level.LevelNum, "quit");
                level.player.Die();
                level.player.LeaveGame();
                Game.ForceExit = true;
                return;
            }

            // Moves player to new free position    
            if(level.player.Move(map, playerInput, print))
                map[level.player.Row, level.player.Column].
                Free("player");

            // Occupies inserted position with player
            map[level.player.Row,level.player.Column].
            Occupy("player");
                
        }

        /// <summary>
        /// Asks for user name for high score
        /// </summary>
        /// <returns>User name</returns>
        internal String InsertName()
        {
            Renderer print = new Renderer();
            string trim = "";
            bool leave = false;
            while (leave == false)
            {   // Removes spaces from the string and accepts a
                // string length shorter than 12 characters   
                string name = Console.ReadLine();
                trim = name.Trim();
                trim = trim.Replace( " ", "_");
                trim = trim.Replace(".", "");
                if (trim.Length < 12 && trim.Length > 0) leave = true;
                else print.InsertShorterName();
            }
            return trim;
        }
        internal string InsertFileName()
        {
            Renderer print = new Renderer();
            string trim = "";
            bool leave = false;
            while (leave == false)
            {   // Removes spaces from the string and accepts a
                // string length shorter than 12 characters   
                string name = Console.ReadLine();
                trim = name.Trim();
                trim = trim.Replace( " ", "_");
                trim = trim.Replace(".", "");
                if (trim.Length < 12 && trim.Length > 0) leave = true;
                else print.InsertShorterName();
            }
            return trim;
        }
    
        /// <summary>
        /// Gets the player's intention of saving the game progress or not
        /// </summary>
        /// <returns></returns>
        internal string GetSaveIntention() 
        {
            string input;
            input = Convert.ToString(Console.ReadKey().KeyChar);
            return input;
        }


    }
}