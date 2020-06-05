using System.Threading;

namespace RogueLike
{
    /// <summary>
    /// Runs the game loop
    /// </summary>
    sealed public class Game
    {
        //Controls the game cycle
        bool gameOver;
        //Holds all positions of the game
        Map[,] map;
    
        /// <summary>
        /// Controls main game loop
        /// </summary>
        /// <param name="rows">Number of Rows</param>
        /// <param name="columns">Number of Columns</param>
        /// <param name="seed">Seed of the game</param>
        public Game(int rows, int columns, long seed)
        {
            // Instances / Variables
            Level level     = new Level(rows, columns, seed);
            Renderer print  = new Renderer();
            Input input     = new Input(); 
            map             = new Map[rows, columns];
            bool levelUp;
            string playerInput;
            string turn;

            // Runs Menu Loop until user inputs 1 or 5
            do
            {  
                //Prints the Title Card
                print.Introduction();

                //Prints a blank line
                print.BlankLine();
                
                // Prints Initial Menu
                print.PrintMenu();
                
                // Gets user Input
                playerInput = input.MenuOptions(rows, columns);
                if(playerInput == "1") break;
                
                //Breaks the loop and quits the game
                if(playerInput == "5") break;

            } while (playerInput != "5" || playerInput != "1");
           
            // Run Game after player inputs 1
            if (playerInput == "1")
            {
                //Creates all squares and pieces of the game
                CreateMap(rows, columns);

                // Prints First Action
                print.PrintGameActions(); 
                
                //Starts the game loop using this variable and runs until its 
                //value equals TRUE
                gameOver = false;


                // Generates the map and its elements
                level.CreateLevel(map, level.LevelNum);

                // CreateEnemy(level);                
                while (gameOver == false)
                {
                    levelUp = false;

                    // If player has not moves left, it's gameover
                    if (NoRemainingMoves(level.player)){
                        print.NoMoves();
                        level.player.Die();
                    }

                    ////////////////////////////////////////////////////////////
                    // Player Movement and Map print ///////////////////////////
                    // Resets player's Movement per turn
                    level.player.MovementReset();


                    // Maintains the players turn until he moves twice or dies
                    while (level.player.Movement > 0 &&    
                            level.player.IsAlive)          
                    {
                        //Used to print Player in the game's screen
                        turn = "Player";

                        //Prints all the game's information in the screen
                        print.Map(map, rows, columns, level.PowerUps, 
                            level.Enemies, level.player, turn, level.LevelNum);
                        
                        //Asks the user for input to move the player
                        map = input.GetPosition(level, map, print);

                        // Checks if the player is in a square with a Power-Up
                        //and picks it up, printing a message on screen.
                        foreach (PowerUp powerUp in level.PowerUps)
                            if (PowerUpPosition(level.player, powerUp))
                                if (!powerUp.Picked)
                                {
                                    level.player.PickPowerUp(map, powerUp);
                                    print.GetGameActions(powerUp);
                                }
                        
                        // Checks if the player has reached the exit 
                        if (map[level.player.Position.Row, 
                            level.player.Position.Column].Position.HasExit)
                        {
                            //Adds 1 to the level number
                            level.LevelNum++;
                            
                            //Resets the tags in the player and exit position
                            level.EscapeLevel(map);

                            //Redraws the game's map and Sets new positions for
                            // the player and exit
                            CreateMap(rows, columns);
                            level.CreateLevel(map, level.LevelNum);
                            levelUp = true;
                            //Prints a message to the screen once player exits.
                            print.GetGameActions(levelUp);
                        }
                        //Prints list of the game's actions
                        print.PrintGameActions();
                    }
                    
                    // Enemy Turn
                    //Checks if the player is alive and hasn't 
                    //finished the level
                    if (level.player.IsAlive && levelUp == false)
                    {   // Prints the map, moves enemy, prints the map
                        foreach (Enemy enemy in level.Enemies)
                        {
                            //Used to print Enemy in the game's screen
                            turn = "Enemy";

                            //Prints all the game's information in the screen
                            print.Map(map, rows, columns, level.PowerUps, 
                            level.Enemies, level.player, turn,  level.LevelNum);

                            // Checks if the player is in a square with a 
                            //Power-Up and blocks the square
                            if (map[enemy.Position.Row, enemy.Position.Column]
                                .Position.HasPowerUp)
                            {
                                map[enemy.Position.Row, enemy.Position.Column].
                                    Position.EnemyFree(false);
                            }
                            else
                            {   // If the enemy moves to an empty position
                                map[enemy.Position.Row, enemy.Position.Column].
                                    Position.EnemyFree(); 
                            }

                            //Delays the game for the Enemys movement
                            Thread.Sleep(1000);

                            //Moves the enemy, occupies it's space and prints it
                            enemy.Move(level.player, 1, map);
                            map[enemy.Position.Row, enemy.Position.Column].
                                Position.EnemyOccupy();
                            print.Map(map, rows, columns, level.PowerUps, level.
                                Enemies,level.player, turn, level.LevelNum);
                        }

                        // Player gets damage if the he's 1 square distance
                        foreach (Enemy enemy in level.Enemies)
                            if (DamagePosition(level.player, enemy))
                            {
                                level.player.TakeDamage(enemy);
                                print.GetGameActions(enemy);
                            }
                        // Prints actions list
                        print.PrintGameActions();
                    }
                    
                    // Checks if the player is dead and closes the game loop
                    if (!level.player.IsAlive)
                    {
                        //Prints the last round information
                        print.Map(map, rows, columns, level.PowerUps, level.
                            Enemies, level.player, "Enemy", level.LevelNum);

                        //Prints a goodbye message
                        print.GoodBye();
                        // Saves score
                        print.SaveScore(level.LevelNum, rows, columns);
                        Quit();
                    }
                }
            }
        
        }

        /// <summary>
        /// Compares character position with another character position
        /// </summary>
        /// <param name="p1">Character1 Position</param>
        /// <param name="en">Character2 Position</param>
        /// <returns>Returns true if the distance is 1 square around
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
        /// Checks if the player can move
        /// </summary>
        /// <returns>Returns true if the player is stuck, otherwise 
        /// false</returns>
        private bool NoRemainingMoves(Player player)
        {
            int  count = 0;
            bool lose  = false;

            try
            {   
                // Checks north
                if (map[player.Position.Row - 1, player.Position.Column].
                    Position.Walkable == false) 
                        count++;               
            
               // Checks south
                if (map[player.Position.Row + 1, player.Position.Column].
                    Position.Walkable == false) 
                        count++;               
            
               // Checks east
                if (map[player.Position.Row, player.Position.Column + 1].
                    Position.Walkable == false) 
                        count++;               
            
               // Checks Column
                if (map[player.Position.Row, player.Position.Column - 1].
                    Position.Walkable == false) 
                        count++; 
              
            } catch {count++;};
            
            // If count == 4, it's gameover
            if (count == 4) lose = true;
            return lose;
        }



        /// <summary>
        /// Quits the game loop
        /// </summary>
        private void Quit() => gameOver = true;
    }
}