namespace RogueLike
{
    sealed public class Map
    {
        internal Position Position      { get; private set; }

        public Map(Position position)
        {
            Position = position;
        }
    }
}