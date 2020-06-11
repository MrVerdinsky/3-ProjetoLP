namespace RogueLike
{
    /// <summary>
    /// Position for every element in the game
    /// </summary>
    internal class Position
    {
        /// <summary>
        /// Auto-implemented property that represents the level's max rows
        /// </summary>
        /// <value>Current Level's number of rows</value>
        internal int Row        { get; set; }

        /// <summary>
        /// Auto-implemented property that represents the level's max columns
        /// </summary>
        /// <value>Current Level's number of column</value>
        internal int Column     { get; set; }

        /// <summary>
        /// Auto-implemented property that checks if a position is empty
        /// </summary>
        /// <value>True if a position is empty false if occupied</value>
        internal bool Empty         { get; set; } = true;

        /// <summary>
        /// Auto-implemented property that checks if the player can move to 
        /// a specific position
        /// </summary>
        /// <value>True if the player can move, otherwise false</value>
        internal bool Walkable      { get; set; } = true;

        /// <summary>
        /// Auto-implemented property that checks if the position has 
        /// the player in it
        /// </summary>
        /// <value>True if the position has the player otherwise false</value>
        internal bool IsPlayer     { get; set; } = false;

        /// <summary>
        /// Auto-implemented property that checks if the position has 
        /// an enemy in it
        /// </summary>
        /// <value>True if the position has an enemy otherwise false</value>
        internal bool IsEnemy      { get; set; } = false;

        /// <summary>
        /// Auto-implemented property that checks if the position has 
        /// the exit in it
        /// </summary>
        /// <value>True if the position has the exit otherwise false</value>
        internal bool IsExit       { get; set; } = false;

        /// <summary>
        /// Auto-implemented property that checks if the position has 
        /// a power up in it
        /// </summary>
        /// <value>True if the position has a power up otherwise false</value>
        internal bool IsPowerUp    { get; set; } = false;

        /// <summary>
        /// Auto-implemented property that checks if the position has 
        /// an obstacle in it
        /// </summary>
        /// <value>True if the position has an obstacle otherwise false</value>
        internal bool IsWall       { get; set; } = false;

        /// <summary>
        /// Gets the position hash code based on its Row and Column values
        /// </summary>
        /// <returns>A hash code for the current position</returns>
        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Column.GetHashCode();
        }

        /// <summary>
        /// Determines whether two Position instances are equal or not.
        /// </summary>
        /// <param name="obj">The Position to compare with the 
        /// current Position.</param>
        /// <returns>true if the specified object is equal to the current 
        /// object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            Position other = obj as Position;
            if (other == null) return false;
            return Row == other.Row && Column == other.Column;

        }
    }
}
