using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    class Enemy
    {
        public Dictionary<string, Uri> Anims { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public Enemy()
        {
            this.Anims = new Dictionary<string, Uri>();
            this.Anims.Add("idle", new Uri("img/anim/waiting.gif", UriKind.Relative));
            this.Anims.Add("fastAttack", new Uri("img/anim/attack.gif", UriKind.Relative));

            this.MaxHP = 20;
            this.HP = this.MaxHP;
            this.XP = 0;
            this.Attack = 1;
            this.Defense = 1;
            this.Speed = 1;
        }
    }
}
