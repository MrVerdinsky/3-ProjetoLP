using System;
namespace RogueLike
{
    /// <summary>
    /// Enemy class, created from Character class
    /// </summary>
    public class Enemy : Character
    {
        internal int damage { get; private set; }

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

            // TOP LEFT ////////////////////////////////////////////////////////
            if (distanceX < 0 && distanceY < 0) 
            {   // IF X < Y
                if (distanceX < distanceY)  
                {   // WALKS LEFT
                    if(map[Position.Row,Position.Column-1]. 
                        Position.Walkable) 
                        Position.Column -= 1;
                    else
                    {   // WALKS TOP
                        if(map[Position.Row-1,Position.Column].
                            Position.Walkable) 
                            Position.Row -= 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS BOTTOM
                                if(map[Position.Row+1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row += 1;
                                else
                                {   // WALKS RIGHT
                                    if(map[Position.Row,Position.Column+1].
                                        Position.Walkable) 
                                        Position.Column += 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS BOTTOM
                                    if(map[Position.Row,Position.Column+1].
                                        Position.Walkable) 
                                        Position.Column += 1; 
                                else
                                {   // WALKS RIGHT
                                    if(map[Position.Row+1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row += 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }
                        }         
                    }
                }
                else
                {   // WALKS TOP
                    if(map[Position.Row-1,Position.Column].
                        Position.Walkable) 
                        Position.Row -= 1;
                    else
                    {   // WALKS LEFT
                        if(map[Position.Row,Position.Column-1]. 
                            Position.Walkable) 
                            Position.Column -= 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS BOTTOM
                                if(map[Position.Row+1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row += 1;
                                else
                                {   // WALKS RIGHT
                                    if(map[Position.Row,Position.Column+1].
                                        Position.Walkable) 
                                        Position.Column += 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS RIGHT
                                    if(map[Position.Row,Position.Column+1].
                                        Position.Walkable) 
                                        Position.Column += 1; 
                                else
                                {   // WALKS BOTTOM
                                    if(map[Position.Row+1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row += 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }
                        }         
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////
                
            // TOP RIGHT ///////////////////////////////////////////////////////
            if (distanceX > 0 && distanceY < 0) 
            {   // IF X > Y
                if (distanceX > distanceY)  
                {   // WALKS RIGHT
                    if(map[Position.Row,Position.Column+1]. 
                        Position.Walkable) 
                        Position.Column += 1;
                    else
                    {   // WALKS TOP
                        if(map[Position.Row-1,Position.Column].
                            Position.Walkable) 
                            Position.Row -= 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS BOTTOM
                                if(map[Position.Row+1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row += 1;
                                else
                                {   // WALKS Left
                                    if(map[Position.Row,Position.Column-1].
                                        Position.Walkable) 
                                        Position.Column -= 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS Left
                                    if(map[Position.Row,Position.Column-1].
                                        Position.Walkable) 
                                        Position.Column -= 1; 
                                else
                                {   // WALKS BOTTOM
                                    if(map[Position.Row+1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row += 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }
                        }         
                    }
                }
                else
                {    
                    // WALKS TOP
                    if(map[Position.Row-1,Position.Column].
                        Position.Walkable) 
                        Position.Row -= 1;
                    else
                    {   // WALKS RIGHT
                        if(map[Position.Row,Position.Column+1]. 
                            Position.Walkable) 
                            Position.Column += 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS BOTTOM
                                if(map[Position.Row+1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row += 1;
                                else
                                {   // WALKS Left
                                    if(map[Position.Row,Position.Column-1].
                                        Position.Walkable) 
                                        Position.Column -= 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS Left
                                if(map[Position.Row,Position.Column-1].
                                    Position.Walkable) 
                                    Position.Column -= 1; 
                                else
                                {   // WALKS BOTTOM
                                    if(map[Position.Row+1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row += 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }
                        }         
                    }
                }
            }
                ////////////////////////////////////////////////////////////////

            // BOTTOM RIGHT ////////////////////////////////////////////////////
            if (distanceX > 0 && distanceY > 0) 
            {   // IF X > Y
                if (distanceX > distanceY)  
                {   // WALKS RIGHT
                    if(map[Position.Row,Position.Column+1]. 
                        Position.Walkable) 
                        Position.Column += 1;
                    else
                    {   // WALKS BOTTOM
                        if(map[Position.Row+1,Position.Column].
                            Position.Walkable) 
                            Position.Row += 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS TOP
                                if(map[Position.Row-1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row -= 1;
                                else
                                {   // WALKS LEFT
                                    if(map[Position.Row,Position.Column-1].
                                        Position.Walkable) 
                                        Position.Column -= 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS LEFT
                                if(map[Position.Row,Position.Column-1].
                                    Position.Walkable) 
                                    Position.Column -= 1; 
                                else
                                {   // WALKS TOP
                                    if(map[Position.Row-1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row -= 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }
                        }         
                    }
                }
                else
                {   // WALKS BOTTOM
                    if(map[Position.Row+1,Position.Column].
                        Position.Walkable) 
                        Position.Row += 1;
                    else
                    {   // WALKS RIGHT
                        if(map[Position.Row,Position.Column+1]. 
                            Position.Walkable) 
                            Position.Column += 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS TOP
                                if(map[Position.Row-1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row -= 1;
                                else
                                {   // WALKS LEFT
                                    if(map[Position.Row,Position.Column-1].
                                        Position.Walkable) 
                                        Position.Column -= 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS LEFT
                                if(map[Position.Row,Position.Column-1].
                                    Position.Walkable) 
                                    Position.Column -= 1; 
                                else
                                {   // WALKS TOP
                                    if(map[Position.Row-1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row -= 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }
                        }         
                    }
                }
            } 
            ////////////////////////////////////////////////////////////////////
            
            // BOTTOM LEFT /////////////////////////////////////////////////////
            if (distanceX > 0 && distanceY > 0) 
            {   // IF X < Y
                if (distanceX < distanceY)  
                {   // WALKS LEFT
                    if(map[Position.Row,Position.Column-1]. 
                        Position.Walkable) 
                        Position.Column -= 1;
                    else
                    {   // WALKS BOTTOM
                        if(map[Position.Row+1,Position.Column].
                            Position.Walkable) 
                            Position.Row += 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS TOP
                                if(map[Position.Row-1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row -= 1;
                                else
                                {   // WALKS RIGHT
                                    if(map[Position.Row,Position.Column+1].
                                        Position.Walkable) 
                                        Position.Column += 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS RIGHT
                                if(map[Position.Row,Position.Column+1].
                                    Position.Walkable) 
                                    Position.Column += 1; 
                                else
                                {   // WALKS TOP
                                    if(map[Position.Row-1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row -= 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }
                        }         
                    }
                }  
                else
                {   // WALKS BOTTOM
                    if(map[Position.Row+1,Position.Column].
                        Position.Walkable) 
                        Position.Row += 1;
                    else
                    {   // WALKS LEFT
                        if(map[Position.Row,Position.Column-1]. 
                            Position.Walkable) 
                            Position.Column -= 1;
                        else
                        {   // IF RANDOM 1
                            if (random > 0)
                            {   // WALKS TOP
                                if(map[Position.Row-1,Position.Column].
                                    Position.Walkable) 
                                    Position.Row -= 1;
                                else
                                {   // WALKS RIGHT
                                    if(map[Position.Row,Position.Column+1].
                                        Position.Walkable) 
                                        Position.Column += 1; 
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
                                    }
                                }
                            }   
                            // IF RANDOM 2
                            if (random == 0)
                            {   // WALKS RIGHT
                                if(map[Position.Row,Position.Column+1].
                                    Position.Walkable) 
                                    Position.Column += 1; 
                                else
                                {   // WALKS TOP
                                    if(map[Position.Row-1,Position.Column].
                                        Position.Walkable) 
                                        Position.Row -= 1;
                                    else 
                                    {
                                        Position.Row = Position.Row;
                                        Position.Column = Position.Column;
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