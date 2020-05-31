namespace RogueLike
{
    /// <summary>
    /// Boss class, created from Enemy class
    /// </summary>
    sealed public class Boss : Enemy
    {
        /// <summary>
        /// Creates a Boss
        /// </summary>
        /// <param name="position">Position of the Enemy</param>
        public Boss (Position position)
        {
            base.Position   = position;
            base.damage     = 10;
            base.movement   = 1;
        }
    }
}