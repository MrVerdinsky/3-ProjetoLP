namespace RogueLike
{
    sealed public class Player : Character
    {
        private int HP;

        public Player (Position position)
        {
            base.Position = position;
            base.movement = 2;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP - damage < 1)
            {
                // Player dies

            }
        }
    } 
}