namespace RogueLike
{
    sealed public class Position
    {
        internal int Row        { get; set; }
        internal int Column     { get; set; }
        internal bool Empty         { get; set; } = true;
        internal bool Walkable      { get; set; } = true;
        internal bool HasPlayer     { get; set; } = false;
        internal bool HasEnemy      { get; set; } = false;
        internal bool HasPowerUp    { get; set; } = false;
        internal bool HasWall       { get; set; } = false;
        

        /// <summary>
        /// Creates a position
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
            Empty     = true;
            Walkable     = true;
        }
        public void EnemyFree(){
            HasEnemy    = false;
            Empty    = true;
            Walkable    = true;
        }
        public void PowerUpFree(){
            HasPowerUp  = false;
            Empty    = true;
            Walkable    = true;
        }

        public void PlayerOccupy(){
            HasPlayer   = true;
            Empty    = false;
            Walkable    = false;
        }
        public void EnemyOccupy(){
            HasEnemy    = true;
            Empty    = false;
            Walkable    = false;
        }
        public void PowerUpOccupy(){
            HasPowerUp  = true;
            Empty    = false;
            Walkable    = true;
        }
    }
}
