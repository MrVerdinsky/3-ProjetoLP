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

        /// <summary>
        /// Removes the player position and declares it empty and walkable
        /// </summary>
        public void PlayerFree(){
            HasPlayer    = false;
            Empty       = true;
            Walkable     = true;
        }

        /// <summary>
        /// Removes the Enemy position and declares it empty and walkable
        /// </summary>
        // public void EnemyFree(){
        //     HasEnemy    = false;
        //     Empty       = true;
        //     Walkable    = true;
        // }
        public void EnemyFree(bool makeEmpty = true){
            HasEnemy    = false;
            Empty       = makeEmpty;
            Walkable    = true;
        }
        /// <summary>
        /// Removes the Power-Up position and declares it empty and walkable
        /// </summary>
        public void PowerUpFree(){
            HasPowerUp  = false;
            Empty       = true;
            Walkable    = true;
        }

        /// <summary>
        /// Adds the Player position and declares it occupied and not walkable.
        /// </summary>
        public void PlayerOccupy(){
            HasPlayer   = true;
            Empty       = false;
            Walkable    = false;
        }

        /// <summary>
        /// Adds the Enemy position and declares it occupied and not walkable.
        /// </summary>
        public void EnemyOccupy(){
            HasEnemy    = true;
            Empty       = false;
            Walkable    = false;
        }
        /// <summary>
        /// Adds the Power-Up position and declares it occupied and walkable.
        /// </summary>
        public void PowerUpOccupy(){
            HasPowerUp  = true;
            Empty       = false;
            Walkable    = true;
        }
        /// <summary>
        /// Adds the wall position and declares it occupied and walkable.
        /// </summary>
        public void WallOccupy(){
            HasWall     = true;
            Empty       = false;
            Walkable    = false;
        }
    }
}
