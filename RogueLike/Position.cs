namespace RogueLike
{
    sealed public class Position
    {
        internal int Row        { get; set; }
        internal int Column     { get; set; }
        internal bool Playable      { get; set; } = true;
        internal bool HasPlayer     { get; set; } = false;
        internal bool HasEnemy      { get; set; } = false;
        internal bool HasPowerUp    { get; set; } = false;
        internal bool HasWall       { get; set; } = false;
        

        /// <summary>
        /// Playable position
        /// </summary>
        /// <param name="row">Number of row</param>
        /// <param name="column">Number of column</param>
        public Position(int row, int column)
        {
            Row     = row;
            Column  = column;
        }


        public void PlayerFree(){
            HasPlayer    = false;
            Playable     = true;
        }
        public void EnemyFree(){
            HasEnemy    = false;
            Playable    = true;
        }
        public void PowerUpFree(){
            HasPowerUp  = false;
        }

    }
}
