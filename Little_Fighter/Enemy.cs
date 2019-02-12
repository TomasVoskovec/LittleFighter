using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class Enemy
    {
        public Dictionary<string, Uri> Anims { get; set; }
        public float MaxHP { get; set; }
        public float HP { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Speed { get; set; }
        public float Size { get; set; }
    }
}
