using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public abstract class Enemy
    {
        public Dictionary<string, Uri> Anims { get; set; }
        public Element Element { get; set; }

        public float MaxHP { get; set; }
        public int HP { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Speed { get; set; }
        public float Size { get; set; }

        public Dictionary<string, EnemyAttack> Attacks { get; set; } = new Dictionary<string, EnemyAttack>();
    }
}
