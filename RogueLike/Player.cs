using System; //tESTING
namespace RogueLike
{ 
    /// <summary>
    /// Player class, created from Character class
    /// </summary>
    sealed public class Player : Character
    {
        //static internal int HP = (Game.rows * Game.columns) / 4;
        static internal int HP = 250;
        internal int    Movement        { get; private set; }
        internal bool   IsAlive         { get; private set; }
        

        /// <summary>
        /// Creates the player
        /// </summary>
        /// <param name="position">Gives a position to the player</param>
        /// <param name="gameRows">Used to give player's initial HP</param>
        /// <param name="gameColumns">Used to give player's initial HP</param>
        public Player (Position position)
        {
            base.Position           = position;
            IsAlive                 = true;
        }

        /// <summary>
        /// Player loses HP equal to Enemies damage
        /// </summary>
        /// <param name="enemy">To get damage value from enemy</param>
        public void TakeDamage(Enemy enemy)
        {
            if (HP - enemy.Damage < 1)
                IsAlive = false;
            HP -= enemy.Damage;  
        }

        /// <summary>
        /// Player recovers HP equal to powerUp.heal and destroys the powerup
        /// </summary>
        /// <param name="powerUp">To get heal value from powerUp</param>
        /// <param name="powerUp">To get map positions</param>
        public void PickPowerUp(Map[,] map, PowerUp powerUp)
        {
                HP += powerUp.Heal;
                map[powerUp.Position.Row, powerUp.Position.Column].
                    Position.PowerUpFree();
                map[powerUp.Position.Row, powerUp.Position.Column].
                    Position.PlayerOccupy();
                powerUp.PickUp();
        }

        /// <summary>
        /// Moves the Player
        /// </summary>
        /// <param name="map">All map Positions</param>
        /// <param name="input">Gets which character the user pressed</param>
        /// <returns>Returns true if the movement is possible 
        /// otherwise false</returns>
        public bool Move(Map[,] map, char input, Renderer print)
        {
            //Checks if the player can move
            bool canMove = false;

            //Conditions used to check if 
            //chosen Input goes into an occupied position
            try
            {
                switch(input)
                {
                    //Moves Left
                    case 'a':
                        if (map[this.Position.Row,this.Position.Column-1].
                            Position.Walkable == false)
                                canMove = false;
                        else
                        {
                            this.Position.Column -= 1;
                            canMove = true;
                            
                        }
                        break; 

                    //Moves Right
                    case 'd':
                        if (map[this.Position.Row, this.Position.Column+1].
                            Position.Walkable == false)
                                canMove = false;
                        else
                        {
                            this.Position.Column += 1;
                            canMove = true;
                        }                     
                        break;
                    
                    //Moves Upwards
                    case 'w':
                        if (map[this.Position.Row-1, this.Position.Column].
                            Position.Walkable == false)
                                canMove = false;     
                        else
                        {
                            this.Position.Row -= 1;
                            canMove = true; 
                        }   
                        break;

                    //Moves Downwards
                    case 's':
                        if (map[this.Position.Row+1, this.Position.Column].
                            Position.Walkable == false)
                            canMove = false;
                        else
                        {
                            this.Position.Row += 1;
                            canMove = true;
                        }
                        break;
                    
                    //Prints error message in case of wrong Input
                    default:
                        print.PrintInputError();
                        break;
                }
                
            }
            catch(IndexOutOfRangeException)
            {}
            if (canMove)
            {
                Movement -= 1;
                HP -= 1;
                print.GetGameActions(input);
            }
            if (HP < 1) IsAlive = false;
            
            return canMove;
        }

        /// <summary>
        /// Resets the number of times the player can move in a turn
        /// </summary>
        public void MovementReset()
        {
            Movement = 2;
        }

        /// <summary>
        /// Changes the player status to Dead
        /// </summary>
        public void Die()
        {
            IsAlive = false;
        }

    } 
}