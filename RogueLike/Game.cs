using System; // SO PARA TESTES, APAGAR <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

namespace RogueLike
{
    sealed public class Game
    {
        bool gameOver;
        Player player;
        Map[,] map;

        public Game(int rows, int columns)
        {
            // Instances / Variables
            Level level     = new Level(rows, columns);
            Renderer render = new Renderer();
            Input input     = new Input(); 
            map             = new Map[rows, columns];
            string playerInput;
            Enemy[] enemies = new Enemy[100];
            // Prints Initial Menu
            render.PrintMenu();

            // Gets user Input
            playerInput = input.MenuOptions();


            // Runs Gameloop
            //Starts the game loop after choosing option 1.
            if (playerInput == "1")
            {
                CreateMap(rows, columns);
                CreatePlayer(0, rows, columns); ///////////////////////// < METER O NUMERO RANDOM EM VEZ DE 0
                
                // enemies[0] = new Enemy(new Position(1,1), 1); ////////////////// < INIMIGO PARA TESTAR

                gameOver = false;
                while (gameOver == false)
                {

                    level.CreateLevel(map);

                    // Resets Movement
                    player.MovementReset(); 

                    // Player Movement and Map render -> 2 Movements
                    render.Map(map, rows, columns);
                    map = input.GetPosition(player, map);
                    render.Map(map, rows, columns);
                    map = input.GetPosition(player, map);


                    // Player gets damage if the he's 1 square distance
                    // foreach (Enemy enemy in enemies)
                    //     if (DamagePosition(player, enemy))
                    //         player.TakeDamage(enemy);
                    

                    Console.WriteLine("\nHP --------- " + player.HP); /////////////// TEMPORARIO PARA TESTAR
                    level.LevelNum ++;
                }
            }

            // If players choses option 5 on the menu Quits the game
            else if (playerInput == "5")
            {
            }
        }


        /// <summary>
        /// Compares character position with map position
        /// </summary>
        /// <param name="p1">Character position</param>
        /// <param name="map">Map</param>
        /// <param name="r">Number of Rows</param>
        /// <param name="c">Number of Columns</param>
        /// <returns>True if the position is the same</returns>
        private bool ComparePosition(Character p1, Map[,] map, int r, int c)
        {
            bool occupied = false;
            for (int i = 0; i < r; i++) 
                for (int j = 0; j < c; j++)
                    if (p1.Position.Row == map[i,j].Position.Row &&
                        p1.Position.Column == map[i,j].Position.Column)
                        occupied = true;

            return occupied;
        }

        /// <summary>
        /// Compares character position with another character position
        /// </summary>
        /// <param name="p1">Character1 Position</param>
        /// <param name="en">Character2 Position</param>
        /// <returns>True if the distance is 1 square around</returns>
        private bool DamagePosition(Character p1, Character en)
        {
            bool occupied = false;
                if (p1.Position.Row == en.Position.Row -1 &&
                    p1.Position.Column == en.Position.Column ||
                    p1.Position.Row == en.Position.Row +1 &&
                    p1.Position.Column == en.Position.Column ||
                    p1.Position.Column == en.Position.Column -1 &&
                    p1.Position.Row == en.Position.Row ||
                    p1.Position.Column == en.Position.Column +1 &&
                    p1.Position.Row == en.Position.Row)
                    occupied = true;
            return occupied;
        }

        /// <summary>
        /// Creates the game map
        /// </summary>
        /// <param name="rows">Number of rows in the game</param>
        /// <param name="columns">Number of columns in the game</param>
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
            player = new Player(new Position(x, 0), rows, columns);
            map[x, 0].Position.PlayerOccupy();
        }
        

        private void Quit() => gameOver = true;
    }
}