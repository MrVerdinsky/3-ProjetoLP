namespace RogueLike
{
    sealed public class MediumPowerUp : PowerUp
    {
        public MediumPowerUp(Position position)
        {
            base.position   = position;
            base.heal       = 8;
        }
    }
}