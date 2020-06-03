using System; // SO PARA TESTES, APAGAR <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
using System.Threading;

namespace RogueLike
{
    sealed public class Game
    {
        bool gameOver;
        Player player;
        // Enemy[] enemies;
        // PowerUp[] powerUps;
        Map[,] map;
    
        /// <summary>
        /// Controls main game loop
        /// </summary>
        /// <param name="rows">Number of Rows</param>
        /// <param name="columns">Number of Columns</param>
        public Game(int rows, int columns)
        {
            // Instances / Variable
            Level level     = new Level(rows, columns);
            Renderer print = new Renderer();
            Input input     = new Input(); 
            map             = new Map[rows, columns];
            string playerInput;
            string turn;

            ////////////////////////////////////////////////////////////////////
            // Runs Menu Loop
            do
            {
                print.BlankLine();
                // Prints Initial Menu
                print.PrintMenu();
                // Gets user Input
                playerInput = input.MenuOptions();
                if(playerInput == "1") break;
                if(playerInput == "5") break;
            } while (playerInput != "5" || playerInput != "1");
            ////////////////////////////////////////////////////////////////////
            
            ////////////////////////////////////////////////////////////////////
            // Run Game
            if (playerInput == "1")
            {
                CreateMap(rows, columns);
                CreatePlayer(0, rows, columns); ///////////////////////// < METER O NUMERO RANDOM EM VEZ DE 0
                // CreatePowerUp(1); ///////////////////////// < METER O NUMERO RANDOM EM VEZ DE 0
                // CreateEnemy(1); ///////////////////////// < METER O NUMERO RANDOM EM VEZ DE 0
                

                print.PrintGameActions(); // Prints First Action

                gameOver = false;
                level.CreateLevel(map);
                // CreateEnemy(level);                
                while (gameOver == false)
                {
                    // Resets Movement
                    player.MovementReset(); 

                    ////////////////////////////////////////////////////////////
                    // Player Movement and Map print ///////////////////////////
                    while (player.Movement > 0 &&    // Player has 2 Movements
                            player.IsAlive)          // Player is alive
                    {
                        turn = "Player";
                        print.Map(map, rows, columns, level.PowerUps, level.enemies, 
                                player, turn);
                        map = input.GetPosition(player, map, print);
                        // Checks if the player got any powerUp
                        foreach (PowerUp powerUp in level.PowerUps)
                            if (PowerUpPosition(player, powerUp))
                                if (!powerUp.Picked)
                                {
                                    player.PickPowerUp(map, powerUp);
                                    print.GetGameActions(powerUp);
                                }
                        print.PrintGameActions();
                    }
                    ////////////////////////////////////////////////////////////

                    // Enemy Turn //////////////////////////////////////////////
                    if (player.IsAlive)
                    {   // Prints the map, moves enemy, prints the map
                        foreach (Enemy enemy in level.enemies)
                        {
                            turn = "Enemy";
                            print.Map(map, rows, columns, level.PowerUps, level.enemies, 
                                    player, turn);
                            if (map[enemy.Position.Row, enemy.Position.Column].Position.HasPowerUp)
                            {
                                map[enemy.Position.Row, enemy.Position.Column].
                                    Position.EnemyFree(false);
                            }
                            else
                            {
                                map[enemy.Position.Row, enemy.Position.Column].
                                    Position.EnemyFree(); 
                            }
                            Thread.Sleep(1000);
                            enemy.Move(player, 1, map);
                            map[enemy.Position.Row, enemy.Position.Column].
                                Position.EnemyOccupy();
                            print.Map(map, rows, columns, level.PowerUps, level.enemies, 
                                    player, turn);
                        }
                        // Player gets damage if the he's 1 square distance
                        foreach (Enemy enemy in level.enemies)
                            if (DamagePosition(player, enemy))
                            {
                                player.TakeDamage(enemy);
                                print.GetGameActions(enemy);
                            }
                        print.PrintGameActions();
                    }
                    ////////////////////////////////////////////////////////////


                    // Prints player HP or ends the game
                    if (!player.IsAlive)
                    {
                        print.Map(map, rows, columns, level.PowerUps, level.enemies, 
                                player, "Enemy");
                        print.GoodBye();
                        Quit();
                    }
                }
            }
        
        }
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Compares character position with another character position
        /// </summary>
        /// <param name="p1">Character1 Position</param>
        /// <param name="en">Character2 Position</param>
        /// <returns>True if the distance is 1 square around
        ///  otherwise false</returns>
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
        /// <returns>True if both positions are the same 
        /// otherwise false</returns>
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

        /// <summary>
        /// Creates Power-Ups based on a random number
        /// </summary>
        /// <param name="i">Random Generated Number</param>
        // private void CreatePowerUp(int i)
        // {
        //     powerUps = new PowerUp[i];

        //     powerUps[0] = new PowerUp(new Position(0,1), 16);    // TESTEEEE

        //     foreach (PowerUp powerUp in powerUps)
        //     {   
        //         map[powerUp.Position.Row, 
        //             powerUp.Position.Column].Position.PowerUpOccupy();
        //     }
        // }
        
        /// <summary>
        /// Creates Enemies based on a random number
        /// </summary>
        /// <param name="i">Random Generated Number</param>
        // private void CreateEnemy(int i)
        // {
        //     enemies = new Enemy[i];

        //     enemies[0] = new Enemy(new Position(2,2), 5);   // TESTEEEE
            
        //     foreach (Enemy enemy in enemies)
        //     {
        //         map[enemy.Position.Row, 
        //             enemy.Position.Column].Position.EnemyOccupy();
        //     }
        // }

        /// <summary>
        /// Stops the game loop and exits game
        /// </summary>
        private void Quit() => gameOver = true;
    }
}