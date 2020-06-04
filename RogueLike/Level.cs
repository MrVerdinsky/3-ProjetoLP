using System;

namespace RogueLike
{
    public class Level
    {
        public int EnemyNum{get; set;}
        public int RowNum{get; set;}
        public int ColumnNum{get; set;}
        public int PowerUpNum{get; set;}
        public int LevelNum{get; set;}
        private int AvailableArea{get; set;}
        public int ObstacleNum{get; set;}
        public Enemy[] enemies{get; set;}
        private Random random = new Random();
        public PowerUp[] PowerUps{get; set;}

        public Level(int firstRowNum, int firstColumnNum)
        {
            RowNum          = firstRowNum;
            ColumnNum       = firstColumnNum;
            LevelNum        = 0;
            EnemyNum        = 0;
            ObstacleNum     = 0;
            AvailableArea   = RowNum * ColumnNum;
        }

        /// <summary>
        /// Gets all level paramaters
        /// </summary>
        /// <param name="map">Current level map</param>
        public void CreateLevel(Map[,] map)
        {
            // Gets random number of enemies
            GetEnemyNum();
            // Sets enemies to their positions
            GetEnemyPos(map);
            // Gets random number of obstacles
            GetObstacleNum();
            // Sets obstacles to their position
            GetObsPos(map);
            // Gets random number of power-ups
            GetPowerUpNum();
            // Sets obstacles to their power-ups
            GetPowerUpPos(map);
            
        }

        /// <summary>
        /// Gets a random number of power-ups
        /// </summary>
        private void GetPowerUpNum()
        {
            int tempPowerUpNum = 0;
            int maxPUNum = AvailableArea/2;
            while (tempPowerUpNum >= maxPUNum || tempPowerUpNum <= 0)
            {
                tempPowerUpNum = 0;
                tempPowerUpNum = Logistic(LevelNum, maxPUNum);

            }
            PowerUpNum = tempPowerUpNum;
        }

        /// <summary>
        /// Gets a random number of enemies
        /// </summary>
        private void GetEnemyNum()
        {
            // Temporary enemy number
            int tempEnemyNum = 0;

            // Max enemy number
            int maxEnemyNum =  AvailableArea/2;
            // Loop the runs while the random generated number of enemies
            // is grater then the maximum amount of enemies allowed
            // or if it is equal or smaller then 0
            // In every loop iteration, it will aks for a random number of 
            // enemies
            while (tempEnemyNum >= maxEnemyNum || tempEnemyNum <= 0)
            {
                tempEnemyNum = 0;
                tempEnemyNum = Log(LevelNum);

            }
            EnemyNum = tempEnemyNum;
            AvailableArea -= EnemyNum;
        }

        /// <summary>
        /// Get a random number os obstacles
        /// </summary>
        private void GetObstacleNum()
        {
            // temporary obstacle number
            int tempObsNum = 0;

            // Max obstacle number
            int maxObsNum = AvailableArea/2 - 1;

            // Loop the runs while the random generated number of obstacles
            // is grater then the maximum amount of obstacles allowed
            // or if it is equal or smaller then 0
            // In every loop iteration, it will aks for a random number of 
            // obstacles
            while (tempObsNum >= maxObsNum || tempObsNum <= 0)
            {
                tempObsNum = 0;
                tempObsNum = Log(LevelNum);

            }
            ObstacleNum = tempObsNum;

        }
        private void GetPowerUpPos(Map[,] map)
        {            
            PowerUps = new PowerUp[PowerUpNum];
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
                    // Variable to check if it is suppose to "roll" the 
                    // positions again
                    bool reroll = false;

                    // Random row
                    int randRow     = random.Next(RowNum);

                    // Random column
                    int randColumn  = random.Next(ColumnNum);

                    PowerUps[i]= new PowerUp(new Position(randRow, randColumn), 4); 

                        
                    for (int j = 0; j < i; j++)
                    {
                        // Checks if the randomized position is occupied and 
                        //it is different from another power ups positions
                        if ((PowerUps[i].Position.Row == PowerUps[j].
                            Position.Row && 
                            PowerUps[i].Position.Column == PowerUps[j].
                            Position.Column) ||
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

            // Goes throes the whole PowerUps list and occupies the map positions
            // with them
            foreach (PowerUp powerUp in PowerUps)
            {
                map[powerUp.Position.Row, powerUp.Position.Column].Position.PowerUpOccupy();
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
        /// Gets enemies random positions
        /// </summary>
        /// <param name="map">Current level map</param>
        private void GetEnemyPos(Map[,] map)
        {            
            enemies = new Enemy[EnemyNum];
            
            // Gives a temporary position to each enemy
            for (int i = 0; i < EnemyNum; i++)
            {
                enemies[i] = new Enemy(new Position(1,1), 5);
            }

            // Randomize all enemies positions
            if (!(enemies == null) || !(enemies.Length == 0))
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    // Variable to check if it is suppose to "roll" the 
                    // positions again
                    bool reroll = false;

                    // Random row
                    int randRow     = random.Next(RowNum);

                    // Random column
                    int randColumn  = random.Next(ColumnNum);

                    enemies[i].Position = new Position(randRow, randColumn); 

                        
                    for (int j = 0; j < i; j++)
                    {
                        // Checks if the randomized position is occupied and 
                        //it is different from another enemies positions
                        if ((enemies[i].Position.Row == enemies[j].
                            Position.Row && 
                            enemies[i].Position.Column == enemies[j].
                            Position.Column) ||
                            (!(map[enemies[i].Position.Row, enemies[i].
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
                        enemies[i].Position.Row, 
                        enemies[i].Position.Column].
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

            // Goes throes the whole enemies list and occupies the map positions
            // with them
            foreach (Enemy enemy in enemies)
            {
                map[enemy.Position.Row, enemy.Position.Column].Position.EnemyOccupy();
            }
        }

       
        private int Log(int x)
        {
            int a;
            a = random.Next((RowNum * ColumnNum)/2);
            return (int)( a * Math.Log(1.2 * (x + 1)) + 1);
        }


        private int Logistic(int x, int max)
        {
            x++;
            int x0  = 28;
            float k = 0.14f;
            float L = random.Next(max);
            int min = 1;   
            return (int)(((-L)/ (1 + Math.Pow(Math.E, (-k * (x - x0))))) + L + min);
        }
    }
}