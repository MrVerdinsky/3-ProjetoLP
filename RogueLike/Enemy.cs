using System;
namespace RogueLike
{
    /// <summary>
    /// Enemy class, created from Character class
    /// </summary>
    public class Enemy : Character
    {
        internal int damage { get; private set; }
        int Movements = 1;

        /// <summary>
        /// Creates an Enemy
        /// </summary>
        /// <param name="position">Sets the enemy position</param>
        /// <param name="damage">Sets the enemy damage</param>
        public Enemy (Position position, int damage)
        {
            base.Position           = position;
            this.damage             = damage;
        }

        public void Move(Player player, int random, Map[,] map, 
                        int rows, int columns)
        {
            int p1R = player.Position.Row;
            int p1C = player.Position.Column;
            int eR = this.Position.Row;
            int eC = this.Position.Column;

            int distanceX = (p1C - eC);
            int distanceY = (p1R - eR);

            if (Movements > 0)
            {   
                Movements -= 1;
                for (int i = 0; i < rows; i ++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (random == 1)
                        {
                            if (distanceX < 0 && j > 0 && i > 0)
                            {
                                if(map[i,j-1].Position.Walkable) 
                                    this.Position.Column -= 1;
                                else
                                {
                                    if(map[i+1,j].Position.Walkable) 
                                        this.Position.Row += 1;
                                    else
                                    {
                                        if(map[i-1,j].Position.Walkable) 
                                            this.Position.Row += 1;
                                        else
                                        {
                                            if(map[i,j+1].Position.Walkable) 
                                                this.Position.Column += 1;
                                            else 
                                            {
                                                this.Position.Row = 
                                                    this.Position.Row;
                                                this.Position.Column = 
                                                    this.Position.Column;
                                            }
                                        }
                                    }
                                }
                            }
                                                
                        }
                    }
                }
            }
        }
    }
}