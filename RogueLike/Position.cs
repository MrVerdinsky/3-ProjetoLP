namespace RogueLike
{
    /// <summary>
    /// Position for every element in the game
    /// </summary>
    sealed public class Position
    {
        internal int Row        { get; set; }
        internal int Column     { get; set; }
        internal bool Empty         { get; set; } = true;
        internal bool Walkable      { get; set; } = true;
        internal bool HasPlayer     { get; set; } = false;
        internal bool HasEnemy      { get; set; } = false;
        internal bool HasExit       { get; set; } = false;
        internal bool HasPowerUp    { get; set; } = false;
        internal bool HasWall       { get; set; } = false;
        

        /// <summary>
        /// Creates a position
        /// </summary>
        /// <param name="row">Number of row</param>
        /// <param name="column">Number of column</param>
        public Position(int row, int column)
        {
            Row     = row;
            Column  = column;
        }

        /// <summary>
        /// Removes the player position and declares it empty and walkable
        /// </summary>
        public void PlayerFree()
        {
            HasPlayer    = false;
            Empty        = true;
            Walkable     = true;
        }

        /// <summary>
        /// Removes the enemy's position from the map
        /// </summary>
        /// <param name="makeEmpty">Chages postion to walkable</param>
        public void EnemyFree(bool makeEmpty = true)
        {
            HasEnemy    = false;
            Empty       = makeEmpty;
            Walkable    = true;
        }

        /// <summary>
        /// Removes the Power-Up position and declares it empty and walkable
        /// </summary>
        public void PowerUpFree()
        {
            HasPowerUp  = false;
            Empty       = true;
            Walkable    = true;
        }

        /// <summary>
        /// Removes the Exit position.
        /// </summary>
        public void ExitFree()
        {
            HasExit     = false;
        }

        /// <summary>
        /// Adds the Player position and declares it occupied and not walkable.
        /// </summary>
        public void PlayerOccupy()
        {
            HasPlayer   = true;
            Empty       = false;
            Walkable    = false;
        }

        /// <summary>
        /// Adds the Enemy position and declares it occupied and not walkable.
        /// </summary>
        public void EnemyOccupy()
        {
            HasEnemy    = true;
            Empty       = false;
            Walkable    = false;
        }

        /// <summary>
        /// Adds the Power-Up position and declares it occupied and walkable.
        /// </summary>
        public void PowerUpOccupy()
        {
            HasPowerUp  = true;
            Empty       = false;
            Walkable    = true;
        }

        /// <summary>
        /// Adds the Exit position and declares it occupied and walkable.
        /// </summary>
        public void ExitOccupy()
        {
            Empty       = false;
            Walkable    = true;
            HasExit     = true;
        }

        /// <summary>
        /// Adds the wall position and declares it occupied and walkable.
        /// </summary>
        public void WallOccupy(){
            HasWall     = true;
            Empty       = false;
            Walkable    = false;
        }

        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Column.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Position other = obj as Position;
            if (other == null) return false;
            return Row == other.Row && Column == other.Column;

        }


    }
}
