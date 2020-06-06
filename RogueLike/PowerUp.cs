using System.Collections;
using System.Collections.Generic;

namespace RogueLike
{
    /// <summary>
    /// PowerUp class
    /// </summary>
    sealed public class PowerUp : Position
    {
        internal int          Heal      { get; private set; }
        internal bool         Picked    { get; private set; }
        internal string       Symbol      { get; private set; }

        /// <summary>
        /// Creates a PowerUp
        /// </summary>
        /// <param name="row">Sets a row the powerUp</param>
        /// <param name="column">Sets a column position for the powerUp</param>
        /// <param name="heal">Heal amount</param>
        public PowerUp(int row, int column, int heal)
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
        public void PickUp()
        {
            Picked = true;
        }

        private void SetSymbol()
        {
            if (Heal == 4) Symbol = "🍙|";
            else if (Heal == 8) Symbol = "🧀|";
            else if (Heal == 16) Symbol = "🍖|";
        }

    }
}