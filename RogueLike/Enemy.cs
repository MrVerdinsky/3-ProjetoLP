namespace RogueLike
{
    /// <summary>
    /// Enemy class, created from Position class
    /// </summary>
    sealed internal class Enemy : Position
    {
        /// <summary>
        /// Auto-implemented property that creates the enemies damage
        /// </summary>
        /// <value>Enemies Damage</value>
        internal int Damage     { get; private set; }

        /// <summary>
        /// Auto-implemented property that creates the enemies symbol
        /// </summary>
        /// <value>Enemies Icon</value>
        internal string Symbol  { get; private set; }

        /// <summary>
        /// Creates an Enemy
        /// </summary>
        /// <param name="row">Sets the enemy row</param>
        /// <param name="column">Sets the enemy column</param>
        /// <param name="damage">Sets the enemy damage</param>
        internal Enemy (int row, int column, int damage)
        {
            Row             = row;
            Column          = column;
            Damage          = damage;
            SetSymbol();
        }

        /// <summary>
        /// Sets the enemy symbol to be printed based on its Damage
        /// </summary>
        private void SetSymbol()
        {
            if (Damage == 5){Symbol = "🐀|";}
            else if (Damage == 10){Symbol = "🐉|";};
        }
        
        /// <summary>
        /// Compares the enemy's position with the player position.
        /// Checks the closest path beetween both of them.
        /// Moves the enemy randomly to the nearest walkable position
        /// </summary>
        /// <param name="player">Player position</param>
        /// <param name="random">Random umber to move the enemy</param>
        /// <param name="map">Map positions</param>
        internal void Move(Player player, int random, Map[,] map)
        {
            int distanceX = (player.Column - this.Column);
            int distanceY = (player.Row - this.Row);

            // Number beetween 0 and 1
            int chance = random;  
            // Number 1 or -1
            int rMove;
            rMove = chance == 1 ? +1: -1;

            // One square range
            if (this.Row == player.Row +1 && this.Column == player.Column ||
                this.Row == player.Row -1 && this.Column == player.Column ||
                this.Column == player.Column -1 && this.Row == player.Row ||
                this.Column == player.Column +1 && this.Row == player.Row)
                { 
                }

            // Same column
            else if (distanceX == 0)
            {   // Move Up
                if (distanceY < 0)
                {   // Up
                    if (map[Row-1,Column].Walkable&& !map[Row-1,Column].IsExit)
                        this.Row -= 1;
                    else
                    {
                        try
                        {   // Move left or right
                            if (map[Row,Column+rMove].Walkable 
                            && !map[Row,Column+rMove].IsExit)
                                this.Column += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Row,Column-rMove].Walkable
                                && !map[Row,Column-rMove].IsExit)
                                    this.Column -= rMove;
                            }
                            catch
                            {
                                try
                                {   // Bottom
                                    if (map[Row+1,Column].Walkable
                                    && !map[Row+1,Column].IsExit)
                                        this.Row += 1;
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
                    if (map[Row+1,Column].Walkable
                    && !map[Row+1,Column].IsExit)
                        this.Row += 1;
                    else
                    {
                        try
                        {   // Move left or right
                            if (map[Row,Column+rMove].Walkable
                            && !map[Row,Column+rMove].IsExit)
                                this.Column += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Row,Column-rMove].Walkable
                                && !map[Row,Column-rMove].IsExit)
                                    this.Column -= rMove;
                            }
                            catch
                            {
                                try
                                {   // Top
                                    if (map[Row-1,Column].Walkable
                                    && !map[Row-1,Column].IsExit)
                                        this.Row -= 1;
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
                    if (map[Row,Column-1].Walkable && !map[Row,Column-1].IsExit)
                        this.Column -= 1;
                    else
                    {
                        try
                        {   // Move top or down
                            if (map[Row+rMove,Column].Walkable
                            && !map[Row+rMove,Column].IsExit)
                                this.Row += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Row-rMove,Column].Walkable
                                && !map[Row-rMove,Column].IsExit)
                                    this.Row -= rMove;
                            }
                            catch
                            {
                                try
                                {
                                    if (map[Row,Column+1].Walkable
                                    && !map[Row,Column+1].IsExit)
                                        this.Column += 1;
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
                    if (map[Row,Column+1].Walkable && !map[Row,Column+1].IsExit)
                        this.Column += 1;
                    else
                    {
                        try
                        {   // Move left or right
                            if (map[Row+rMove,Column].Walkable
                            && !map[Row+rMove,Column].IsExit)
                                this.Row += rMove;
                        }
                        catch
                        {
                            try
                            {
                                if (map[Row-rMove,Column].Walkable
                                && !map[Row-rMove,Column].IsExit)
                                    this.Row -= rMove;
                            }
                            catch
                            {
                                try
                                {
                                    if (map[Row,Column-1].Walkable
                                    && !map[Row,Column-1].IsExit)
                                        this.Column -= 1;
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
                        if (map[Row-1,Column].Walkable
                        && !map[Row-1,Column].IsExit)
                            this.Row -= 1; 
                        else
                        {   // Left
                            if (map[Row,Column-1].Walkable
                            && !map[Row,Column-1].IsExit)
                                this.Column -= 1; 
                            else
                            {
                                try
                                {   // Right
                                    if (map[Row,Column+1].Walkable
                                    && !map[Row,Column+1].IsExit)
                                        this.Column += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Bottom
                                        if (map[Row+1,Column].Walkable
                                        && !map[Row+1,Column].IsExit)
                                            this.Row += 1;
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
                        if (map[Row,Column-1].Walkable
                        && !map[Row,Column-1].IsExit)
                            this.Column -= 1; 
                        else
                        {
                            // Up
                            if (map[Row-1,Column].Walkable
                            && !map[Row-1,Column].IsExit)
                                this.Row -= 1; 
                            else
                            {
                                try
                                {   // Bottom
                                    if (map[Row+1,Column].Walkable
                                    && !map[Row+1,Column].IsExit)
                                        this.Row += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Right
                                        if (map[Row,Column+1].Walkable
                                        && !map[Row,Column+1].IsExit)
                                            this.Column += 1;
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
                        if (map[Row+1,Column].Walkable
                        && !map[Row+1,Column].IsExit)
                            this.Row += 1; 
                        else
                        {   // Left
                            if (map[Row,Column-1].Walkable
                            && !map[Row,Column-1].IsExit)
                                this.Column -= 1; 
                            else
                            {
                                try
                                {   // Right
                                    if (map[Row,Column+1].Walkable
                                    && !map[Row,Column+1].IsExit)
                                        this.Column += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Up
                                        if (map[Row-1,Column].Walkable
                                        && !map[Row-1,Column].IsExit)
                                            this.Column -= 1;
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
                        if (map[Row,Column-1].Walkable
                        && !map[Row,Column-1].IsExit)
                            this.Column -= 1; 
                        else
                        {
                            // Down
                            if (map[Row+1,Column].Walkable
                            && !map[Row+1,Column].IsExit)
                                this.Row += 1; 
                            else
                            {
                                try
                                {   // Up
                                    if (map[Row-1,Column].Walkable
                                    && !map[Row-1,Column].IsExit)
                                        this.Row -= 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Right
                                        if (map[Row,Column+1].Walkable
                                        && !map[Row,Column+1].IsExit)
                                            this.Column += 1;
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
                        if (map[Row-1,Column].Walkable
                        && !map[Row-1,Column].IsExit)
                            this.Row -= 1; 
                        else
                        {   // Right
                            if (map[Row,Column+1].Walkable
                            && !map[Row,Column+1].IsExit)
                                this.Column += 1; 
                            else
                            {
                                try
                                {   // Left
                                    if (map[Row,Column-1].Walkable
                                    && !map[Row,Column-1].IsExit)
                                        this.Column -= 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Bottom
                                        if (map[Row+1,Column].Walkable
                                        && !map[Row+1,Column].IsExit)
                                            this.Row += 1;
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
                        if (map[Row,Column+1].Walkable
                        && !map[Row,Column+1].IsExit)
                            this.Column += 1; 
                        else
                        {
                            // Up
                            if (map[Row-1,Column].Walkable
                            && !map[Row-1,Column].IsExit)
                                this.Row -= 1; 
                            else
                            {
                                try
                                {   // Bottom
                                    if (map[Row+1,Column].Walkable
                                    && !map[Row+1,Column].IsExit)
                                        this.Row += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Left
                                        if (map[Row,Column-1].Walkable
                                        && !map[Row,Column-1].IsExit)
                                            this.Column -= 1;
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
                        if (map[Row+1,Column].Walkable
                        && !map[Row+1,Column].IsExit)
                            this.Row += 1; 
                        else
                        {   // Right
                            if (map[Row,Column+1].Walkable
                            && !map[Row,Column+1].IsExit)
                                this.Column += 1; 
                            else
                            {
                                try
                                {   // Left
                                    if (map[Row,Column-1].Walkable
                                    && !map[Row,Column-1].IsExit)
                                        this.Column -= 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Up
                                        if (map[Row-1,Column].Walkable
                                        && !map[Row-1,Column].IsExit)
                                            this.Row -= 1;
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
                        if (map[Row,Column+1].Walkable
                        && !map[Row,Column+1].IsExit)
                            this.Column += 1; 
                        else
                        {
                            // Down
                            if (map[Row+1,Column].Walkable
                            && !map[Row+1,Column].IsExit)
                                this.Row += 1; 
                            else
                            {
                                try
                                {   // Up
                                    if (map[Row+1,Column].Walkable
                                    && !map[Row+1,Column].IsExit)
                                        this.Row += 1;
                                }
                                catch
                                {   
                                    try
                                    {
                                        // Left
                                        if (map[Row,Column-1].Walkable
                                        && !map[Row,Column-1].IsExit)
                                            this.Column -= 1;
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