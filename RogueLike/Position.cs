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
