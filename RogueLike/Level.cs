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
        public Enemy[] enemies{get; set;}
        private Random random = new Random();

        public Level(int firstRowNum, int firstColumnNum)
        {
            RowNum      = firstRowNum;
            ColumnNum   = firstColumnNum;
            LevelNum    = 0;
            EnemyNum    = 0;
        }
        // public Level(){}

        /// <summary>
        /// Gets all level paramaters
        /// </summary>
        /// <param name="map"></param>
        public void CreateLevel(Map[,] map)
        {
            Map[,] auxMap;
            auxMap = map;
            GetEnemyNum();
            GetEnemyPos(auxMap);
        }

        /// <summary>
        /// Gets a random number of enemies
        /// </summary>
        private void GetEnemyNum()
        {
            int auxNum = 0;
            int maxEnemyNum = (RowNum * ColumnNum)/2;
            while (auxNum >= maxEnemyNum || auxNum <= 0)
            {
                auxNum = 0;
                auxNum = Log(LevelNum);

            }
            EnemyNum = auxNum;
        }
        /// <summary>
        /// Gets enemies random positions
        /// </summary>
        /// <param name="map">Map variable</param>
        private void GetEnemyPos(Map[,] map)
        {
            // foreach (Map point in map)
            // {
            //     Console.WriteLine($"Empty  [{point.Position.Row}, {point.Position.Column}]: {point.Position.Empty}");   
            // }
            
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
                    bool reroll = false;
                    int randRow     = random.Next(RowNum);
                    int randColumn  = random.Next(ColumnNum);

                    enemies[i].Position = new Position(randRow, randColumn); 

                        
                    for (int j = 0; j < i; j++)
                    {
                        // Console.WriteLine($"pos({i}): [{enemies[i].Position.Row}, {enemies[i].Position.Column}] - {map[enemies[i].Position.Row, enemies[i].Position.Column].Position.Empty}");
                        // Checks if the randomized position if free and it is 
                        // different from another enemies positions
                        if ((enemies[i].Position.Row == enemies[j].
                            Position.Row && 
                            enemies[i].Position.Column == enemies[j].
                            Position.Column) ||
                            (!(map[enemies[i].Position.Row, enemies[i].
                            Position.Column].Position.Empty)))
                        {
                            reroll = true;
                            i --;
                            break;
                        }
                    }

                    if (!(map[
                        enemies[i].Position.Row, 
                        enemies[i].Position.Column].
                        Position.Empty))
                    {
                        reroll = true;
                        i --;
                    }
                
                    if (reroll)
                        continue;
                  
                }
                // foreach (Enemy enemy in enemies)
                // {
                //     Console.WriteLine($"enemy pos: {enemy.Position.Row}, {enemy.Position.Column}");
                // }     
            }
        }

        private void GetPowerUp()
        {
            PowerUpNum = 2;
        }
        private int Log(int x)
        {
            int a;
            a = random.Next((RowNum * ColumnNum)/2);
            return (int)( a * Math.Log(1.2 * (x + 1)) + 1);
        }
    }
}