using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.IO;

namespace Dice_Game
{
    class Program
    {

        /// <summary>
        /// Registers a user and validates they are authorised.
        /// </summary>
        /// <param name="playerNo">Number of the user being registered, either player '1' or '2'.</param>
        /// <returns>Returns the username of authorised player.</returns>
        static string Registration(int playerNo)
        {
            string player;
            Console.WriteLine("Player " + playerNo + " Username:");
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
                if (rollTotal < 0)
                {
                    rollTotal = 0;
                }
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

        /// <summary>
        /// Creates/Updates the Scores.XML with winning player name and score.
        /// </summary>
        /// <param name="player">Winning Player</param>
        /// <param name="score">Winning player's score</param>
        static void RecordHighScores(string player, int score)
        {
            XDocument scoresXML;

            //If the file exists, load it into 'scoresXML', otherwise create it.
            if (File.Exists(@"C:\Users\aaronrobinson\source\repos\Dice_Game\Scores.xml"))
            {
                scoresXML = XDocument.Load(@"C:\Users\aaronrobinson\source\repos\Dice_Game\Scores.xml");
            }
            else
            {
                scoresXML = new XDocument(
                new XElement("scores"));
            }

            //Adds the winning score into the XML file loaded/created above.
            scoresXML.Root.Add(
                    new XElement("entry",
                        new XElement("player", player),
                        new XElement("score", score)));

            scoresXML.Save(@"C:\Users\aaronrobinson\source\repos\Dice_Game\Scores.xml");

            Console.WriteLine("Press Enter to view the winners list!");
            Console.ReadLine();

            //Reads entries from XML and lists on screen.
            var entries = from r in scoresXML.Descendants("entry")
                          orderby r.Element("score").Value descending
                          select new
                          {
                              Player = r.Element("player").Value,
                              Score = r.Element("score").Value
                          };

            int i = 0;

            foreach (var r in entries)
            {
                if (i < 5)
                {
                    Console.WriteLine(i+1 + ". " + r.Player + " " + r.Score);
                    i++;
                }
                
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Main program that calls the procedures.
        /// </summary>
        /// <param name="args">???</param>
        static void Main(string[] args)
        {
            Console.Title = "Dice Game";
            string playerOne;
            string playerTwo;

            int playerOneScore = 0;
            int playerTwoScore = 0;

            playerOne = Registration(1);
            playerTwo = Registration(2);

            PlayerList(playerOne, playerTwo);

            //Runs the round procedure 5 times.
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("ROUND " + i);
                playerOneScore += Round(playerOne);
                playerTwoScore += Round(playerTwo);
            }

            //Tie-break - runs the round procdure indefinitely until a winner is found.
            while (playerOneScore == playerTwoScore)
            {
                Console.WriteLine("Tie-Break! Sudden Death!");
                playerOneScore += Round(playerOne);
                playerTwoScore += Round(playerTwo);
            }

            //Displays totals.
            Console.WriteLine("The Winner is...");

            if (playerOneScore > playerTwoScore)
            {
                Console.WriteLine(playerOne + "! - " + playerOneScore);
                Console.WriteLine();
                Console.WriteLine(playerTwo + ", you came second with " + playerTwoScore);
                RecordHighScores(playerOne, playerOneScore);
            }
            else
            {
                
                Console.WriteLine(playerTwo + "! - " + playerTwoScore);
                Console.WriteLine();
                Console.WriteLine(playerOne + ", you came second with " + playerOneScore);
                RecordHighScores(playerTwo, playerTwoScore);
            }
        }
    }
}
