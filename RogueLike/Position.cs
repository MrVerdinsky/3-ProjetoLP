namespace RogueLike
{
    /// <summary>
    /// Position for every element in the game
    /// </summary>
    public class Position
    {
        public int Row        { get; set; }
        public int Column     { get; set; }
        internal bool Empty         { get; set; } = true;
        internal bool Walkable      { get; set; } = true;
        internal bool IsPlayer     { get; set; } = false;
        internal bool IsEnemy      { get; set; } = false;
        internal bool IsExit       { get; set; } = false;
        internal bool IsPowerUp    { get; set; } = false;
        internal bool IsWall       { get; set; } = false;
        

        /// <summary>
        /// Removes the player position and declares it empty and walkable
        /// </summary>
        public void PlayerFree()
        {
            IsPlayer    = false;
            Empty        = true;
            Walkable     = true;
        }

        /// <summary>
        /// Removes the enemy's position from the map
        /// </summary>
        /// <param name="makeEmpty">Chages postion to walkable</param>
        public void EnemyFree(bool makeEmpty = true)
        {
            IsEnemy    = false;
            Empty       = makeEmpty;
            Walkable    = true;
        }

        /// <summary>
        /// Removes the Power-Up position and declares it empty and walkable
        /// </summary>
        public void PowerUpFree()
        {
            IsPowerUp  = false;
            Empty       = true;
            Walkable    = true;
        }

        /// <summary>
        /// Removes the Exit position.
        /// </summary>
        public void ExitFree()
        {
            IsExit     = false;
        }

        /// <summary>
        /// Adds the Player position and declares it occupied and not walkable.
        /// </summary>
        public void PlayerOccupy()
        {
            IsPlayer   = true;
            Empty       = false;
            Walkable    = false;
        }

        /// <summary>
        /// Adds the Enemy position and declares it occupied and not walkable.
        /// </summary>
        public void EnemyOccupy()
        {
            IsEnemy    = true;
            Empty       = false;
            Walkable    = false;
        }

        /// <summary>
        /// Adds the Power-Up position and declares it occupied and walkable.
        /// </summary>
        public void PowerUpOccupy()
        {
            IsPowerUp  = true;
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
            IsExit     = true;
        }

        /// <summary>
        /// Adds the wall position and declares it occupied and walkable.
        /// </summary>
        public void WallOccupy(){
            IsWall     = true;
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
