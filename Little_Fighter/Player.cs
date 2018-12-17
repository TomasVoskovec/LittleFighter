using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    class Player
    {
        // Character animation
        public Dictionary<string, Uri> Anims { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public Player()
        {
            this.Anims = new Dictionary<string, Uri>();
            this.Anims.Add("idle", new Uri("img/anim/waiting.gif", UriKind.Relative));
            this.Anims.Add("fastAttack", new Uri("img/anim/attack.gif", UriKind.Relative));

            this.HP = 100;
            this.XP = 0;
            this.Attack = 1;
            this.Defense = 1;
            this.Speed = 1;
        }

        Random rn = new Random();

        // returns value of Fast Attack damage
        public int FastAttack()
        {
            int damage;
            int missChance;

            missChance = rn.Next(100-this.Speed);

            if (missChance >= 75)
            {
                damage = 0;
            }
            else
            {
                damage = rn.Next(1, 5 * this.Attack);
            }

            return damage;
        }

        // returns value of Jump Attack damage
        public int JumpAttack()
        {
            int damage;
            int missChance;

            missChance = rn.Next(100 / this.Speed);

            if (missChance >= 25)
            {
                damage = 0;
            }
            else
            {
                damage = rn.Next(5 * this.Attack, 10 * this.Attack);
            }

            return damage;
        }

        // returns percentage of defended damage
        public int isDefended()
        {
            int chance;
            int def;

            chance = rn.Next(1 * this.Defense, 100);

            if(chance >= 50)
            {
                def = rn.Next(1 * this.Defense, 100);
            }
            else
            {
                def = 0;
            }

            return def;
        }
    }
}