namespace RogueLike
{
    sealed public class Map
    {
        internal Position Position      { get; private set; }

        /// <summary>
        /// Creates game's map
        /// </summary>
        /// <param name="position">All squares position's</param>
        public Map(Position position)
        {
            Position = position;
        }
    }
}