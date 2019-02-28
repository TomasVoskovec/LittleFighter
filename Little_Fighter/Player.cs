﻿using System;
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
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public Player()
        {
            this.Anims = new Dictionary<string, Uri>();
            this.Anims.Add("idle", new Uri("img/anim/idle.gif", UriKind.Relative));
            this.Anims.Add("death", new Uri("img/anim/death.gif", UriKind.Relative));
            this.Anims.Add("after_death", new Uri("img/anim/after_death.gif", UriKind.Relative));
            this.Anims.Add("hurt", new Uri("img/anim/hurt.gif", UriKind.Relative));
            this.Anims.Add("def", new Uri("img/anim/def.gif", UriKind.Relative));
            this.Anims.Add("fastAttack", new Uri("img/anim/attack.gif", UriKind.Relative));
            this.Anims.Add("jumpAttack", new Uri("img/anim/jump_attack.gif", UriKind.Relative));

            this.MaxHP = 30;
            this.HP = this.MaxHP;
            this.XP = 0;
            this.Attack = 1;
            this.Defense = 1;
            this.Speed = 1;
        }
    }
}