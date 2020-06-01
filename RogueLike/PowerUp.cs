namespace RogueLike
{
    /// <summary>
    /// PowerUp class
    /// </summary>
    sealed public class PowerUp
    {
        internal Position     Position  { get; private set; }
        internal int          Heal      { get; private set; }
        internal bool         Picked    { get; private set; }

        /// <summary>
        /// Creates a PowerUp
        /// </summary>
        /// <param name="position">Position of the PowerUp</param>
        /// <param name="heal">Heal amount</param>
        public PowerUp(Position position, int heal)
        {
            Position            = position;
            Heal                = heal;
            Picked              = false;
        }

        /// <summary>
        /// Sets picked to true
        /// </summary>
        public void PickUp()
        {
            Picked = true;
        }
    }
}