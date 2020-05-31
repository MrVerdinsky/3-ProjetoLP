namespace RogueLike
{
    /// <summary>
    /// MediumPowerUp class, created from PowerUp class
    /// </summary>
    sealed public class MediumPowerUp : PowerUp
    {
        /// <summary>
        /// Creates MediumPowerUp
        /// </summary>
        /// <param name="position">Position of the PowerUp</param>
        public MediumPowerUp(Position position)
        {
            base.position   = position;
            base.heal       = 8;
        }
    }
}