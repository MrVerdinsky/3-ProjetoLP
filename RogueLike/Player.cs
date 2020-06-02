using System; //tESTING
namespace RogueLike
{
    /// <summary>
    /// Player class, created from Character class
    /// </summary>
    sealed public class Player : Character
    {
        internal int    HP          { get; private set; }
        internal bool   IsAlive     { get; private set; }

        /// <summary>
        /// Creates the player
        /// </summary>
        /// <param name="position">Gives a position to the player</param>
        /// <param name="gameRows">Used to give player's initial HP</param>
        /// <param name="gameColumns">Used to give player's initial HP</param>
        public Player (Position position, int gameRows, int gameColumns)
        {
            base.Position           = position;
            base.Movement           = 2;
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
            {
                IsAlive = false;
            }
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
        /// Moves the Player
        /// </summary>
        /// <param name="map">All map Positions</param>
        /// <param name="input">Gets which character the user pressed</param>
        /// <returns>Returns true if the movement is possible 
        /// otherwise false</returns>
        public bool Move(Map[,] map, char input)
        {
            
            // If movement is > 0, removes 1 movement and moves the player
            if (base.Movement > 0)
            {
                base.Movement -= 1;

                //Conditions used to check if 
                //chosen Input goes into an occupied position
                switch(input)
                {
                    case 'a':
                        if (map[this.Position.Row, 
                            this.Position.Column-1].Position.Walkable == false)
                                return false;
                        else
                            this.Position.Column -= 1;
                        return true;
                    case 'd':
                        if (map[this.Position.Row, 
                            this.Position.Column+1].Position.Walkable == false)
                                return false;
                        else
                            this.Position.Column += 1;
                        return true;
                    case 'w':
                        if (map[this.Position.Row-1, 
                            this.Position.Column].Position.Walkable == false)
                                return false;
                        else
                            this.Position.Row -= 1;
                        return true;
                    case 's':
                        if (map[this.Position.Row+1, 
                            this.Position.Column].Position.Walkable == false)
                                return false;
                        else
                            this.Position.Row += 1;
                        return true;
                    default:
                        break;// PEDIR AO RENDER PARA IMRPIMIR QUE N ACEITA COMANDO
                        
                }
            
            }
            // If the player has no movements left
            else
            {
                // PEDIR AO RENDER PARA IMPRIMIR QUE NAO DA PARA ANDAR MAIS
            }
            return false;
        }

        /// <summary>
        /// Resets the number of times the player can move in a turn
        /// </summary>
        public void MovementReset()
        {
            base.Movement = 2;
        }
    } 
}