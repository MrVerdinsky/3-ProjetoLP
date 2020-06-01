namespace RogueLike
{
    /// <summary>
    /// Enemy class, created from Character class
    /// </summary>
    public class Enemy : Character
    {
        internal int damage { get; private set; }

        /// <summary>
        /// Creates an Enemy
        /// </summary>
        /// <param name="position">Sets the enemy position</param>
        /// <param name="damage">Sets the enemy damage</param>
        public Enemy (Position position, int damage)
        {
            base.movement   = 1;
            base.Position   = position;
            this.damage     = damage;
        }
    }
}