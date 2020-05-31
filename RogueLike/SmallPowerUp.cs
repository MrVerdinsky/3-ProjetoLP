namespace RogueLike
{
    /// <summary>
    /// SmallPowerUp class, created from PowerUp class
    /// </summary>
    sealed public class SmallPowerUp : PowerUp
    {
        /// <summary>
        /// Creates SmallPowerUp
        /// </summary>
        /// <param name="position">Position of the PowerUp</param>
        public SmallPowerUp(Position position)
        {
            base.position   = position;
            base.heal       = 4;
        }
    }
}