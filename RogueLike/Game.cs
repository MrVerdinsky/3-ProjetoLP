using System; // SO PARA TESTES, APAGAR <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

namespace RogueLike
{
    sealed public class Game
    {
        bool gameOver;
        string playerInput;
        public Game(int numberOfRows, int numberOfColumns)
        {
            // Instances / Variables
            Renderer render = new Renderer();
            Input input     = new Input(); 
            Map map         = new Map(numberOfRows, numberOfColumns);

            // Prints Initial Menu
            render.PrintMenu();

            // Gets user Input


            // Runs Gameloop
            gameOver = false;
            playerInput = input.MenuOptions();
            
            //Starts the game loop after choosing option 1.
            if (playerInput == "1")
            {
                while (gameOver == false)
                {
                    // Renders map
                    render.Map(numberOfRows, numberOfColumns);
                    Quit();
                }
            }

            //If players choses option 5 on the menu
            else if (playerInput == "5")
            {
                // Quits the game
                Quit();
            }


        }

        public void Quit()
        {
            gameOver = true;
        }
    }
}