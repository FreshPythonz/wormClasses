using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectWorms
{
    class GameStart
    {
        private List<Worm> worms = new List<Worm>();
        private bool Shuffled = false;


        public GameStart(List<Worm> worms)
        {
            this.worms = worms;
        }


        public void Start(int rounds, List<Worm> worms)
        {

            // Shuffles the players at the start of the match
            if (!Shuffled)
            {
                Shuffle();
                Shuffled = true;
            }
            int round = 1;
            int turnCount = 0;


            // Game runs until only 1 worm is left alive or rounds end
            while (this.worms.Count > 1 && round <= rounds)
            {

                if (round % 5 == 0) // Every 5 rounds every player gets new weapons
                {
                    foreach (Worm worm in this.worms)
                    {
                        worm.weapons.Clear();
                        worm.addWeapons();
                    }
                }
                Console.Clear();


                for (int i = 0; i < this.worms.Count; i++)
                {
                    Console.WriteLine("========= Round {0} ==========", round);
                    Console.WriteLine();

                    if (this.worms.Count == 1)
                    {
                        Console.WriteLine("{0} Won!", this.worms[i]);
                    }


                    Worm worm = this.worms[i];
                    Console.WriteLine("{0}'s turn:", worm.name);
                    Console.WriteLine();
                    worm.Turn(this.worms, round);
                    turnCount++;

                    for(int x = this.worms.Count - 1; x >= 0; x--)
                    {
                        if (this.worms[x].Health <= 0)
                        {
                            this.worms.RemoveAt(x);
                        }
                    }
                }

                round++;
            }

            Winner(worms);
        }

        public void Winner(List<Worm> players) // Nustato laimetoja
        {
            int mostHealth = 0;
            int winnerIndex = -1;
            if (players.Count == 1)
            {
                Console.WriteLine("Worm: {0} won the game!", players[0].name);
                players[0].Stats();
            }
            else
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (i == 0)
                    {
                        mostHealth = players[i].Health + players[i].Armor;
                        winnerIndex = i;
                    }
                    else
                    {
                        int tempHealth = players[i].Health + players[i].Armor;
                        if (tempHealth > mostHealth)
                        {
                            mostHealth = tempHealth;
                            winnerIndex = i;
                        }
                        else if (tempHealth == mostHealth)
                        {
                            Console.WriteLine("The game ended in a tie!");
                        }

                    }
                }

                Console.WriteLine("{0} Won this game!", players[winnerIndex].name);
                players[winnerIndex].Stats();
            }
        }


        private void Shuffle() // Ismaiso visus zaidejus
        {
            Random rand = new Random();
            for (int i = this.worms.Count - 1; i > 0; i--)
            {
                int x = rand.Next(i + 1);
                var temp = this.worms[i];
                this.worms[i] = this.worms[x];
                this.worms[x] = temp;
            }
        }
    }
}
