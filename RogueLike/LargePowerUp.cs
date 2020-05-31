namespace RogueLike
{
    /// <summary>
    /// LargePowerUp class, created from PowerUp class
    /// </summary>
    sealed public class LargePowerUp : PowerUp
    {
        /// <summary>
        /// Creates LargePowerUp
        /// </summary>
        /// <param name="position">Position of the PowerUp</param>
        public LargePowerUp(Position position)
        {
            base.position   = position;
            base.heal       = 16;
        }
    }
}