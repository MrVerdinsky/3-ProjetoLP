namespace RogueLike
{
    /// <summary>
    /// Player class, created from Character class
    /// </summary>
    sealed public class Player : Character
    {
        internal int    HP              { get; private set; }
        public int      Movement        { get; private set; }
        internal bool   IsAlive         { get; private set; }

        /// <summary>
        /// Creates the player
        /// </summary>
        /// <param name="position">Gives a position to the player</param>
        /// <param name="gameRows">Used to give player's initial HP</param>
        /// <param name="gameColumns">Used to give player's initial HP</param>
        public Player (Position position, int gameRows, int gameColumns)
        {
            base.Position           = position;
            HP                      = (gameRows * gameColumns) / 4;
            IsAlive                 = true;
        }

        /// <summary>
        /// Player loses HP equal to enemy.damage
        /// </summary>
        /// <param name="enemy">To get damage value from enemy</param>
        public void TakeDamage(Enemy enemy)
        {
            HP -= enemy.damage;
            if (HP - enemy.damage < 1)
                IsAlive = false;
        }

        /// <summary>
        /// Player recovers HP equal to powerUp.heal and destroys the powerup
        /// </summary>
        /// <param name="powerUp">To get heal value from powerUp</param>
        public void PickPowerUp(PowerUp powerUp)
        {
            if (powerUp.Picked == false)
            {
                HP += powerUp.Heal;
                powerUp.Position.PowerUpFree();
                powerUp.PickUp();
            }
        }

        /// <summary>
        /// Moves the player
        /// </summary>
        /// <param name="input">Gets which character the user pressed</param>
        public void Move(char input)
        {
            Movement -= 1;
            HP -= 1;
            if (HP < 1) IsAlive = false;

            switch(input)
            {
                case 'a':
                    this.Position.Column -= 1;
                    break;
                case 'd':
                    this.Position.Column += 1;
                    break;
                case 'w':
                    this.Position.Row -= 1;
                    break;
                case 's':
                    this.Position.Row += 1;
                    break;
                default:
                    // PEDIR AO RENDER PARA IMRPIMIR QUE N ACEITA COMANDO
                    break;
            } 
        }

        public void MovementReset()
        {
            Movement = 2;
        }
    } 
}