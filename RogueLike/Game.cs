using System; // SO PARA TESTES, APAGAR <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

namespace RogueLike
{
    sealed public class Game
    {
        bool gameOver;
        Player player;
        Map[,] map;
        


        public Game(int numberOfRows, int numberOfColumns)
        {
            // Instances / Variables
            Renderer render = new Renderer();
            Input input     = new Input(); 
            map             = new Map[numberOfRows, numberOfColumns];
            string playerInput;

            // Prints Initial Menu
            render.PrintMenu();

            // Gets user Input
            playerInput = input.MenuOptions();


            // Runs Gameloop
            //Starts the game loop after choosing option 1.
            if (playerInput == "1")
            {
                CreateMap(numberOfRows, numberOfColumns);
                CreatePlayer(0, numberOfRows, numberOfColumns); ///////////////////////// < meter o numero random em vez do 0
                gameOver = false;
                while (gameOver == false)
                {
                    // Renders map
                    render.Map(map, numberOfRows, numberOfColumns);
                    map = input.GetPosition(player, map);

                }
            }

            // If players choses option 5 on the menu Quits the game
            else if (playerInput == "5")
            {
            }
        }


        /// <summary>
        /// Compares characters positions
        /// </summary>
        /// <param name="char1">Character 1 position</param>
        /// <param name="char2">Character 2 position</param>
        /// <returns></returns>
        private bool ComparePosition(Character char1, Character char2)
        {
            bool occupied = false;
            if (char1.Position.Row == char2.Position.Row &&
                char1.Position.Column == char2.Position.Column)
                occupied = true;
            return occupied;
        }


        /// <summary>
        /// Creates the game map
        /// </summary>
        /// <param name="rows">Number of rows in the game</param>
        /// <param name="columns">Number of columns in the gaem</param>
        private void CreateMap(int rows, int columns)
        {
            for (int i = 0; i < rows; i++) 
                for (int j = 0; j < columns; j++)
                    map[i,j] = new Map (new Position(i,j));
        }

        
        /// <summary>
        /// Creates player
        /// </summary>
        /// <param name="x">Random number to spawn player</param>
        /// <param name="rows">Number of rows in the game</param>
        /// <param name="columns">Number of columns in the game</param>
        private void CreatePlayer(int x, int rows, int columns)
        {
            // TEMPORARIO SO PARA TESTAR A CRIACAO DO JOGADOR <<<<<<<<<<
            player = new Player(new Position(x, 0), rows, columns);
            map[x, 0].Position.HasPlayer    = true;
            map[x, 0].Position.Playable     = false;
            // TEMPORARIO SO PARA TESTAR A CRIACAO DO JOGADOR <<<<<<<<<<
        }
        

        private void Quit() => gameOver = true;
    }
}