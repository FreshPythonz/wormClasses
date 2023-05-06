using projectWorms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectWorms
{
    public abstract class Weapons
    {
        internal string name;
        internal int ammo;
        internal int damage;
        internal Range range;
        internal string damageString;

        public Weapons(string name, int ammo, int damage, Range range)
        {
            this.name = name;
            this.ammo = ammo;
            this.damage = damage;
            this.range = range;
        }

        public Weapons(string name, int ammo, string damageString, Range range)
        {
            this.name = name;
            this.ammo = ammo;
            this.damageString = damageString;
            this.range = range;
        }

        public abstract int Use(int distance);

    }

    public class Bat : Weapons
    {
        public Bat() : base("Bat", 0, 20, Range.Melee) { }

        public Bat(string name, int ammo, string damageString, Range range) : base(name, ammo, damageString, range) { }

        public override int Use(int distance)
        {
            if (distance > 18) return 0;
            else return this.damage;


        }
    }

    public class Bazooka : Weapons
    {
        public Bazooka() : base("Rocket Launcher", 3, 50, Range.Long) { }

        public Bazooka(string name, int ammo, string damageString, Range range) : base(name, ammo, damageString, range) { }

        public override int Use(int distance) // Decides how much damage the weapon does based on distannce
        {
            Random random = new Random();
            if (distance <= 18)
            {
                this.damage = random.Next(-20, -50);
            }
            else if (distance > 18 && distance <= 100)
            {
                this.damage = random.Next(10, 21);
            }
            else if (distance > 100 && distance <= 250)
            {
                this.damage = random.Next(25, 36);
            }
            else if (distance > 250 && distance <= 400)
            {
                this.damage = random.Next(35, 50);
            }
            else
            {
                this.damage = 0;
            }

            return this.damage;
        }
    }

    public class Shotgun : Weapons
    {
        public Shotgun() : base("Shotgun", 2, 25, Range.ShortMedium) { }

        public Shotgun(string name, int ammo, string damageString, Range range) : base(name, ammo, damageString, range) { }

        public override int Use(int distance) // Decides how much damage the weapon does based on distannce
        {
            Random random = new Random();
            if (distance <= 18)
            {
                this.damage = random.Next(30, 51);
            }
            else if (distance > 18 && distance <= 250)
            {
                this.damage = random.Next(20, 31);
            }
            else if (distance > 250 && distance <= 400)
            {
                this.damage = random.Next(1, 6);
            }
            else
            {
                this.damage = 0;
            }
            return this.damage;
        }
    }

    public class AirStrike : Weapons
    {
        public AirStrike() : base("AirStrike", 1, 30, Range.Insane) { }

        public AirStrike(string name, int ammo, string damageString, Range range) : base(name, ammo, damageString, range) { }

        public void dropBombs(int x, int y) // "Drops" the bomb on specific coordinates
        {
            Explosion(x, y);
            Explosion(x - 25, y);
            Explosion(x + 25, y);
        }

        private void Explosion(int x, int y)
        {

            Console.WriteLine("A Missile just exploded at: X: {0}   Y: {1}", x, y);

        }


        public override int Use(int distance) // Decides how much damage the weapon does based on distannce
        {
            Random random = new Random();
            if (distance == 0)
            {
                this.damage = random.Next(45, 101);
            }
            else if (distance <= 5 && distance > 0)
            {
                this.damage = random.Next(35, 46);
            }
            else if (distance <= 10 && distance > 5)
            {
                this.damage = random.Next(25, 36);
            }
            else if (distance <= 15 && distance > 10)
            {
                this.damage = random.Next(15, 26);
            }
            else
            {
                this.damage = 0;
            }

            return this.damage;
        }
    }

    public class Sniper : Weapons
    {
        public Sniper() : base("Sniper", 3, 80, Range.Long) { }

        public Sniper(string name, int ammo, string damageString, Range range) : base(name, ammo, damageString, range) { }

        public override int Use(int distance) // Decides how much damage the weapon does based on distannce
        {
            Random random = new Random();

            if (distance <= 18)
            {
                this.damage = 0;
            }
            else if (distance > 18 && distance <= 100)
            {
                this.damage = random.Next(20, 31);
            }
            else if (distance > 100 && distance <= 250)
            {
                this.damage = random.Next(31, 61);
            }
            else if (distance > 250 && distance <= 400)
            {
                this.damage = random.Next(61, 81);
            }
            else if (distance > 400)
            {
                this.damage = random.Next(20, 81);
            }

            return this.damage;
        }
    }

    public class FirePunch : Weapons
    {
        public FirePunch() : base("FirePunch", 0, 40, Range.Melee) { }

        public FirePunch(string name, int ammo, string damageString, Range range) : base(name, ammo, damageString, range) { }

        public override int Use(int distance) // Decides how much damage the weapon does based on distannce
        {
            if (distance > 18)
            {
                this.damage = 0;
                return this.damage;
            }
            else
            {
                return this.damage;
            }
        }
    }

    public class Pistol : Weapons
    {
        public Pistol() : base("Pistol", 3, 5, Range.Short) { }

        public Pistol(string name, int ammo, string damageString, Range range) : base(name, ammo, damageString, range) { }

        public override int Use(int distance) // Decides how much damage the weapon does based on distannce
        {
            Random random = new Random();
            if (distance <= 18)
            {
                this.damage = random.Next(7, 11);
            }
            else if (distance > 18 && distance <= 100)
            {
                this.damage = random.Next(3, 8);
            }
            else if (distance > 100 && distance <= 250)
            {
                this.damage = random.Next(1, 4);
            }
            else if (distance > 250)
            {
                this.damage = 0;
            }

            return this.damage;
        }
    }

}
