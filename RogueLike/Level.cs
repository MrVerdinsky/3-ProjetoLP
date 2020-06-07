using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike
{
    /// <summary>
    /// Creates levels and its elements
    /// </summary>
    sealed internal class Level
    {
        private int EnemyNum        { get; set; }
        private int PowerUpNum      { get; set; }
        internal int LevelNum         { get; set; }
        private int AvailbleArea    { get; set; }
        private int ObstacleNum     { get; set; }
        internal Enemy[] Enemies      { get; set; }
        private Random random;
        internal PowerUp[] PowerUps   { get; set; }
        internal Player player        { get; set; }
        internal int EnemyMoveNum     { get; private set; }
 
        /// <summary>
        /// Creates Level
        /// </summary>
        internal Level()
        {
            LevelNum        = 0;
            EnemyNum        = 0;
            ObstacleNum     = 0;
            AvailbleArea    = Game.rows * Game.columns;
            random          = new Random(Game.Seed);
        }

        /// <summary>
        /// Gets all level paramaters
        /// </summary>
        /// <param name="map">Current level map</param>
        internal void CreateLevel(Map[,] map, int LevelNum)
        {
            // Sets random enemy move integer
            SetEnemyMoveNum();
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
        private void ResetAvailableArea() => 
            AvailbleArea = Game.rows * Game.columns;

        /// <summary>
        /// Gets a Random number of Power-Ups
        /// </summary>
        /// <param name="LevelNum">Level's Number</param>
        private void GetPowerUpNum(int LevelNum)
        {
            // Temporary number of power ups
            int tempPowerUpNum = 0;
            // Max number of power ups, based on available area
            int maxPUNum = AvailbleArea/2;
 
            // Loop that runs while the Random generated number of power ups
            // is grater then the maximum amount of power ups allowed
            // or if it is equal or smaller then 0
            // In every loop iteration, it will aks for a Random number of 
            // power ups
            while (tempPowerUpNum >= maxPUNum || tempPowerUpNum <= 0)
            {   
                // resets temporary number of power ups
                tempPowerUpNum = 0;
                tempPowerUpNum = Logistic(LevelNum, maxPUNum, true);
            }
            // Sets the number of power ups to the random generated value stored
            // on the temporary value
            PowerUpNum = tempPowerUpNum;

            // Decrements the availble area based on the number of power ups
            AvailbleArea -= PowerUpNum;
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
            // Loop that runs while the Random generated number of Enemies
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
                PowerUps[i] = new PowerUp(1, 1, 4);
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
                    int randRow     = random.Next(Game.rows);

                    // Random column
                    int randColumn  = random.Next(Game.columns);

                    PowerUps[i]= new PowerUp(randRow, randColumn , powerUpHeal); 

                        
                    for (int j = 0; j < i; j++)
                    {
                        // Checks if the randomized position is occupied and 
                        //it is different from another power ups positions
                        if ((PowerUps[i].Equals(PowerUps[j])) ||
                            !(map[PowerUps[i].Row, PowerUps[i].Column].Empty))
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
                        PowerUps[i].Row, 
                        PowerUps[i].Column].
                        Empty))
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
            // Goes through PowerUps list and occupies the map positions
            // with them
            foreach (PowerUp powerUp in PowerUps)
            {
                map[powerUp.Row, powerUp.Column].Occupy("power_up");
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
                int randRow     = random.Next(Game.rows);
                // Random column
                int randColumn  = random.Next(Game.columns);

                // Checks if the randomized map position is empty
                if (map[randRow,randColumn].Empty)
                {
                    // Occupied the map position with an obstacle
                   map[randRow,randColumn].Occupy("wall"); 
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
                Enemies[i] = new Enemy(1, 1, 5);
            }

            // Randomize all Enemies positions
            if (!(Enemies == null) || !(Enemies.Length == 0))
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    // Variable to check if it is suppose to "roll" the 
                    // positions again
                    bool reroll = false;

                    // Random row
                    int randRow         = random.Next(Game.rows);
                    // Random column
                    int randColumn      = random.Next(Game.columns);
                    // Random damage
                    int randomDamage    = GetEnemyType();

                    Enemies[i]          = new Enemy(randRow, randColumn,
                         randomDamage);

                        
                    for (int j = 0; j < i; j++)
                    {
                        // Checks if the randomized position is occupied and 
                        //it is different from another Enemies positions
                        if ( Enemies[i].Equals(Enemies[j]) ||
                            (!(map[Enemies[i].Row, Enemies[i].Column].Empty)))
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
                    if (!(map[Enemies[i].Row,Enemies[i].Column].Empty))
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
                map[enemy.Row, enemy.Column].
                Occupy("enemy");
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

        /// <summary>
        /// Gets a weightned random choice of enemy type
        /// </summary>
        /// <returns></returns>
        private int GetEnemyType()
        {
            // Enemy index
            int index;

            // array of enemy type's damage
            int[] enemyDamage = new int[2]{5, 10};

            // Stablishes 85% chance of getting a minion and 15% chance of 
            // getting a boss
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
            int randRow = random.Next(Game.rows);
            
            //Continues Randomizing until an empty square is found
            while (!(map[randRow, Game.columns-1].Empty))
            {
                randRow = random.Next(Game.rows);
            }

            //Sets position
            map[randRow,Game.columns-1].Occupy("exit");
        }

        /// <summary>
        /// Sets random Player position
        /// </summary>
        /// <param name="map">Current level map</param>
        private void GetPlayerPos(Map[,] map)
        {
            int randRow = random.Next(Game.rows);
            player = new Player(randRow, 0);

            //Continues Randomizing until an empty square is found
            while(!(map[randRow,0].Empty))
            {
                randRow = random.Next(Game.rows);
                player = new Player(randRow, 0);
            }

            //Sets position
            map[randRow,0].Occupy("player");
        }

        /// <summary>
        /// Logistic mathematical Function
        /// </summary>
        /// <param name="x">Gets an integer</param>
        /// <param name="max">Function curve maximum value</param>
        /// <param name="descending">Defines if value are descending</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets and random integer for enemies movement
        /// </summary>
        internal void SetEnemyMoveNum() => EnemyMoveNum = random.Next(0, 2);
    }
}