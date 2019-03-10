using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    class Bat : Enemy
    {
        public Bat(int lvl = 1)
        {
            this.Anims = new Dictionary<string, Uri>();
            this.Anims.Add("idle", new Uri("img/anim/bat_idle.gif", UriKind.Relative));
            this.Anims.Add("hurt", new Uri("img/anim/bat_hurt.gif", UriKind.Relative));

            this.MaxHP = 20 + (20 / 10 * lvl);
            this.HP = Convert.ToInt32(this.MaxHP);
            this.Attack = 4 + (20 * lvl / 100);
            this.Defense = 1 + (20 * lvl / 100);
            this.Speed = 30 + (20 * lvl / 100);
            this.Size = 1 + (20 * lvl / 100);

            this.Attacks.Add("Bite", new EnemyAttack("Bite", 1, 25, new List<CriticalEffect> { new CriticalEffect("Poison", 2, 100) }, new Uri("img/anim/bat_attack.gif", UriKind.Relative)));
        }
    }
}
