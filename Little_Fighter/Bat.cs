using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    class Bat : Enemy
    {
        public Bat()
        {
            this.Anims = new Dictionary<string, Uri>();
            this.Anims.Add("idle", new Uri("img/anim/bat_idle.gif", UriKind.Relative));

            this.MaxHP = 20;
            this.HP = this.MaxHP;
            this.Attack = 1;
            this.Defense = 1;
            this.Speed = 1;
            this.Size = 1;
        }
    }
}
