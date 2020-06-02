using System; // SO PARA TESTES, APAGAR <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

namespace RogueLike
{
    sealed public class Game
    {
        bool gameOver;
        Player player;
        Enemy[] enemies;
        PowerUp[] powerUps;
        Map[,] map;
        public Game(int rows, int columns)
        {
            // Instances / Variables
            Level level     = new Level(rows, columns);
            Renderer render = new Renderer();
            Input input     = new Input(); 
            map             = new Map[rows, columns];
            string playerInput;

            ////////////////////////////////////////////////////////////////////
            // Prints Initial Menu
            render.PrintMenu();
            // Runs Menu Loop
            do
            {
                // Gets user Input
                playerInput = input.MenuOptions();
                if(playerInput == "1") break;
                if(playerInput == "5") break;

                /// ESPALHAR MAGIA AQUI E METER AS OPCOES, A 1 E a 5 JA TAO FEITAS
                

            } while (playerInput != "5" || playerInput != "1");
            ////////////////////////////////////////////////////////////////////
            
            ////////////////////////////////////////////////////////////////////
            // Run Game
            if (playerInput == "1")
            {
                CreateMap(rows, columns);
                CreatePlayer(0, rows, columns); ///////////////////////// < METER O NUMERO RANDOM EM VEZ DE 0
                CreatePowerUp(1); ///////////////////////// < METER O NUMERO RANDOM EM VEZ DE 0
                // CreateEnemy(level); ///////////////////////// < METER O NUMERO RANDOM EM VEZ DE 0
                
                
                // enemies[0] = new Enemy(new Position(1,1), 1); ////////////////// < INIMIGO PARA TESTAR

                gameOver = false;
                level.CreateLevel(map);
                while (gameOver == false)
                {
                    // Creates enemies
                    CreateEnemy(level);
                    // Resets Movement
                    // player.MovementReset();                 
                    // Player Movement and Map render -> MOVEMENT 1 ////////////
                    render.Map(map, rows, columns, powerUps, level.enemies);
                    map = input.GetPosition(player, map);
                    // Checks if the player got any powerUp
                    foreach (PowerUp powerUp in powerUps)
                        if (PowerUpPosition(player, powerUp))
                            player.PickPowerUp(powerUp);
                            
                    ////////////////////////////////////////////////////////////
                    Console.WriteLine("\nHP --------- " + player.HP); /////////////// TEMPORARIO PARA TESTAR
                    
                    // Player Movement and Map render -> MOVEMENT 2 ////////////
                    render.Map(map, rows, columns, powerUps, level.enemies);
                    map = input.GetPosition(player, map);

                    // Checks if the player got any powerUp
                    foreach (PowerUp powerUp in powerUps)
                        if (PowerUpPosition(player, powerUp))
                            player.PickPowerUp(powerUp);
                    // foreach (Map map in map)
                    // {
                    //     Console.WriteLine($"Has enemy: {map.Position.HasEnemy}");
                    // }
                    ////////////////////////////////////////////////////////////
  

                    // Player gets damage if the he's 1 square distance
                    foreach (Enemy enemy in level.enemies)
                        if (DamagePosition(player, enemy))
                            player.TakeDamage(enemy);
                    

                    Console.WriteLine("\nHP --------- " + player.HP); /////////////// TEMPORARIO PARA TESTAR
                    // level.LevelNum ++;
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////

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
        /// Compares character position with powerUp position
        /// </summary>
        /// <param name="p1">Character position</param>
        /// <param name="powerUp">PowerUp position</param>
        /// <returns>True if both positions are the same</returns>
        private bool PowerUpPosition(Character p1, PowerUp powerUp)
        {
            bool occupied = false;
                if (p1.Position.Row == powerUp.Position.Row &&
                    p1.Position.Column == powerUp.Position.Column)
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

        private void CreatePowerUp(int i)
        {
            powerUps = new PowerUp[i];

            powerUps[0] = new PowerUp(new Position(0,1), 4);    // TESTEEEE

            foreach (PowerUp powerUp in powerUps)
            {   
                map[powerUp.Position.Row, 
                    powerUp.Position.Column].Position.PowerUpOccupy();
            }
        }
        private void CreateEnemy(Level level)
        {            
            foreach (Map aux in map)
            {
                aux.Position.EnemyFree();
            }
            foreach (Enemy enemy in level.enemies)
            {
                Console.WriteLine($"enemy pos: {enemy.Position.Row}, {enemy.Position.Column}");
                map[enemy.Position.Row, 
                    enemy.Position.Column].Position.EnemyOccupy();
            }
        }

        private void Quit() => gameOver = true;
    }
}