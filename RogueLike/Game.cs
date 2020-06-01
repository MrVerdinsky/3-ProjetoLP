using System; // SO PARA TESTES, APAGAR <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

namespace RogueLike
{
    sealed public class Game
    {
        bool gameOver;

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
            while (gameOver == false)
            {
                // Renders map
                render.Map(numberOfRows, numberOfColumns);



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