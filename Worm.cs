using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectWorms
{
    class Worm
    {
    public string name;
    private int health;
    private int armor;
    private int x;
    private int y;
    private bool currentTurn = false;
    private bool weaponsAdded = false;
    private bool hasMelee = false;
    public Weapons currentWeapon;
    public List<Weapons> weapons;

    public Worm(string nickname)
    {
        Random random = new Random();
        this.name = nickname;
        this.health = 100;
        this.armor = 100;
        this.x = random.Next(-250, 250);
        this.y = random.Next(1, 40);
        this.weapons = new List<Weapons>();
        this.currentWeapon = null;

    }

       ~Worm() 
        {
            
        }
        

    public int X
    {
        get { return this.x; }
        set { this.x = value; }
    }
    public int Y
    {
        get { return this.y; }
        set { this.y = value; }
    }
    public int Health
    {
        get { return this.health; }
        set
        {
            if (this.health < 0) { this.health = 0; return; }
            if (this.health > 100) { this.health = 100; return; }
            this.health = value;
        }
    }
    public int Armor
    {
        get { return this.armor; }
        set
        {
            if (this.armor < 0) { this.armor = 0; return; }
            if (this.armor > 100) { this.armor = 100; return; }
            this.armor = value;
        }
    }
    public int Distance(Worm p2) // Calculates the distance between the 2 players
    {
        int xx = this.X - p2.x;
        int yy = this.Y - p2.y;
        double temp = Math.Sqrt(xx * xx + yy * yy);

        int dist = (int)Math.Round(temp, 0);
        return dist;
    }
    public int explosionDistance(int x1, int y1, int x2, int y2) // Calculates the distance between the explosions and the players
    {
        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        int dist = (int)Math.Round(distance, 0);

        return dist;
    }
    public void addWeapons() // Adds weapons to the players
    {
        bool hasMelee = false;
        List<Weapons> allWeapons = new List<Weapons>();
        allWeapons.Add(new Bazooka("Rocket Launcher", 3, "10-50", Range.Long));
        allWeapons.Add(new Shotgun("Shotgun", 2, "1-50", Range.ShortMedium));
        allWeapons.Add(new AirStrike("AirStrike", 1, "15-100", Range.Insane));
        allWeapons.Add(new Sniper("Sniper", 3, "20-80", Range.Long));
        allWeapons.Add(new FirePunch("FirePunch", 0, "40", Range.Melee));
        allWeapons.Add(new Pistol("Pistol", 3, "1-10", Range.Short));
        allWeapons.Add(new Bat("Bat", 0, "20", Range.Melee));
         

        Random random = new Random();
        for (int i = 0; i < 3; i++)
        {
            int ind = random.Next(allWeapons.Count);
            var temp = allWeapons[ind];
            if (allWeapons[ind].range == Range.Melee) { hasMelee = true; }
            this.weapons.Add(allWeapons[ind]);
            allWeapons.RemoveAt(ind);

            
        }

        if(this.weapons.Count == 3 && hasMelee == false)
            {
                this.weapons.Add(new Bat("Bat", 0, "20", Range.Melee));
            }else if(this.weapons.Count == 3 && hasMelee == true)
            {
                int ind = random.Next(allWeapons.Count);
                var temp = allWeapons[ind];
                this.weapons.Add(allWeapons[ind]);
                allWeapons.RemoveAt(ind);
            }

        // Code Test
        //this.weapons.Add(new Bazooka("Rocket Launcher", 3, "10-50", Range.Long));
        }

    public void Turn(List<Worm> players, int round) // Game logic
    {

        bool validInput = false;
        int activePlayer = -1;
        int attackInd = -1;
        int amount = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].name == this.name)
            {
                activePlayer = i;
            }
        }

        
        Console.WriteLine("Press F if you want to check your stats, otherwise press any other key");
        ConsoleKeyInfo stats = Console.ReadKey();
        if (stats.KeyChar == 'F' || stats.KeyChar == 'f')
        {
            Console.Clear();
            Worm current = null;
            foreach (Worm player in players)
            {
                player.currentTurn = player == this;
                if (player.currentTurn == true)
                {
                    player.Stats();
                }
            }
            }
            else
            {
                Console.Clear();
            }
        

        Console.WriteLine("Do you want to move or to check the stats?");
        Console.WriteLine("l - left | r - right | n - Don't Move");
        Thread.Sleep(2000);
        ConsoleKeyInfo key = Console.ReadKey();
        Console.Clear();

        while (!validInput)
        {
            if (key.KeyChar == 'l' || key.KeyChar == 'L')
            {
                Console.WriteLine("How far do you want to move left? (Max 25)");
                try
                {
                    amount = int.Parse(Console.ReadLine());
                    if (amount >= 0 && amount <= 25)
                    {

                        if (this.X - amount >= -250)
                        {
                            validInput = true;
                            Console.Clear();
                            Console.WriteLine("{0} moved to the left {1}m", this.name, amount);
                            Console.WriteLine("New Coordinates X: {0}   Y: {1}", this.X, this.Y);
                        }
                        else
                        {
                            Console.WriteLine("You reached the border of the map!");
                        }

                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Please put in a number (max 25):");
                }

            }
            else if (key.KeyChar == 'r' || key.KeyChar == 'R')
            {
                Console.WriteLine("How far do you want to move right? (Max 25)");
                try
                {
                    amount = int.Parse(Console.ReadLine());
                    if (amount >= 0 && amount <= 25)
                    {
                        if (this.X + amount <= 250)
                        {
                            validInput = true;
                            Console.Clear();
                            Console.WriteLine("{0} moved to the right {1}m", this.name, amount);
                            Console.WriteLine("New Coordinates X: {0}   Y: {1}", this.X, this.Y);
                        }
                        else
                        {
                            Console.WriteLine("You reached the border of the map!");
                        }
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Please put in a number (max 25):");
                }
            }
            else if (key.KeyChar == 'n' || key.KeyChar == 'N')
            {
                validInput = true;
            }

        }

        Console.WriteLine("========= Round {0} ==========", round);
        Console.WriteLine();
        Console.WriteLine("{0}'s Turn: ", this.name);
        Console.WriteLine();
        Console.WriteLine("Available Players: ");
        for (int i = 0; i < players.Count; i++)
        {
            int distance;
            if (players[i].name != this.name)
            {

                distance = Distance(players[i]);
                Console.WriteLine("{0}: {1}     Health: {2}     Armor: {3}  Distance: {4}m", i + 1, players[i].name, players[i].Health, players[i].Armor, distance);

            }
        }

        int target = 0;

        do
        {
            Console.WriteLine("Select who you want to attack: ");
            try
            {
                target = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a number");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please enter a number between 1 and {0}", players.Count);
            }
        } while (target < 1 || target > players.Count || target == attackInd + 1);

        Console.Clear();


        Worm attackingPlayer = players[activePlayer];
        int playerDistance = Distance(players[target - 1]);
        Worm attackedPlayer = players[target-1];

        bool outOfAmmo = false;

        Info(this.name, round);
        Console.WriteLine("Select which weapon you want to use");
            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine("{0}. Weapon: {1}     Ammo: {2}   Range: {3}  Damage: {4}", i + 1, weapons[i].name, weapons[i].ammo, weapons[i].range, weapons[i].damageString);
            }

           int ind = int.Parse(Console.ReadLine()) - 1;
        Console.Clear();

        if (ind >=0 && ind < this.weapons.Count)
        {
            this.currentWeapon = this.weapons[ind]; // set the current weapon
            if(this.weapons[ind].ammo != 0)
            {
                outOfAmmo = false;
                Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                Console.WriteLine();
                this.weapons[ind].ammo--;
            }
            else
            {
                outOfAmmo = true;
                Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                Console.WriteLine();
            }
            

        }

    }

    public void Info(string name, int round)
    {
        Console.WriteLine("========= Round {0} ==========", round);
        Console.WriteLine();
        Console.WriteLine("{0}'s turn:", name);
        Console.WriteLine();
    }

        Bat bat = new Bat();
        Bazooka rocketLauncher = new Bazooka();
        Shotgun shotgun = new Shotgun();
        Sniper sniper = new Sniper();
        FirePunch punch = new FirePunch();
        Pistol pistol = new Pistol();
        AirStrike missiles = new AirStrike();

        public void Attack(Worm attackedPlayer, int playerDistance, Worm attackingPlayer, bool ammo, List<Worm> players) 
    {
        if (this.currentWeapon.name == "Bat")
        {
            
            int dmg = bat.Use(playerDistance);
            if (dmg == 0)
            {
                Console.WriteLine("{0} For some reason Swung the bat at {1} who was {2} metres away, way out of range, {0} did 0 damage", attackingPlayer.name
                    ,attackedPlayer.name, playerDistance);
            }
            else
            {
                Console.WriteLine("{0} Swung the bat at {1} who was too close for comfort and did {2} damage", attackingPlayer.name, attackedPlayer.name, dmg);
            }
            attackedPlayer.takeDamageStats(dmg, attackedPlayer);
            attackedPlayer.Stats();
        }
        if(this.currentWeapon.name == "Rocket Launcher")
        {
            if (!ammo)
            {
                int dmg = rocketLauncher.Use(playerDistance);
                    // Code Test
                    //dmg = 250;
                if (dmg == 0)
                {
                    Console.WriteLine("{0} Tried to attack {1} using Rocket Launcher but did {2} damage because it cannot reach {1} ", attackingPlayer.name, attackedPlayer.name, dmg);
                }
                else if(dmg <= -20)
                {
                    Console.WriteLine("{0} decided to blow up himself together with {1} and did {2} damage to himself and {3} damage to {1}", attackingPlayer.name, attackedPlayer.name, -dmg, -dmg/2);
                    attackingPlayer.takeDamageStats(-dmg, attackedPlayer);
                    attackedPlayer.takeDamageStats(-dmg / 2, attackedPlayer);
                }
                if (dmg >= 10 && dmg <= 21)
                {
                    Console.WriteLine("{0} used a rocket Launcher to attack {1}, but did only {2} damage, because {1} was too close", attackingPlayer.name, attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }else if(dmg >= 25 && dmg < 36)
                {
                    Console.WriteLine("{0} used a rocket Launcher to attack {1}, did {2} damage, as {1} is a bit too close", attackingPlayer.name, attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }
                else if (dmg >= 36 && dmg <= 50)
                {
                    Console.WriteLine("{0} used a rocket Launcher to attack {1} and did {2} damage. {1} was at perfect range", attackingPlayer.name, attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                    }
                    // Code Test
                    //else
                    //{
                    //    Console.WriteLine(dmg);
                    //    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                    //}

            }
            else
            {
                Console.WriteLine("Your Rocket  Launcher is out of ammo!");

                Console.WriteLine("Select which weapon you want to use");
                for (int i = 0; i < weapons.Count; i++)
                {
                    Console.WriteLine("{0}. Weapon: {1}     Ammo: {2}   Range: {3}  Damage: {4}", i + 1, weapons[i].name, weapons[i].ammo, weapons[i].range, weapons[i].damageString);
                }

                int ind = int.Parse(Console.ReadLine()) - 1;
                Console.Clear();

                if (ind >= 0 && ind < this.weapons.Count)
                {
                    this.currentWeapon = this.weapons[ind]; // set the current weapon
                    if (this.weapons[ind].ammo != 0)
                    {
                        bool outOfAmmo = false;
                        Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                        this.weapons[ind].ammo--;
                    }
                    else
                    {
                        bool outOfAmmo = true;
                        Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                    }


                }

            }



        }
        if(this.currentWeapon.name == "Shotgun")
        {
            if (!ammo)
            {
                int dmg = shotgun.Use(playerDistance);
                if(dmg == 0)
                {
                    Console.WriteLine("{0} tried using a shotgun to attack {1} who is very far away, because of that he did {2} damage", attackingPlayer.name, attackedPlayer.name, dmg);
                }else if(dmg >= 1 && dmg < 6)
                {
                    Console.WriteLine("{0} tried using a shotgun to attack {1} who is pretty far away, but still did a bit of damage: {2}", attackingPlayer.name, attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }else if(dmg >= 20 && dmg < 31)
                {
                    Console.WriteLine("{0} used a Shotgun to attack {1} who was {3} metres away, because of that he did {2} damage", attackingPlayer.name, attackedPlayer.name, dmg, playerDistance);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }else if(dmg >= 31 && dmg < 51)
                {
                    Console.WriteLine("{0} used a Shotgun to attack {1}, lucky for him {1} was insanely close so he did {2} damage", attackingPlayer.name, attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }
            }
            else
            {
                Console.WriteLine("Your Shotgun is out of ammo!");

                Console.WriteLine("Select which weapon you want to use");
                for (int i = 0; i < weapons.Count; i++)
                {
                    Console.WriteLine("{0}. Weapon: {1}     Ammo: {2}   Range: {3}  Damage: {4}", i + 1, weapons[i].name, weapons[i].ammo, weapons[i].range, weapons[i].damageString);

                    int ind = int.Parse(Console.ReadLine()) - 1;
                    Console.Clear();

                    if (ind >= 0 && ind < this.weapons.Count)
                    {
                        this.currentWeapon = this.weapons[ind]; // set the current weapon
                        if (this.weapons[ind].ammo != 0)
                        {
                            bool outOfAmmo = false;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                            this.weapons[ind].ammo--;
                        }
                        else
                        {
                            bool outOfAmmo = true;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                        }


                    }
                }
            }
        }
        if(this.currentWeapon.name == "Sniper")
        {
            if (!ammo)
            {
                int dmg = sniper.Use(playerDistance);


                if(dmg == 0)
                {
                    Console.WriteLine("{0} tried to attack {1} using a Sniper rifle in Melee range, that's not smart, did only {2} damage", attackingPlayer.name, attackedPlayer.name, dmg);
                }
                else if (dmg >= 21 && dmg < 81 && playerDistance >= 400)
                {
                    Console.WriteLine("{0} tried to snipe {1} who was insanely far {2} metres away, he did {3} damage", attackingPlayer.name, attackedPlayer.name, playerDistance, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }
                else if(dmg >= 20 && dmg < 31 ){
                    Console.WriteLine("{0} used a Sniper rifle to attack {1}, but because {1} was only {2} metres away, he did {3} damage", attackingPlayer.name, attackedPlayer.name,playerDistance, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }else if(dmg >= 31 && dmg < 61)
                {
                    Console.WriteLine("{0} used a Sniper Rifle to attack {1}, while {1} was a little close, {0} did {2} damage", attackingPlayer.name, attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }else if(dmg >= 61 && dmg < 81)
                {
                    Console.WriteLine("{0} sniped {1} almost throug the whole map and did an amazing {2} damage", attackingPlayer.name, attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }
            }
            else
            {
                Console.WriteLine("Your Sniper Rifle is out of ammo!");

                Console.WriteLine("Select which weapon you want to use");
                for (int i = 0; i < weapons.Count; i++)
                {
                    Console.WriteLine("{0}. Weapon: {1}     Ammo: {2}   Range: {3}  Damage: {4}", i + 1, weapons[i].name, weapons[i].ammo, weapons[i].range, weapons[i].damageString);

                    int ind = int.Parse(Console.ReadLine()) - 1;
                    Console.Clear();

                    if (ind >= 0 && ind < this.weapons.Count)
                    {
                        this.currentWeapon = this.weapons[ind]; // set the current weapon
                        if (this.weapons[ind].ammo != 0)
                        {
                            bool outOfAmmo = false;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                            this.weapons[ind].ammo--;
                        }
                        else
                        {
                            bool outOfAmmo = true;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                        }


                    }
                }
            }
        }
        if(this.currentWeapon.name == "FirePunch")
        {
            int dmg = punch.Use(playerDistance);

            if(dmg == 0)
            {
                Console.WriteLine("{0} Tried to attack {1} with FirePunch when he was {2} metres away, why would you do that... you did 0 damage", attackingPlayer.name,
                    attackedPlayer.name, playerDistance);
            }
            else
            {
                Console.WriteLine("{0} FirePunched {1} and did insane {2} damage", attackingPlayer.name, attackedPlayer.name, dmg);
                attackedPlayer.takeDamageStats(dmg, attackedPlayer);
            }
        }
        if(this.currentWeapon.name == "Pistol")
        {
            if (!ammo)
            {
                int dmg = pistol.Use(playerDistance);

                if(dmg == 0)
                {
                    Console.WriteLine("Why would you {0} choose a Pistol to shoot {1} from {2} metres? That's the dumbest idea ever, you did 0 damage", attackingPlayer.name,
                        attackedPlayer.name, playerDistance);
                }
                else if (dmg > 0 && dmg <= 4)
                {
                    Console.WriteLine("{0} chose a pistol to attack {1} with, even though {1} is {2} metres away, atleast you did some damage: {3}", attackingPlayer.name,
                        attackedPlayer.name, playerDistance, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }else if(dmg > 4 && dmg <= 7)
                {
                    Console.WriteLine("{0} attacked {1} with a Pistol and did {2} damage", attackingPlayer.name,
                        attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }
                else
                {
                    Console.WriteLine("{0} attacked {1} with a Pistol in very close range and from did {2} damage", attackingPlayer.name,
                        attackedPlayer.name, dmg);
                    attackedPlayer.takeDamageStats(dmg, attackedPlayer);
                }
            }
            else
            {
                Console.WriteLine("Your pistol is out of ammo! ");

                Console.WriteLine("Select which weapon you want to use");
                for (int i = 0; i < weapons.Count; i++)
                {
                    Console.WriteLine("{0}. Weapon: {1}     Ammo: {2}   Range: {3}  Damage: {4}", i + 1, weapons[i].name, weapons[i].ammo, weapons[i].range, weapons[i].damageString);

                    int ind = int.Parse(Console.ReadLine()) - 1;
                    Console.Clear();

                    if (ind >= 0 && ind < this.weapons.Count)
                    {
                        this.currentWeapon = this.weapons[ind]; // set the current weapon
                        if (this.weapons[ind].ammo != 0)
                        {
                            bool outOfAmmo = false;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                            this.weapons[ind].ammo--;
                        }
                        else
                        {
                            bool outOfAmmo = true;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                        }


                    }
                }
            }

        }
        if(this.currentWeapon.name == "AirStrike")
        {
            if (!ammo)
            {

               
                Console.WriteLine("Input Main Point x coordinate (-250; 250): ");
                int x = int.Parse(Console.ReadLine());
                Console.WriteLine("Input Main Point y coordinate (1; 40): ");
                int y = int.Parse(Console.ReadLine());

                for (int i = 0; i < players.Count; i++)
                {
                    Console.WriteLine("{0} Launched an AirStrike at X: {1}  Y: {2}", attackingPlayer.name, x, y);
                    missiles.dropBombs(x, y);

                    int firstExplosion = explosionDistance(x, players[i].X, y, players[i].Y);
                    int fDamage = 0;
                    int secondExplosion = explosionDistance(x + 25, players[i].X, y, players[i].Y);
                    int sDamage = 0;
                    int thirdExplosion = explosionDistance(x - 25, players[i].X, y, players[i].Y);
                    int tDamage = 0;
                    if (firstExplosion <= 15)
                    {
                        fDamage = missiles.Use(firstExplosion);
                    }
                    else if (secondExplosion <= 15)
                    {
                        sDamage = missiles.Use(secondExplosion);
                    }
                    else if (thirdExplosion <= 15)
                    {
                        tDamage = missiles.Use(thirdExplosion);
                    }


                    int dmg = Math.Max(fDamage, Math.Max(sDamage, tDamage));
                    int minDistance = Math.Min(firstExplosion, Math.Min(secondExplosion, thirdExplosion));

                    if (dmg == 0)
                    {
                        Console.WriteLine("{0} didn't get hit by a missile, distance to the closest missile: {1}", players[i].name, minDistance);
                    }
                    else if (dmg > 15 && dmg <= 25)
                    {
                        Console.WriteLine("{0} Missile touched {1} a bit and did {2} damage", attackingPlayer.name, players[i].name, dmg);
                        players[i].takeDamage(dmg);
                    }
                    else if (dmg > 25 && dmg <= 35)
                    {
                        Console.WriteLine("{0} Missile, hit {1} and did {2}", attackingPlayer.name, players[i].name, dmg);
                        players[i].takeDamage(dmg);
                    }
                    else if (dmg > 35 && dmg <= 45)
                    {
                        Console.WriteLine("{0} Missile, almost hit directly {1} in the head and did {2} damage", attackingPlayer.name, players[i].name, dmg);
                        players[i].takeDamage(dmg);
                    }
                    else if (dmg > 45)
                    {
                        Console.WriteLine("{0} Missile, hit directly {1} in the head and did {2} damage... OUCH", attackingPlayer.name, players[i].name, dmg);
                        players[i].takeDamage(dmg);
                    }


                    ConsoleKeyInfo temp = Console.ReadKey();
                    Console.Clear();
                }

            }
            else
            {
                Console.WriteLine("You don't have any AirStrikes to call");

                Console.WriteLine("Select which weapon you want to use");
                    for (int i = 0; i < weapons.Count; i++)
                    {
                        Console.WriteLine("{0}. Weapon: {1}     Ammo: {2}   Range: {3}  Damage: {4}", i + 1, weapons[i].name, weapons[i].ammo, weapons[i].range, weapons[i].damageString);

                    }
                    int ind = int.Parse(Console.ReadLine()) - 1;
                    Console.Clear();

                    if (ind >= 0 && ind < this.weapons.Count)
                    {
                        this.currentWeapon = this.weapons[ind]; // set the current weapon
                        if (this.weapons[ind].ammo != 0)
                        {
                            bool outOfAmmo = false;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                            this.weapons[ind].ammo--;
                        }
                        else
                        {
                            bool outOfAmmo = true;
                            Attack(attackedPlayer, playerDistance, attackingPlayer, outOfAmmo, players);
                        }


                    }
                
            }

            

            
        }
    }


    public void takeDamageStats(int damage, Worm attackedPlayer) // does damage to the player
    {
        if (this.armor >= damage)
        {
            this.armor -= damage;
            Console.WriteLine("{0} took {1} damage          Health: {2} Armor: {3}", this.name, damage,this.health, this.armor);
        }
        else
        {
            damage -= this.armor;
            this.armor = 0;
            this.health -= damage;
            if(this.health <= 0)
                {
                    Console.WriteLine("{0} took {1} damage and died", this.name, damage);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("{0} took {1} damage          Health: {2} Armor: {3}", this.name, damage, this.health, this.armor);
                    Console.WriteLine("");
                }  
        }
    }

    public void takeDamage(int damage)
    {
        if (this.armor >= damage)
        {
            this.armor -= damage;
        }
        else
        {
            damage -= this.armor;
            this.armor = 0;
            this.health -= damage;
            Console.WriteLine("");
        }

        if (this.health < 0)
        {
            Console.WriteLine("{0} had died!", this.name);

        }
    }

        public void Death(Worm attackedPlayer)
        {
            //~attackedPlayer{

            //}
        }

    public void Stats()
    {
        if (this.Health < 0)
        {
            Console.WriteLine("{0} is dead", this.name);
        }
        else
        {
            Console.WriteLine("{0} Stats: ", this.name);
            Console.WriteLine("Health: {0}, Armor: {1}", this.Health, this.Armor);
            Console.WriteLine("Coordinated: X: {0}  Y: {1}", this.X, this.Y);
        }

    }
}
}
