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
        public Uri Gif { get; set; }

        public int HP { get; set; }
        public int XP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public Player(Uri spawnGif)
        {
            this.Gif = spawnGif;

            this.HP = 100;
            this.XP = 0;
            this.Attack = 1;
            this.Defense = 1;
            this.Speed = 1;
        }

        public int FastAttack()
        {
            int damage = 0;
            damage = 0;

            return damage;
        }
    }
}