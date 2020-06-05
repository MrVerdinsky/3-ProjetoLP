using System.Threading;

namespace RogueLike
{
    /// <summary>
    /// Runs the game loop
    /// </summary>
    sealed public class Game
    {
        // Game rows and game columns
        static public int rows;
        static public int columns;
        static public int Seed;
        // Controls the game cycle
        bool gameOver;
        //Holds all positions of the game
        Map[,] map;
    
        /// <summary>
        /// Controls main game loop
        /// </summary>
        /// <param name="rows">Number of Rows</param>
        /// <param name="columns">Number of Columns</param>
        /// <param name="seed">Seed of the game</param>
        public Game(int gameRows, int gameColumns, int seed)
        {
            // Instances / Variables
            rows            = gameRows;
            columns         = gameColumns;
            Seed            = seed;
            Level level     = new Level();
            Renderer print  = new Renderer();
            Input input     = new Input(); 
            map             = new Map[rows, columns];
            HighScoreManager highScore = new HighScoreManager();
            
            string playerInput;
            string turn = "";
            bool firstTurnCheck = true;

            bool levelUp = false;
            gameOver = false;

            ////////////////////////////////////////////////////////////////////
            // MAIN MENU ///////////////////////////////////////////////////////
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
                playerInput = input.MenuOptions();
                if(playerInput == "1") break;
                
                //Breaks the loop and quits the game
                if(playerInput == "5") break;

            } while (playerInput != "5" || playerInput != "1");
            ////////////////////////////////////////////////////////////////////

            // NEW GAME ////////////////////////////////////////////////////////
            // Run Game after player inputs 1
            if (playerInput == "1")
            {
                //Creates all squares and pieces of the game
                CreateMap();

                // Generates the map and its elements
                level.CreateLevel(map, level.LevelNum);

                // MAIN GAME LOOP //////////////////////////////////////////////
                while (gameOver == false)
                {
                    // ON LEVEL UP /////////////////////////////////////////////
                    if (levelUp)
                    { 
                        // Prints "loading" bar
                        print.BlankLine();
                        for (int i = 0; i < 58; i++)
                        {
                            Thread.Sleep(25);
                            print.Dot();
                        }
                        print.BlankLine();
                        // Creates new level elements
                        LevelUp(level, print);
                        print.PrintGameActions(); 
                        levelUp = false;
                        firstTurnCheck = true;
                    } else  print.PrintGameActions(); 

                    // LEVEL GAME LOOP /////////////////////////////////////////
                    while (levelUp == false && level.player.IsAlive)
                    {
                        // If player has not moves left, it's gameover
                        if (NoRemainingMoves(level.player)){
                            print.NoMoves();
                            level.player.Die();
                        }    // Resets player's Movement per turn
                        else level.player.MovementReset();                

                        // Player's turn until he moves twice or dies///////////
                        while (level.player.Movement > 0 &&    
                                level.player.IsAlive)          
                        {
                            //Used to print Player in the game's screen
                            turn = "Player";

                            //Prints all the game's information in the screen
                            print.Map(map, level.PowerUps, level.Enemies, 
                                level.player, turn, level.LevelNum, 
                                firstTurnCheck);

                            // Ends threading on render
                            firstTurnCheck = false;
                            
                            //Asks the user for input to move the player
                            input.GetPosition(level, map);

                            //Checks if the player's in a square with a Power-Up
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
                                levelUp = true;
                                break;
                            }
                            else
                            //Prints list of the game's actions
                            print.PrintGameActions();
                        }
                        
                        // Enemy Turn //////////////////////////////////////////
                        //Checks if the player is alive and hasn't 
                        //finished the level
                        if (level.player.IsAlive && levelUp == false)
                        {   // Prints the map, moves enemy, prints the map
                            foreach (Enemy enemy in level.Enemies)
                            {
                                //Used to print Enemy in the game's screen
                                turn = "Enemy";

                                // Prints all the game's information 
                                print.Map(map, level.PowerUps, level.Enemies,
                                        level.player, turn, level.LevelNum, 
                                        firstTurnCheck);

                                // Checks if the player is in a square with a 
                                //Power-Up and blocks the square
                                if (map[enemy.Position.Row, 
                                    enemy.Position.Column].Position.HasPowerUp)
                                {
                                    map[enemy.Position.Row, 
                                    enemy.Position.Column].
                                    Position.EnemyFree(false);
                                }
                                else
                                {   // If the enemy moves to an empty position
                                    map[enemy.Position.Row, enemy.
                                    Position.Column].Position.EnemyFree(); 
                                }

                                //Delays the game for the Enemys movement
                                Thread.Sleep(500);

                                // Moves the enemy, occupies the position 
                                // and prints and prints it
                                enemy.Move(level.player, level.EnemyMoveNum, 
                                            map);
                                map[enemy.Position.Row, enemy.Position.Column].
                                    Position.EnemyOccupy();
                                print.Map(map, level.PowerUps, level.Enemies,
                                        level.player, turn, level.LevelNum,
                                        firstTurnCheck);
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
                    }

                    // Checks if the player is dead and closes the game loop
                    if (!level.player.IsAlive)
                    {
                        //Prints the last round information
                        print.Map(map, level.PowerUps, level.Enemies, 
                                level.player, turn, level.LevelNum,
                                firstTurnCheck);

                        //Prints a goodbye message
                        print.GoodBye();
                        // Saves score
                        highScore.SaveScore(level.LevelNum);
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
        private bool DamagePosition(ObjectPosition p1, ObjectPosition en)
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
        /// Compares ObjectPosition position with powerUp position
        /// </summary>
        /// <param name="p1">ObjectPosition position</param>
        /// <param name="powerUp">PowerUp position</param>
        /// <returns>True if both positions are the same 
        /// otherwise false</returns>
        private bool PowerUpPosition(ObjectPosition p1, PowerUp powerUp)
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
        private void CreateMap()
        {
            for (int i = 0; i < Game.rows; i++) 
                for (int j = 0; j < Game.columns; j++)
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
            {   // Checks north
                if (map[player.Position.Row - 1, player.Position.Column].
                    Position.Walkable == false) count++;               
            } catch {count++;};
            try
            {   // Checks south
                if (map[player.Position.Row + 1, player.Position.Column].
                    Position.Walkable == false) count++;               
            } catch {count++;};
            try
            {   // Checks east
                if (map[player.Position.Row, player.Position.Column + 1].
                    Position.Walkable == false) count++;               
            } catch {count++;};
            try
            {   // Checks Column
                if (map[player.Position.Row, player.Position.Column - 1].
                    Position.Walkable == false) count++;               
            } catch {count++;};
            // If count == 4, it's gameover
            if (count == 4) lose = true;
            return lose;
        
        }

        /// <summary>
        /// Passes to the next level after reaching the exit
        /// </summary>
        /// <param name="level">Gets level number</param>
        /// <param name="print">Gets Renderer class to print</param>
        /// <param name="rows">Gets game's number of rows</param>
        /// <param name="columns">Gets game's number of columns</param>
        private void LevelUp(Level level, Renderer print)
        {
            //Adds 1 to the level number
            level.LevelNum++;
            
            //Resets the tags in the player and exit position
            //level.EscapeLevel(map);

            //Prints a message to the screen once player exits.
            print.GetGameActions();

            //Redraws the game's map and Sets new positions for
            // the player and exit
            CreateMap();
            level.CreateLevel(map, level.LevelNum);
        }

        /// <summary>
        /// Quits the game loop
        /// </summary>
        private void Quit() => gameOver = true;
    }
}