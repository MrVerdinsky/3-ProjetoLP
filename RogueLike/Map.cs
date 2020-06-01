namespace RogueLike
{
    sealed public class Map
    {
        private int numberOfRows;
        private int numberOfColumns;

        public Map(int numberOfRows, int numberOfColumns)
        {
            this.numberOfRows       = numberOfRows;
            this.numberOfColumns    = numberOfColumns;
        }
    }
}