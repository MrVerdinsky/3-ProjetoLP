namespace RogueLike
{
    sealed public class Map : Position
    {
        /// <summary>
        /// Creates game's map
        /// </summary>
        /// <param name="position">All squares position's</param>
        public Map(int row, int column)
        {
            Row     = row;
            Column  = column;
        }
    }
}