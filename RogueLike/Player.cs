using System;
namespace RogueLike
{ 
    /// <summary>
    /// Player class, created from Position class
    /// </summary>
    sealed internal class Player : Position
    {
        /// <summary>
        /// Auto-implemented property that creates the player's HP
        /// based on Level's rows and columns size
        /// </summary>
        /// <value> Player's HP  </value>
        static internal int HP = (Game.rows * Game.columns) / 4;

        /// <summary>
        /// Auto-implemented property that creates the number of the player's 
        /// movement
        /// </summary>
        /// <value> Number of times the player can move</value>
        internal int    Movement        { get; private set; }

         /// <summary>
        /// Auto-implemented property that checks the players Status
        /// movement
        /// </summary>
        /// <value>True if the player still has Hp left or False if it 
        /// hits 0</value>
        internal bool   IsAlive         { get; private set; }

         /// <summary>
        /// Auto-implemented property that checks if the player 
        /// as reached the exit </summary>
        /// <value>True if the player reached the exit or false if not</value>
        internal bool   HasLeft         { get; private set; }

         /// <summary>
        /// Auto-implemented property that checks if the player has moved
        /// </summary>
        /// <value> True if the player changes position otherwise false</value>
        internal bool   Walked          { get; private set; }
        
        /// <summary>
        /// Creates the player's position
        /// 
        /// </summary>
        /// <param name="row">Sets a player row</param>
        /// <param name="column">Sets a player column</param>
        internal Player (int row, int column)
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
        internal void TakeDamage(Enemy enemy)
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
        internal void PickPowerUp(Map[,] map, PowerUp powerUp)
        {
                HP += powerUp.Heal;
                map[powerUp.Row, powerUp.Column].Free("power_up");
                map[powerUp.Row, powerUp.Column].Occupy("player");
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
        internal bool Move(Map[,] map, ConsoleKeyInfo input, Renderer print)
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
        internal void MovementReset()
        {
            Movement = 2;
        }

        /// <summary>
        /// Damages the player and spends one movement
        /// </summary>
        internal void MovementDamage()
        {
            Movement            -= 1;
            HP                  -= 1;
            if (HP < 1) IsAlive = false;
            Walked              = false;
        }

        /// <summary>
        /// Changes the player status to Dead
        /// </summary>
        internal void Die() => IsAlive = false;

        /// <summary>
        /// If the user leaves the game
        /// </summary>
        internal void LeaveGame() => HasLeft = true;

    } 
}