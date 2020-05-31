namespace RogueLike
{
    sealed public class Minion : Enemy
    {
        public Minion (Position position)
        {
            base.Position   = position;
            base.damage     = 5;
            base.movement   = 1;
        }
    }
}