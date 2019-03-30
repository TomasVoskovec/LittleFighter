using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Player
    {
        // Character animation
        public Dictionary<string, Uri> Anims { get; set; }
        public Dictionary<string, PlayerAttack> Attacks { get; set; }

        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Speed { get; set; } 

        public Player()
        {
            this.Anims = new Dictionary<string, Uri>();
            this.Anims.Add("idle", new Uri("img/anim/idle.gif", UriKind.Relative));
            this.Anims.Add("death", new Uri("img/anim/death.gif", UriKind.Relative));
            this.Anims.Add("after_death", new Uri("img/anim/after_death.gif", UriKind.Relative));
            this.Anims.Add("hurt", new Uri("img/anim/hurt.gif", UriKind.Relative));
            this.Anims.Add("def", new Uri("img/anim/def.gif", UriKind.Relative));

            this.Attacks = new Dictionary<string, PlayerAttack>();
            this.Attacks.Add("Fast Attack", new PlayerAttack("Fast Attack", 1, 25, new List<CriticalEffect>() { new CriticalEffect("Bleading", 1, 100) }, new Uri("img/anim/attack.gif", UriKind.Relative), new Element("Fight", new List<Element> { new Element("Poison") })));
            this.Attacks.Add("Jump Attack", new PlayerAttack("Jump Attack", 2, 75, new List<CriticalEffect> { new CriticalEffect("Bleading", 1, 100) }, new Uri("img/anim/jump_attack.gif", UriKind.Relative), new Element("Fight")));
            
            this.MaxHP = 30;
            this.HP = this.MaxHP;
            this.XP = 0;
            this.Attack = 7;
            this.Defense = 2;
            this.Speed = 1;
        }
    }
}