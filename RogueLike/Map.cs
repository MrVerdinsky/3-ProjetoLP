namespace RogueLike
{
    /// <summary>
    /// Holds map positions properties
    /// </summary>
    sealed internal class Map : Position
    {
        /// <summary>
        /// Creates game's map
        /// </summary>
        /// <param name="position">All squares position's</param>
        internal Map(int row, int column)
        {
            Row     = row;
            Column  = column;
        }

        /// <summary>
        /// Occupies the map position based on the given argument
        /// </summary>
        /// <param name="element">Game element with which is intended to 
        /// occupy the position.</param>
        internal void Occupy(string element)
        {
            switch (element)
            {
                case "enemy":
                    base.IsEnemy     = true;
                    base.Empty       = false;
                    base.Walkable    = false;
                    break;
                case "power_up":
                    base.IsPowerUp   = true;
                    base.Empty       = false;
                    base.Walkable    = true;
                    break;
                case "exit":
                    base.Empty       = false;
                    base.Walkable    = true;
                    base.IsExit      = true;
                    break;
                case "wall":
                    base.IsWall      = true;
                    base.Empty       = false;
                    base.Walkable    = false;
                    break;
                case "player":
                    base.IsPlayer    = true;
                    base.Empty       = false;
                    base.Walkable    = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Frees the map position based on the given argument
        /// </summary>
        /// <param name="element">Game element that is supposed to
        /// take out of the position</param>
        internal void Free(string element)
        {
            switch (element)
            {
                case "enemy":
                    base.IsEnemy     = false;
                    base.Empty       = true;
                    base.Walkable    = true;
                    break;
                case "enemy_power_up":
                    base.IsEnemy     = false;
                    base.Empty       = false;
                    base.Walkable    = true;
                    break;
                case "power_up":
                    base.IsPowerUp   = false;
                    base.Empty       = true;
                    base.Walkable    = true;
                    break;
                case "exit":
                    base.IsExit     = false;
                    break;
                case "wall":
                    base.IsWall      = true;
                    base.Empty       = false;
                    base.Walkable    = false;
                    break;
                case "player":
                    base.IsPlayer    = false;
                    base.Empty        = true;
                    base.Walkable     = true;
                    break;
                default:
                    break;
            }
        }

    }
}