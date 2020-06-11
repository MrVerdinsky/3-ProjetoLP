using System.Threading;

namespace RogueLike
{
    /// <summary>
    /// Responsible for the game events
    /// </summary>
    sealed internal class Game
    {
        /// <summary>
        /// Game's max rows
        /// </summary>
        static internal int rows;

        /// <summary>
        /// Game's max columns
        /// </summary>
        static internal int columns;

        /// <summary>
        /// Game's seed 
        /// </summary>
        static internal int Seed;

        /// <summary>
        /// Checks if the user closed the game
        /// </summary>
        static internal bool ForceExit;
        
        /// <summary>
        /// Checks if the game is over by win or lost consdition
        /// </summary>
        private bool gameOver;
        
        /// <summary>
        /// Holds all of the game's positions
        /// </summary>
        private Map[,] map;

        /// <summary>
        /// Holds of the level elements
        /// </summary>
        private Level level;

        /// <summary>
        /// Checks if the player loaded a save file or if its a new game
        /// </summary>
        private bool LoadedGame = false;

        /// <summary>
        /// Creates an instance of the save class
        /// </summary>
        private Save save = new Save();

        /// <summary>
        /// Set the game properties to the given paramaters
        /// </summary>
        /// <param name="gameRows">Number of Rows</param>
        /// <param name="gameColumns">Number of Columns</param>
        /// <param name="seed">Seed of the game</param>
        internal Game(int gameRows, int gameColumns, int seed)
        {
            ForceExit       = false;
            rows            = gameRows;
            columns         = gameColumns;
            Seed            = seed;
            level           = new Level();
        }
        /// <summary>
        /// Set the game properties to the values loaded from the save file
        /// </summary>
        /// <param name="fileName">Save file name</param>
        internal Game(string fileName)
        {
            ForceExit = false;
            level = new Level();
            save.LoadSave(level, fileName);
            LoadedGame = true;
            // Calls loading save method

        }

        /// <summary>
        /// Executes the main gameloop
        /// </summary>
        internal void RunGame()
        {
            // Instances / Variables

            // Level level     = new Level();
            Renderer print  = new Renderer();
            Input input     = new Input(); 
            map             = new Map[rows, columns];
            HighScoreManager highScore = new HighScoreManager();
            
            string playerInput = "";
            string turn = "";
            bool firstTurnCheck = true;

            bool levelUp = false;
            gameOver = false;

            ////////////////////////////////////////////////////////////////////
            // MAIN MENU ///////////////////////////////////////////////////////
            // Runs Menu Loop until user inputs 1 or 5
            if (!LoadedGame)
            {
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

                } while (true);
            }
            else
                print.WelcomeBack();
            ////////////////////////////////////////////////////////////////////

            // NEW GAME ////////////////////////////////////////////////////////
            // Run Game after player inputs 1
            if (playerInput == "1" || LoadedGame == true)
            {
                // if (LoadedGame)
                

                //Creates all squares and pieces of the game
                CreateMap();
                // Generates the map and its elements
                level.CreateLevel(map);
 
                // MAIN GAME LOOP //////////////////////////////////////////////
                while (gameOver == false)
                {
                    // ON LEVEL UP /////////////////////////////////////////////
                    if (levelUp)
                    { 
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
                        // If player has no moves left, it's gameover
                        if (NoRemainingMoves(level.player))
                        {
                            print.NoMoves();
                            level.player.Die();
                        }    
                        // Resets player's Movement per turn
                        level.player.MovementReset();                

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
                            input.GetPosition(level, map, print);

                            //Checks if the player's in a square with a Power-Up
                            //and picks it up, printing a message on screen.
                            foreach (PowerUp powerUp in level.PowerUps)
                                if (PowerUpPosition(level.player, powerUp))
                                    if (!powerUp.Picked)
                                    {
                                        level.player.PickPowerUp(map, powerUp);
                                        print.GetGameActions(powerUp);
                                    }
                            
                            if (level.player.Walked)
                                level.player.MovementDamage();

                            // Checks if the player has reached the exit 
                            if (map[level.player.Row,level.player.Column].
                                IsExit)
                            {
                                map[level.player.Row,level.player.Column].
                                    Free("exit");
                                if (level.player.IsAlive)
                                {
                                    levelUp = true;
                                    break;
                                }
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
                                // level.SetEnemyMoveNum();
                                //Used to print Enemy in the game's screen
                                turn = "Enemy";

                                // Prints all the game's information 
                                print.Map(map, level.PowerUps, level.Enemies,
                                        level.player, turn, level.LevelNum, 
                                        firstTurnCheck);

                                // Checks if the player is in a square with a 
                                //Power-Up and blocks the square
                                if (map[enemy.Row,enemy.Column].IsPowerUp)
                                {
                                    map[enemy.Row, 
                                    enemy.Column].
                                    Free("enemy_power_up");
                                }
                                else
                                {   // If the enemy moves to an empty position
                                    map[enemy.Row, enemy.Column].Free("enemy"); 
                                }

                                //Delays the game for the Enemys movement
                                Thread.Sleep(500);

                                // Moves the enemy, occupies the position 
                                // and prints and prints it
                                enemy.Move(level.player, 1, 
                                            map);
                                map[enemy.Row, enemy.Column].Occupy("enemy");
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
                        if (level.player.HasLeft == false)
                            print.GoodBye();
                        else
                            print.LeftBye();
                        // Saves score
                        if (!(ForceExit))
                            highScore.SaveScore(level.LevelNum);
                        Quit();
                    }
                }
            }
    
    }

