namespace RogueLike
{
    sealed public class LargePowerUp : PowerUp
    {
        public LargePowerUp(Position position)
        {
            base.position   = position;
            base.heal       = 16;
        }
    }
}