
using projectWorms;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace projectWorms
{
    class ProjectWorms
    {
        static void Main(string[] args)
        {

            // Let's user input how many players will play the game
            int number = playerAmount();


            List<Worm> worms = new List<Worm>();

            // Adds the players to the game
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Enter {0} user name: ", i + 1);
                string name = Console.ReadLine();
                Worm worm = new Worm(name);
                worms.Add(worm);
            }


            GameStart game = new GameStart(worms);
            Console.Clear();


            int roundNum = RoundNumber();

            Console.Clear();

            foreach (Worm worm in worms) // Adds 3 weapons + a Bat to each player
            {
                worm.addWeapons();
            }

            game.Start(roundNum, worms);

        }

        public static int playerAmount() // User inputs how many players will play
        {
            int amount = 0;
            Console.WriteLine("Enter the number of users that will play: (2-10):");
            while (true)
            {
                try
                {
                    amount = int.Parse(Console.ReadLine());
                    if (amount < 2 || amount > 10)
                    {
                        Console.WriteLine("Please put in a number (2-10)");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please input a number (2-10)");
                    playerAmount();
                }

            }

            return amount;
        }

        public static int RoundNumber() // User inputs how many rounds they will play
        {
            int temp = 0;
            Console.WriteLine("Choose how many rounds you want to play (Must be between 5 and 20)");


            while (true)
            {
                try
                {
                    temp = int.Parse(Console.ReadLine());
                    if (temp < 1 || temp > 20)
                    {
                        Console.WriteLine("Please enter a number (5-20)");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number (5-20)");
                }
            }

            return temp;
        }
    }

    public enum Range
    {
        Melee,
        Short,
        ShortMedium,
        Medium,
        MediumLong,
        Long,
        Insane
    }

}