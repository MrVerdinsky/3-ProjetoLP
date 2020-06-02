using System;
using System.Text;
namespace RogueLike
{
    sealed public class Renderer
    {
        
        /// <summary>
        /// Prints the map
        /// </summary>
        /// <param name="map">Map Positions</param>
        /// <param name="rows">Number of Rows</param>
        /// <param name="columns">Number of Columns</param>
        /// <param name="powerUps">List of PowerUps</param>
        /// <param name="enemies">List of enemies</param>
        public void Map(Map[,] map, int rows, int columns, 
                            PowerUp[] powerUps,Enemy[] enemies,
                            Player player, string turn)
        {            
            Console.OutputEncoding = Encoding.UTF8;
            // Prints the meaning of each symbol in the map
            Console.WriteLine();
            Console.Write(" _________________________________________________________\n");
            Console.Write("|\u2654 - Player||\u2749 - Small Power-Up | \u2716 - Obstacle            |\n");
            Console.Write("|\u265F - Minion||\u273E - Medium Power-Up|                         |\n");
            Console.Write("|\u265A - Boss  ||\u2740 - Large Power-Up |                         |\n");
            Console.Write("|                                                         |\n");
            Console.Write($"|Moves Left: {player.Movement}                       Arrow Keys - to move |\n");
            Console.Write($"|Player HP : {player.HP,-5}  - {turn} Turn -      Escape - to leave|\n");
            Console.Write("|_________________________________________________________|\n");

            for (int i = 0; i < rows; i++)
            {   
                // For FIRST row
                
                for (int j = 0; j < columns; j++)
                    Console.Write(" __ ");
                Console.WriteLine();
        
                // For the OTHER rows
                // A magia acontece aqui V
                for (int j = 0; j < columns; j++)
                {
                    // If the square is empty   
                    if (map[i,j].Position.Empty)
                        Console.Write($"|__|");

                    // If the square has a wall   
                    if (map[i,j].Position.HasWall)
                        Console.Write($"|\u2716 |");

                    // If the square has a player
                    if (map[i,j].Position.HasPlayer)
                        Console.Write("|\u2654 |");
                    
                    // Prints all enemies in the list
                    if (map[i,j].Position.HasEnemy)
                        foreach (Enemy enemy in enemies)
                        {
                            if (enemy.damage == 5) Console.Write($"|\u265F |");
                            else Console.Write("|\u265A |");
                        }
                    
                    //Prints all power Ups in the list
                    if (map[i,j].Position.HasPowerUp)
                        foreach (PowerUp powerUp in powerUps)
                            if (powerUp.Picked == false)
                            {
                                if (powerUp.Heal == 4) Console.Write("|\u2749 |");
                                else if (powerUp.Heal == 8) 
                                            Console.Write("|\u273E |");
                                else Console.Write("|\u2740 |"); 
                            }
                    
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the Main Menu Options
        /// </summary>
        public void PrintMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit");
        }
        
        /// <summary>
        /// Prints Game Developers
        /// </summary>
        public void PrintCredits()
        {
            Console.WriteLine("Developed by:");
            Console.WriteLine("Pedro Marques");
            Console.WriteLine("Luiz Santos");
            Console.WriteLine("Gonçalo Vila Verde");
        }

        /// <summary>
        /// Prints Menu Options Input Error
        /// </summary>
        public void PrintInputError()
        {
            Console.WriteLine("Option Unkown");
        }
        
        //Prints a message before exiting the game
        public void PrintExitMsg()
        {
            Console.WriteLine("Thanks for playing.");
        }

        /// <summary>
        /// Prints an error message when theres an error starting the game
        /// </summary>
        public void IntroErrorMessage()
        {
            Console.WriteLine("\nIn order to start the game, please enter" +
                " your choices as it shows in the next example:\n" +
                "dotnet run -p RogueLike -- -r [NUMBER] -c [NUMBER]");
        }

        public void PlayerHP(Player p1)
        {
            Console.WriteLine("\nHP --------- " + p1.HP);
        }

        public void EnemyTurn()
        {
            Console.WriteLine("\nEnemy Turn");
        }

        public void PlayerTurn()
        {
            Console.WriteLine("\nPlayer Turn");
        }

        public void PrintInstructions()
        {
            Console.WriteLine(" ______________________________________________"+
            "___________________________");
            Console.WriteLine("|\t\t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\t\t\t      Rules and Controls\t\t\t  |");
            Console.WriteLine("|_____________________________________________"+
            "____________________________|");
            Console.WriteLine("|\t\t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a You will always start in the closest" + 
            " side of the room, in a random     |\n|   position.\t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a Your objective, if you choose to accept" +
             " it, is to reach the exit gate, |\n|  positioned in the far" +
             " end of the room.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a You can and will have to, move up to 2 " +
            "Squares per turn in 4 directions|\n|  (⭠ ⭢ ⭡ ⭣), but beware moving" + 
            " will deplete your HP by 1 point each time |\n|   you move." + 
            " \t\t\t\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a To move you can use the keys WASD," + 
            " representing ⭡, ⭠, ⭣, ⭢ .\t\t  |");
            Console.WriteLine("|\u2b9a As you move through the room, you will" + 
            " find enemies, either big or     |\n|  small. You can't fight them so you" + 
            " better use your noggin and figure   |\n|  out a way  to go around" + 
            " them.\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a They aren't blind either, as you move," + 
            " they will try to get you, moving|\n|  1 square per turn, chasing"+ 
            " after you.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a If you fail to evade them, it will cost" +
            " ya, losing 5 HP to the little  |\n|  fellas and 10 Hp to big" +  
            " ones.\t\t\t\t\t  |");
            Console.WriteLine("|\u2b9a But don't worry there's a few Power-Ups" + 
            " laying around, with 4, 8 or if |\n|  you're lucky, 16 HP points for" + 
            " you to grab.\t\t\t\t  |");
            Console.WriteLine("|\u2b9a There might be some rubbles in the way" + 
            " so you have to go around them   |\n|  aswell, but I think you're smart" + 
            " enough to do that.\t\t\t  |");
            Console.WriteLine("|\u2b9a Goodluck and Have fun, if you live....\t\t\t\t  |");
            Console.WriteLine("|_____________________________________________"+
            "____________________________|");
        }
    }
}