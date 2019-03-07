using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public abstract class Effect
    {
        public string Name { get; set; }
        public int Chance { get; set; }

        public abstract bool isEffect();
    }
}
