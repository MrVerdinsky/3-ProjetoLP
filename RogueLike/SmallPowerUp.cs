namespace RogueLike
{
    sealed public class SmallPowerUp : PowerUp
    {
        public SmallPowerUp(Position position)
        {
            base.position   = position;
            base.heal       = 4;
        }
    }
}