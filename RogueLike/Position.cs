namespace RogueLike
{
    /// <summary>
    /// Position for every element in the game
    /// </summary>
    internal class Position
    {
        internal int Row        { get; set; }
        internal int Column     { get; set; }
        internal bool Empty         { get; set; } = true;
        internal bool Walkable      { get; set; } = true;
        internal bool IsPlayer     { get; set; } = false;
        internal bool IsEnemy      { get; set; } = false;
        internal bool IsExit       { get; set; } = false;
        internal bool IsPowerUp    { get; set; } = false;
        internal bool IsWall       { get; set; } = false;

        /// <summary>
        /// Gets the position hash code based on its Row and Column values
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Column.GetHashCode();
        }

        /// <summary>
        /// Determines whether two Position instances are equal or not.
        /// </summary>
        /// <param name="obj">The Position to compare with the 
        /// current Position.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Position other = obj as Position;
            if (other == null) return false;
            return Row == other.Row && Column == other.Column;

        }
    }
}
