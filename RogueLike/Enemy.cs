using System;
namespace RogueLike
{
    /// <summary>
    /// Enemy class, created from ObjectPosition class
    /// </summary>
    public class Enemy : ObjectPosition
    {
        internal int Damage     { get; private set; }
        internal string Symbol  { get; private set; }

        /// <summary>
        /// Creates an Enemy
        /// </summary>
        /// <param name="position">Sets the enemy position</param>
        /// <param name="damage">Sets the enemy damage</param>
        public Enemy (Position position, int damage, string symbol)
        {
            base.Position   = position;
            Damage          = damage;
            Symbol          = symbol;
        }
        
        /// <summary>
        /// Compares the enemy's position with the player position.
        /// Checks the closest path beetween both of them.
        /// Moves the enemy randomly to the nearest walkable position
        /// </summary>
        /// <param name="player">Player position</param>
        /// <param name="random">Random umber to move the enemy</param>
        /// <param name="map">Map positions</param>
        public void Move(Player player, int random, Map[,] map)
        {
            int p1R = player.Position.Row;
            int p1C = player.Position.Column;
            int eR  = this.Position.Row;
            int eC  = this.Position.Column;
            int distanceX = (p1C - eC);
            int distanceY = (p1R - eR);

            
                    // MAGIC HERE GABRIEL
            Random rand = new Random();

            int chance = rand.Next(2);   // 0 e 1
 
            int rMove;
            rMove = chance == 1 ? +1: -1;   // 1 ou -1  
                    //


            // One square range
            if (this.Position.Row == player.Position.Row +1 &&
                this.Position.Column == player.Position.Column ||
                this.Position.Row == player.Position.Row -1 &&
                this.Position.Column == player.Position.Column ||
                this.Position.Column == player.Position.Column -1 &&
                this.Position.Row == player.Position.Row ||
                this.Position.Column == player.Position.Column +1 &&
                this.Position.Row == player.Position.Row)
                { 
                }

            // Same column
            else if (distanceX == 0)
            {   // Move Up
                if (distanceY < 0)
                {   // Up
                    if (map[Position.Row-1,Position.Column].Position.Walkable
                    && !map[Position.Row-1,Position.Column].Position.HasExit)
                        this.Position.Row -= 1;
                    else
                    {
                        try
                        {   // Move left or right
                            if (map[Position.Row,Position.Column+rMove].
                                Position.Walkable
                            && !map[Position.Row,Position.Column+rMove].
                                Position.HasExit)
                                this.Position.Column += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Position.Row,Position.Column-rMove].
                                    Position.Walkable
                                && !map[Position.Row,Position.Column-rMove].
                                    Position.HasExit)
                                    this.Position.Column -= rMove;
                            }
                            catch
                            {
                                try
                                {   // Bottom
                                    if (map[Position.Row+1,Position.Column].
                                        Position.Walkable
                                    && !map[Position.Row+1,Position.Column].
                                        Position.HasExit)
                                        this.Position.Row += 1;
                                }
                                catch
                                {     
                                }
                            }
                        }    
                    }
                }
                // Move Down
                if (distanceY > 0) 
                {   // Down
                    if (map[Position.Row+1,Position.Column].Position.Walkable
                    && !map[Position.Row+1,Position.Column].Position.HasExit)
                        this.Position.Row += 1;
                    else
                    {
                        try
                        {   // Move left or right
                            if (map[Position.Row,Position.Column+rMove].
                                Position.Walkable
                            && !map[Position.Row,Position.Column+rMove].
                                Position.HasExit)
                                this.Position.Column += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Position.Row,Position.Column-rMove].
                                    Position.Walkable
                                && !map[Position.Row,Position.Column-rMove].
                                    Position.HasExit)
                                    this.Position.Column -= rMove;
                            }
                            catch
                            {
                                try
                                {   // Top
                                    if (map[Position.Row-1,Position.Column].
                                        Position.Walkable
                                    && !map[Position.Row-1,Position.Column].
                                        Position.HasExit)
                                        this.Position.Row -= 1;
                                }
                                catch
                                {     
                                }
                            }
                        }    
                    }
                }
            }

            // Same Row
            else if (distanceY == 0)
            {   // Move Left
                if (distanceX < 0)
                {   // Left
                    if (map[Position.Row,Position.Column-1].Position.Walkable
                    && !map[Position.Row,Position.Column-1].Position.HasExit)
                        this.Position.Column -= 1;
                    else
                    {
                        try
                        {   // Move top or down
                            if (map[Position.Row+rMove,Position.Column].
                                Position.Walkable
                            && !map[Position.Row+rMove,Position.Column].
                                Position.HasExit)
                                this.Position.Row += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Position.Row-rMove,Position.Column].
                                    Position.Walkable
                                && !map[Position.Row-rMove,Position.Column].
                                    Position.HasExit)
                                    this.Position.Row -= rMove;
                            }
                            catch
                            {
                                try
                                {
                                    if (map[Position.Row,Position.Column+1].
                                        Position.Walkable
                                    && !map[Position.Row,Position.Column+1].
                                        Position.HasExit)
                                        this.Position.Column += 1;
                                }
                                catch
                                {     
                                }
                            }
                        }    
                    }
                }
                // Move Right
                if (distanceX > 0) 
                {   // Right
                    if (map[Position.Row,Position.Column+1].Position.Walkable
                    && !map[Position.Row,Position.Column+1].Position.HasExit)
                        this.Position.Column += 1;
                    else
                    {
                        try
                        {   // Move left or right
                            if (map[Position.Row+rMove,Position.Column].
                                Position.Walkable
                            && !map[Position.Row+rMove,Position.Column].
                                Position.HasExit)
                                this.Position.Row += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Position.Row-rMove,Position.Column].
                                    Position.Walkable
                                && !map[Position.Row-rMove,Position.Column].
                                    Position.HasExit)
                                    this.Position.Row -= rMove;
                            }
                            catch
                            {
                                try
                                {
                                    if (map[Position.Row,Position.Column-1].
                                        Position.Walkable
                                    && !map[Position.Row,Position.Column-1].
                                        Position.HasExit)
                                        this.Position.Column -= 1;
                                }
                                catch
                                {     
                                }
                            }
                        }    
                    }
                }
            }

            // Left
            else if (distanceX < 0)
            {   // Top Left
                if (distanceY < 0)
                {
                    if (chance == 0)
                    {   // Up
                        if (map[Position.Row-1,Position.Column].
                            Position.Walkable
                        && !map[Position.Row-1,Position.Column].
                            Position.HasExit)
                            this.Position.Row -= 1; 
                        else
                        {   // Left
                            if (map[Position.Row,Position.Column-1].
                                Position.Walkable
                            && !map[Position.Row,Position.Column-1].
                                Position.HasExit)
                                this.Position.Column -= 1; 
                            else
                            {
                                try
                                {   // Right
                                    if (map[Position.Row,Position.Column+1].
                                        Position.Walkable
                                    && !map[Position.Row,Position.Column+1].
                                        Position.HasExit)
                                        this.Position.Column += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Bottom
                                        if (map[Position.Row+1,Position.Column].
                                            Position.Walkable
                                        && !map[Position.Row+1,Position.Column].
                                            Position.HasExit)
                                            this.Position.Row += 1;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    if (chance == 1)
                    {   // Left
                        if (map[Position.Row,Position.Column-1].
                            Position.Walkable
                        && !map[Position.Row,Position.Column-1].
                            Position.HasExit)
                            this.Position.Column -= 1; 
                        else
                        {
                            // Up
                            if (map[Position.Row-1,Position.Column].
                                Position.Walkable
                            && !map[Position.Row-1,Position.Column].
                                Position.HasExit)
                                this.Position.Row -= 1; 
                            else
                            {
                                try
                                {   // Bottom
                                    if (map[Position.Row+1,Position.Column].
                                        Position.Walkable
                                    && !map[Position.Row+1,Position.Column].
                                        Position.HasExit)
                                        this.Position.Row += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Right
                                        if (map[Position.Row,Position.Column+1].
                                            Position.Walkable
                                        && !map[Position.Row,Position.Column+1].
                                            Position.HasExit)
                                            this.Position.Column += 1;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }

                // Bottom Left
                if (distanceY > 0)
                {
                    if (chance == 0)
                    {   // Down
                        if (map[Position.Row+1,Position.Column].
                            Position.Walkable
                        && !map[Position.Row+1,Position.Column].
                            Position.HasExit)
                            this.Position.Row += 1; 
                        else
                        {   // Left
                            if (map[Position.Row,Position.Column-1].
                                Position.Walkable
                            && !map[Position.Row,Position.Column-1].
                                Position.HasExit)
                                this.Position.Column -= 1; 
                            else
                            {
                                try
                                {   // Right
                                    if (map[Position.Row,Position.Column+1].
                                        Position.Walkable
                                    && !map[Position.Row,Position.Column+1].
                                        Position.HasExit)
                                        this.Position.Column += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Up
                                        if (map[Position.Row-1,Position.Column].
                                            Position.Walkable
                                        && !map[Position.Row-1,Position.Column].
                                            Position.HasExit)
                                            this.Position.Column -= 1;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    if (chance == 1)
                    {   // Left
                        if (map[Position.Row,Position.Column-1].
                            Position.Walkable
                        && !map[Position.Row,Position.Column-1].
                            Position.HasExit)
                            this.Position.Column -= 1; 
                        else
                        {
                            // Down
                            if (map[Position.Row+1,Position.Column].
                                Position.Walkable
                            && !map[Position.Row+1,Position.Column].
                                Position.HasExit)
                                this.Position.Row += 1; 
                            else
                            {
                                try
                                {   // Up
                                    if (map[Position.Row-1,Position.Column].
                                        Position.Walkable
                                    && !map[Position.Row-1,Position.Column].
                                        Position.HasExit)
                                        this.Position.Row -= 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Right
                                        if (map[Position.Row,Position.Column+1].
                                            Position.Walkable
                                        && !map[Position.Row,Position.Column+1].
                                            Position.HasExit)
                                            this.Position.Column += 1;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Right
            else if (distanceX > 0)
            {   // Top Right
                if (distanceY < 0)
                {
                    if (chance == 0)
                    {   // Up
                        if (map[Position.Row-1,Position.Column].
                            Position.Walkable
                        && !map[Position.Row-1,Position.Column].
                            Position.HasExit)
                            this.Position.Row -= 1; 
                        else
                        {   // Right
                            if (map[Position.Row,Position.Column+1].
                                Position.Walkable
                            && !map[Position.Row,Position.Column+1].
                                Position.HasExit)
                                this.Position.Column += 1; 
                            else
                            {
                                try
                                {   // Left
                                    if (map[Position.Row,Position.Column-1].
                                        Position.Walkable
                                    && !map[Position.Row,Position.Column-1].
                                        Position.HasExit)
                                        this.Position.Column -= 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Bottom
                                        if (map[Position.Row+1,Position.Column].
                                            Position.Walkable
                                        && !map[Position.Row+1,Position.Column].
                                            Position.HasExit)
                                            this.Position.Row += 1;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    if (chance == 1)
                    {   // Right
                        if (map[Position.Row,Position.Column+1].
                            Position.Walkable
                        && !map[Position.Row,Position.Column+1].
                            Position.HasExit)
                            this.Position.Column += 1; 
                        else
                        {
                            // Up
                            if (map[Position.Row-1,Position.Column].
                                Position.Walkable
                            && !map[Position.Row-1,Position.Column].
                                Position.HasExit)
                                this.Position.Row -= 1; 
                            else
                            {
                                try
                                {   // Bottom
                                    if (map[Position.Row+1,Position.Column].
                                        Position.Walkable
                                    && !map[Position.Row+1,Position.Column].
                                        Position.HasExit)
                                        this.Position.Row += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Left
                                        if (map[Position.Row,Position.Column-1].
                                            Position.Walkable
                                        && !map[Position.Row,Position.Column-1].
                                            Position.HasExit)
                                            this.Position.Column -= 1;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }

                // Bottom Right
                if (distanceY > 0)
                {
                    if (chance == 0)
                    {   // Down
                        if (map[Position.Row+1,Position.Column].
                            Position.Walkable
                        && !map[Position.Row+1,Position.Column].
                            Position.HasExit)
                            this.Position.Row += 1; 
                        else
                        {   // Right
                            if (map[Position.Row,Position.Column+1].
                                Position.Walkable
                            && !map[Position.Row,Position.Column+1].
                                Position.HasExit)
                                this.Position.Column += 1; 
                            else
                            {
                                try
                                {   // Left
                                    if (map[Position.Row,Position.Column-1].
                                        Position.Walkable
                                    && !map[Position.Row,Position.Column-1].
                                        Position.HasExit)
                                        this.Position.Column -= 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Up
                                        if (map[Position.Row-1,Position.Column].
                                            Position.Walkable
                                        && !map[Position.Row-1,Position.Column].
                                            Position.HasExit)
                                            this.Position.Row -= 1;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    if (chance == 1)
                    {   // Right
                        if (map[Position.Row,Position.Column+1].
                            Position.Walkable
                        && !map[Position.Row,Position.Column+1].
                            Position.HasExit)
                            this.Position.Column += 1; 
                        else
                        {
                            // Down
                            if (map[Position.Row+1,Position.Column].
                                Position.Walkable
                            && !map[Position.Row+1,Position.Column].
                                Position.HasExit)
                                this.Position.Row += 1; 
                            else
                            {
                                try
                                {   // Up
                                    if (map[Position.Row+1,Position.Column].
                                        Position.Walkable
                                    && !map[Position.Row+1,Position.Column].
                                        Position.HasExit)
                                        this.Position.Row += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Left
                                        if (map[Position.Row,Position.Column-1].
                                            Position.Walkable
                                        && !map[Position.Row,Position.Column-1].
                                            Position.HasExit)
                                            this.Position.Column -= 1;
                                    }
                                    catch
                                    {
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