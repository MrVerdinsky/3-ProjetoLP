namespace RogueLike
{
    /// <summary>
    /// Minion class, created from Enemy class
    /// </summary>
    sealed public class Minion : Enemy
    {
        /// <summary>
        /// Creates a Minion
        /// </summary>
        /// <param name="position">Position of the Enemy</param>
        public Minion (Position position)
        {
            base.Position   = position;
            base.damage     = 5;
            base.movement   = 1;
        }
    }
}