        /// <summary>
        /// Compares a position with another position
        /// </summary>
        /// <param name="p1">Position 1</param>
        /// <param name="en">Position 2</param>
        /// <returns>Returns true if the distance is 1 square around
        ///  otherwise false</returns>
        private bool DamagePosition(Position p1, Position en)
        {
            bool occupied = false;
                if (p1.Row == en.Row -1 && p1.Column == en.Column ||
                    p1.Row == en.Row +1 && p1.Column == en.Column ||
                    p1.Column == en.Column -1 && p1.Row == en.Row ||
                    p1.Column == en.Column +1 && p1.Row == en.Row)
                    occupied = true;
            return occupied;
        }

        /// <summary>
        /// Compares p1 position with powerUp position
        /// </summary>
        /// <param name="p1">Position position</param>
        /// <param name="powerUp">PowerUp position</param>
        /// <returns>True if both positions are the same 
        /// otherwise false</returns>
        private bool PowerUpPosition(Position p1, PowerUp powerUp)
        {
            bool occupied = false;
                if (p1.Row == powerUp.Row &&p1.Column == powerUp.Column)
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
                    map[i,j] = new Map(i,j);
        }

        /// <summary>
        /// Checks if the player can move
        /// </summary>
        /// <param name="player">Player's position</param>
        /// <returns>Returns true if the player is stuck, otherwise 
        /// false</returns>
        private bool NoRemainingMoves(Player player)
        {
            int  count = 0;
            bool lose  = false;
            // Raises count if the player can't move
            try
            {   // Checks north
                if (map[player.Row - 1, player.Column].Walkable == false) 
                    count++;               
            } catch {count++;};
            try
            {   // Checks south
                if (map[player.Row + 1, player.Column].Walkable == false) 
                    count++;               
            } catch {count++;};
            try
            {   // Checks east
                if (map[player.Row, player.Column + 1].Walkable == false) 
                    count++;               
            } catch {count++;};
            try
            {   // Checks Column
                if (map[player.Row, player.Column - 1].Walkable == false) 
                count++;               
            } catch {count++;};
            // If count == 4, it's gameover
            if (count == 4) lose = true;
            return lose;
        
        }

        /// <summary>
        /// Goes to the next level after reaching the exit
        /// </summary>
        /// <param name="level">Gets level number</param>
        /// <param name="print">Gets Renderer class to print</param>
        private void LevelUp(Level level, Renderer print)
        {
            //Adds 1 to the level number
            level.LevelNum++;

            //Prints a message to the screen once player exits.
            print.GetGameActions();

            //Redraws the game's map and Sets new positions for
            // the player and exit
            save.GetSave(level.LevelNum, "continue");
            CreateMap();
            // Prints "loading" bar
            level.CreateLevel(map);
            for (int i = 0; i < 58; i++)
            {
                Thread.Sleep(25);
                print.Dot();
            }
        }

        /// <summary>
        /// Quits the game loop
        /// </summary>
        private void Quit() => gameOver = true;
    }
}