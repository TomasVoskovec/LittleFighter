using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class Effect
    {
        public string Name { get; set; }
        public int Chance { get; set; }

        public abstract bool isEffect();
    }
}
