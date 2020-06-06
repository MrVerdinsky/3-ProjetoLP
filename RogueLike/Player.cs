using System; //tESTING
namespace RogueLike
{ 
    /// <summary>
    /// Player class, created from ObjectPosition class
    /// </summary>
    sealed public class Player : Position
    {
        static internal int HP = (Game.rows * Game.columns) / 4;

        internal int    Movement        { get; private set; }
        internal bool   IsAlive         { get; private set; }
        internal bool   HasLeft         { get; private set; }
        internal bool   Walked          { get; private set; }
        
        /// <summary>
        /// Creates the player
        /// </summary>
        /// <param name="row">Sets a player row</param>
        /// <param name="column">Sets a player column</param>
        public Player (int row, int column)
        {
            Row         = row;
            Column      = Column;
            IsAlive     = true;
            Walked      = false;
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
            if (HP < 0) HP = 0;
        }

        /// <summary>
        /// Player recovers HP equal to powerUp.heal and destroys the powerup
        /// </summary>
        /// <param name="map">Gets map for position</param>
        /// <param name="powerUp">Gets powerup heal power</param>
        public void PickPowerUp(Map[,] map, PowerUp powerUp)
        {
                HP += powerUp.Heal;
                map[powerUp.Row, powerUp.Column].PowerUpFree();
                map[powerUp.Row, powerUp.Column].PlayerOccupy();
                powerUp.PickUp();
        }

        /// <summary>
        /// Moves the Player
        /// </summary>
        /// <param name="map">All map Positions</param>
        /// <param name="input">Gets which character the user pressed</param>
        /// <param name="print">Gets renderer</param>
        /// <returns>Returns true if the movement is possible 
        /// otherwise false</returns>
        public bool Move(Map[,] map, ConsoleKeyInfo input, Renderer print)
        {
            //Checks if the player can move
            bool canMove = false;
        
            //chosen Input goes into an occupied position
            try
            {

                switch(input.Key)
                {
                    //Moves Left
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        if (map[this.Row,this.Column-1].Walkable == false)
                                canMove = false;
                        else
                        {
                            this.Column -= 1;
                            canMove = true;
                            
                        }
                        break; 

                    //Moves Right
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        if (map[this.Row, this.Column+1].Walkable == false)
                                canMove = false;
                        else
                        {
                            this.Column += 1;
                            canMove = true;
                        }                     
                        break;
                    
                    //Moves Upwards
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (map[this.Row-1, this.Column].Walkable == false)
                                canMove = false;     
                        else
                        {
                            this.Row -= 1;
                            canMove = true; 
                        }   
                        break;

                    //Moves Downwards
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (map[this.Row+1, this.Column].Walkable == false)
                            canMove = false;
                        else
                        {
                            this.Row += 1;
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
            // If the player moves, adds the movement to actions list
            if (canMove)
            {
                Walked = true;
                print.GetGameActions(input);
            }

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
        /// Damages the player and spends one movement
        /// </summary>
        public void MovementDamage()
        {
            Movement            -= 1;
            HP                  -= 1;
            if (HP < 1) IsAlive = false;
            Walked              = false;
        }

        /// <summary>
        /// Changes the player status to Dead
        /// </summary>
        public void Die() => IsAlive = false;

        /// <summary>
        /// If the user leaves the game
        /// </summary>
        public void LeaveGame() => HasLeft = true;

    } 
}