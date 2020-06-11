namespace RogueLike
{
    /// <summary>
    /// PowerUp class
    /// </summary>
    sealed internal class PowerUp : Position
    {
        /// <summary>
        /// Auto-implemented property that creates the number which the 
        /// power up will heal
        /// </summary>
        /// <value>Amount of Healing from the PowerUp</value>
        internal int          Heal      { get; private set; }

        /// <summary>
        /// Auto-implemented property that checks if the power up has been 
        /// picked up by the player 
        /// </summary>
        /// <value>True if the power up has been picked up
        ///  otherwise false</value>
        internal bool         Picked    { get; private set; }
        /// <summary>
        /// Auto-implemented property that creates the power ups symbols
        /// </summary>
        /// <value>PowerUps Icons</value>
        internal string       Symbol      { get; private set; }

        /// <summary>
        /// Creates a PowerUp
        /// </summary>
        /// <param name="row">Sets a row the powerUp</param>
        /// <param name="column">Sets a column position for the powerUp</param>
        /// <param name="heal">Heal amount</param>
        internal PowerUp(int row, int column, int heal)
        {
            Row                 = row;
            Column              = column;
            Heal                = heal;
            Picked              = false;
            SetSymbol();
        }

        /// <summary>
        /// Sets picked to true
        /// </summary>
        internal void PickUp()
        {
            Picked = true;
        }
        /// <summary>
        /// Defines the power up symbol to be printed based on its Heal value
        /// </summary>
        private void SetSymbol()
        {
            if (Heal == 4) Symbol = "🍙|";
            else if (Heal == 8) Symbol = "🧀|";
            else if (Heal == 16) Symbol = "🍖|";
        }

    }
}