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
    }
}
