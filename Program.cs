using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Game
{
    class Program
    {

        /// <summary>
        /// Registers a user and validates they are authorised.
        /// </summary>
        /// <param name="user">Number of the user being registered, either player '1' or '2'.</param>
        /// <returns>Returns the username of authorised player.</returns>
        static string Registration(int user)
        {
            string player;
            Console.WriteLine("Player " + user + " Username:");
            player = Console.ReadLine();
            while (player.ToUpper() != "AARON" && player.ToUpper() != "TONY")
            {
                Console.WriteLine("Unauthorised. Re-enter username.");
                player = Console.ReadLine();
            }
            return player;
        }

        /// <summary>
        /// This is a single round for one player. Includes rolls and calculates total for round.
        /// </summary>
        /// <param name="player">This is the current player's username.</param>
        /// <returns></returns>
        static int Round(string player)
        {
            Random rnd = new Random();

            int roll1 = rnd.Next(1, 6);
            int roll2 = rnd.Next(1, 6);
            int roll3 = rnd.Next(1, 6);
            int rollTotal = roll1 + roll2;

            Console.WriteLine(player + " it's your turn. Press enter to roll...");
            Console.ReadLine();
            Console.WriteLine("Roll 1 = " + roll1);
            Console.WriteLine("Roll 2 = " + roll2);
            Console.ReadLine();

            if (rollTotal % 2 == 0)
            {
                rollTotal += 10;
            }
            else
            {
                rollTotal -= 5;
            }

            if (roll1 == roll2)
            {
                Console.WriteLine("DOUBLE! Press enter to roll again!");
                Console.ReadLine();
                Console.WriteLine("Roll 3 = " + roll3);
                rollTotal += roll3;
            }

            Console.WriteLine(player + ", your round total is " + rollTotal);
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
            return rollTotal;
            
                        

        }

        /// <summary>
        /// Lists the registered players for the game.
        /// </summary>
        /// <param name="playerOne">Player One Name</param>
        /// <param name="playerTwo">Player Two Name</param>
        static void PlayerList(string playerOne, string playerTwo)
        {
            Console.Clear();
            Console.WriteLine("Registered Players:");
            Console.WriteLine("Player 1 = " + playerOne);
            Console.WriteLine("Player 2 = " + playerTwo);
            Console.WriteLine("Press enter to begin...");
            Console.ReadLine();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            string playerOne;
            string playerTwo;

            int playerOneScore = 0;
            int playerTwoScore = 0;

            playerOne = Registration(1);
            playerTwo = Registration(2);

            PlayerList(playerOne, playerTwo);


            for (int i = 1; i < 6; i++ )
            {
                Console.WriteLine("ROUND " + i);
                playerOneScore += Round(playerOne);
                playerTwoScore += Round(playerTwo);
            }
            
            while (playerOneScore == playerTwoScore)
            {
                Console.WriteLine("Tie-Break! Sudden Death!");
                playerOneScore += Round(playerOne);
                playerTwoScore += Round(playerTwo);
            }

            Console.WriteLine(playerOne + " " + playerOneScore);
            Console.WriteLine(playerTwo + " " + playerTwoScore);

            Console.ReadLine();










        }
    }
}
