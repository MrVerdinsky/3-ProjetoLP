namespace RogueLike
{
    /// <summary>
    /// Character class
    /// </summary>
    public class Character
    {
        internal Position   Position { get; set; }
        public int          Movement { get; protected set; }
    }
}