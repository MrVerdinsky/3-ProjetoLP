using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike
{
    /// <summary>
    /// Creates levels and its elements
    /// </summary>
    sealed public class Level
    {
        private int EnemyNum        { get; set; }
        private int RowNum          { get; set; }
        private int ColumnNum       { get; set; }
        private int PowerUpNum      { get; set; }
        public int LevelNum         { get; set; }
        private int AvailbleArea    { get; set; }
        private int ObstacleNum     { get; set; }
        public Enemy[] Enemies      { get; set; }
        private Random random;
        public PowerUp[] PowerUps   { get; set; }
        public Player player        { get; set; }
        public int Seed             { get; private set; }

        /// <summary>
        /// Creates Level
        /// </summary>

        /// <param name="firstColumnNum">totals of Columns</param>
        /// <param name="seed">Current Game's seed</param>
        public Level(long seed)
        {
            RowNum          = Game.rows;
            ColumnNum       = Game.columns;
            Seed            = (int)seed;
            LevelNum        = 0;
            EnemyNum        = 0;
            ObstacleNum     = 0;
            AvailbleArea    = RowNum * ColumnNum;
            random          = new Random((int)(Seed));
        }

        /// <summary>
        /// Gets all level paramaters
        /// </summary>
        /// <param name="map">Current level map</param>
        public void CreateLevel(Map[,] map, int LevelNum)
        {
            // Rests availble area
            ResetAvailableArea();
            // Sets Random Exit position
            GetExitPos(map);
            // Sets Random player position
            GetPlayerPos(map);
            // Gets Random number of Enemies
            GetEnemyNum(LevelNum);
            // Sets Enemies to their positions
            GetEnemyPos(map);
            // Gets Random number of obstacles
            GetObstacleNum(LevelNum);
            // Sets obstacles to their position
            GetObsPos(map);
            // Gets Random number of power-ups
            GetPowerUpNum(LevelNum);
            // Sets obstacles to their power-ups
            GetPowerUpPos(map);            
        }

        /// <summary>
        /// Resets the area availble to create new level elements 
        /// </summary>
        private void ResetAvailableArea() => AvailbleArea = RowNum * ColumnNum;

        /// <summary>
        /// Gets a Random number of power-ups
        /// </summary>
        /// <param name="LevelNum">Level's Number</param>
        private void GetPowerUpNum(int LevelNum)
        {
            int tempPowerUpNum = 0;
            int maxPUNum = AvailbleArea/2;
            while (tempPowerUpNum >= maxPUNum || tempPowerUpNum <= 0)
            {
                tempPowerUpNum = 0;
                tempPowerUpNum = Logistic(LevelNum, maxPUNum, true);

            }
            PowerUpNum = tempPowerUpNum;
            AvailbleArea -= maxPUNum;
        }

        /// <summary>
        /// Gets a Random number of Enemies
        /// </summary>
        /// <param name="LevelNum">Level's Number</param>
        private void GetEnemyNum(int LevelNum)
        {
            
            // Temporary enemy number
            int tempEnemyNum = 0;

            // Max enemy number
            int maxEnemyNum =  AvailbleArea/2;
            // Loop the runs while the Random generated number of Enemies
            // is grater then the maximum amount of Enemies allowed
            // or if it is equal or smaller then 0
            // In every loop iteration, it will aks for a Random number of 
            // Enemies
            while (tempEnemyNum >= maxEnemyNum || tempEnemyNum <= 0)
            {
                tempEnemyNum = 0;
                tempEnemyNum = Logistic(LevelNum, maxEnemyNum);
            }
            EnemyNum = tempEnemyNum;
            AvailbleArea -= EnemyNum;
        }

        /// <summary>
        /// Get a Random number os obstacles
        /// </summary>
        /// <param name="LevelNum">Level's Number</param>
        private void GetObstacleNum(int LevelNum)
        {

            
            // temporary obstacle number
            int tempObsNum = 0;

            // Max obstacle number
            int maxObsNum = AvailbleArea/2 - 1;

            // Loop the runs while the Random generated number of obstacles
            // is grater then the maximum amount of obstacles allowed
            // or if it is equal or smaller then 0
            // In every loop iteration, it will aks for a Random number of 
            // obstacles
            while (tempObsNum >= maxObsNum || tempObsNum <= 0)
            {
                tempObsNum = 0;
                tempObsNum = Logistic(LevelNum, maxObsNum);

            }
            ObstacleNum = tempObsNum;
            AvailbleArea -= maxObsNum;

        }

        /// <summary>
        /// Positions each power up on the map
        /// </summary>
        /// <param name="map">Current level map</param>
        private void GetPowerUpPos(Map[,] map)
        {            
            PowerUps = new PowerUp[PowerUpNum];

            int powerUpHeal;
            // Gives a temporary position to each enemy
            for (int i = 0; i < PowerUpNum; i++)
            {
                PowerUps[i] = new PowerUp(new Position(1,1), 4);
            }
            // Randomize all power ups positions
            if (!(PowerUps == null) || !(PowerUps.Length == 0))
            {
                for (int i = 0; i < PowerUps.Length; i++)
                {
                    powerUpHeal = GetPowerUpType(PowerUpNum);
                    // Variable to check if it is suppose to "roll" the 
                    // positions again
                    bool reroll = false;

                    // Random row
                    int randRow     = random.Next(RowNum);

                    // Random column
                    int randColumn  = random.Next(ColumnNum);

                    PowerUps[i]= new PowerUp(
                        new Position(randRow, randColumn), powerUpHeal); 

                        
                    for (int j = 0; j < i; j++)
                    {
                        // Checks if the randomized position is occupied and 
                        //it is different from another power ups positions
                        if ((PowerUps[i].Position.Equals(PowerUps[j].
                            Position)) ||
                            (!(map[PowerUps[i].Position.Row, PowerUps[i].
                            Position.Column].Position.Empty)))
                        {
                            // "Reroll" of the positions is necessary
                            reroll = true;
                            // Decrements i to stay on the same i iteration
                            i --;
                            break;
                        }
                    }

                    // This verification is for cases when the power up list has 
                    // only one power up
                    //Check is the randomized position is occupied
                    if (!(map[
                        PowerUps[i].Position.Row, 
                        PowerUps[i].Position.Column].
                        Position.Empty))
                    {
                        // "Reroll" of the positions is necessary
                        reroll = true;
                        // Decrements i to stay on the same i iteration
                        i --;
                    }

                    // In case a "reroll" is necessary, it goes to the top
                    // of the loop and repeats the randomizing process
                    if (reroll)
                        continue;
                }    
            }
            string[]  testear = new string[2];
            // Goes throes the whole PowerUps list and occupies the map positions
            // with them
            foreach (PowerUp powerUp in PowerUps)
            {
                map[powerUp.Position.Row, powerUp.Position.Column].Position.
                PowerUpOccupy();
            }
        }

        /// <summary>
        /// Positions each obstacle on the map
        /// </summary>
        /// <param name="map">Current level map</param>
        private void GetObsPos(Map[,] map)
        {
            for(int i = 0; i < ObstacleNum; i++)
            {
                // Random row
                int randRow     = random.Next(RowNum);
                // Random column
                int randColumn  = random.Next(ColumnNum);

                // Checks if the randomized map position is empty
                if (map[randRow,randColumn].Position.Empty)
                {
                    // Occupied the map position with an obstacle
                   map[randRow,randColumn].Position.WallOccupy(); 
                }
                // In case the position is occupied
                else
                {  
                     // Decrements i to stay on the same i iteration
                    i --;
                    // It goes to the top of the loop and repeats the
                    // randomizing process
                    continue;
                }
            }
        }

        /// <summary>
        /// Gets Enemies Random positions
        /// </summary>
        /// <param name="map">Current level map</param>
        private void GetEnemyPos(Map[,] map)
        {            
            Enemies = new Enemy[EnemyNum];
            
            // Gives a temporary position to each enemy
            for (int i = 0; i < EnemyNum; i++)
            {
                Enemies[i] = new Enemy(new Position(1,1), 5, "");
            }

            // Randomize all Enemies positions
            if (!(Enemies == null) || !(Enemies.Length == 0))
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    // Variable to check if it is suppose to "roll" the 
                    // positions again
                    bool reroll = false;
                    string symbol = "|Na |";
                    // Random row
                    int randRow         = random.Next(RowNum);
                    // Random column
                    int randColumn      = random.Next(ColumnNum);
                    // Random damage
                    int randomDamage    = GetEnemyType(EnemyNum);
                    if (randomDamage == 5)
                        symbol          = "|\u265F |";

                    else if (randomDamage == 10)
                        symbol          = "|\u265A |";

                    Enemies[i]          = new Enemy(
                        new Position(randRow, randColumn), randomDamage, symbol);

                        
                    for (int j = 0; j < i; j++)
                    {
                        // Checks if the randomized position is occupied and 
                        //it is different from another Enemies positions
                        if ((Enemies[i].Position.Equals(Enemies[j].
                            Position)) ||
                            (!(map[Enemies[i].Position.Row, Enemies[i].
                            Position.Column].Position.Empty)))
                        {
                            // "Reroll" of the positions is necessary
                            reroll = true;
                            // Decrements i to stay on the same i iteration
                            i --;
                            break;
                        }
                    }

                    // This verification is for cases when the enemy list has 
                    // only one enemy
                    //Check is the randomized position is occupied
                    if (!(map[
                        Enemies[i].Position.Row, 
                        Enemies[i].Position.Column].
                        Position.Empty))
                    {
                        // "Reroll" of the positions is necessary
                        reroll = true;
                        // Decrements i to stay on the same i iteration
                        i --;
                    }

                    // In case a "reroll" is necessary, it goes to the top
                    // of the loop and repeats the randomizing process
                    if (reroll)
                        continue;
                }    
            }

            // Goes throes the whole Enemies list and occupies the map positions
            // with them
            foreach (Enemy enemy in Enemies)
            {
                map[enemy.Position.Row, enemy.Position.Column].Position.
                EnemyOccupy();
            }
        }

        /// <summary>
        /// Gets Random amount of power up types 
        /// </summary>
        /// <param name="powerUpNum">Number of power ups</param>
        private int GetPowerUpType(int powerUpNum)
        {
            // Index of the power up
            int index;
            // Array with all power-up types
            int[] healValue = new int[3]{4, 8, 16};
            // Array with the weight of each type for randomizing
            List <float> weights = new List<float>(){50, 35, 15}; 

            // Asks for weightned Random index
            index = RandomWeight(weights);

            return healValue[index];

        }

        private int GetEnemyType(int enemyNum)
        {
            // Enemy index
            int index;

            // array of enemy type's damage
            int[] enemyDamage = new int[2]{5, 10};
            List <float> weights = new List<float>(){85, 15}; 

            // Asks for weightned Random index
            index = RandomWeight(weights);

            return enemyDamage[index];
        }

        /// <summary>
        /// Sets random exit position
        /// </summary>
        /// <param name="map">Current level map</param>
        private void GetExitPos(Map[,] map)
        {   
            //Creates a random number based on the row's total
            int randRow = random.Next(RowNum);
            
            //Continues Randomizing until an empty square is found
            while (!(map[randRow, ColumnNum-1].Position.Empty))
            {
                randRow = random.Next(RowNum);
            }

            //Sets position
            map[randRow,ColumnNum-1].Position.ExitOccupy();
        }

        /// <summary>
        /// Sets random Player position
        /// </summary>
        /// <param name="map">Current level map</param>
        private void GetPlayerPos(Map[,] map)
        {
            int randRow = random.Next(RowNum);
            player = new Player(new Position(randRow, 0));

            //Continues Randomizing until an empty square is found
            while(!(map[randRow,0].Position.Empty))
            {
                randRow = random.Next(RowNum);
                player = new Player(new Position(randRow, 0));
            }

            //Sets position
            map[randRow,0].Position.PlayerOccupy();
        }
        // private int Log(int x)
        // {
        //     int a;
        //     a = random.Next((AvailbleArea)/2);
        //     return (int)( a * Math.Log(1.2 * (x + 1)) + 1);
        // }
        private int Logistic(int x, int max, bool descending = false)
        {
            int L;
            float k ;
            L = random.Next((max)/2);
            float x0 = 5f;
            if (descending)
                 k = -0.6f; 
            else
                 k  = 0.6f; 
            return (int)((L)/(1 + Math.Pow(Math.E, -k * (x - x0)))+1);
        }

        // private int Logistic(int x, int max)
        // {
        //     x++;
        //     float k = 0.14f;
        //     float L = random.Next(max);
        //     float x0  = L * 2;
        //     int min = 1;   
        //     return (int)(((-L)/ (1 + Math.Pow(Math.E, (-k * (x - x0))))) + L + min);
        // }
        
        /// <summary>
        /// Gets weighted Random index from a weight list
        /// </summary>
        /// <param name="weights">List of weights</param>
        /// <returns>Returns the probability value for a specific item on the 
        /// list</returns>
        private int RandomWeight(List <float> weights)
        {
            float rnd = (float)(random.NextDouble() * weights.Sum());
            int randomNum = 0;
            foreach (float item in weights)
            {
                    rnd = rnd - item;
                    if (rnd < 0)
                    {
                        randomNum = weights.IndexOf(item) ;
                        break;
                    }
            }
            return randomNum;
        }

        /*    PDOE IR FORA <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        /// <summary>
        /// Used to reset players and exits tags for the start of a new level
        /// </summary>
        /// <param name="map">All game position</param>
        /// <param name="level">Exit position</param>
        public void EscapeLevel(Map[,] map)
        {
            map[player.Position.Row,
                player.Position.Column].Position.PlayerFree(); 
            map[player.Position.Row,
                player.Position.Column].Position.ExitFree();
            map[player.Position.Row,
                player.Position.Column].Position.PlayerOccupy(); 
        }
        */
    }
}