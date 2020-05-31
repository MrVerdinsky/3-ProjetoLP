namespace RogueLike
{
    sealed public class Boss : Enemy
    {
        public Boss (Position position)
        {
            base.Position   = position;
            base.damage     = 10;
            base.movement   = 1;
        }
    }
}