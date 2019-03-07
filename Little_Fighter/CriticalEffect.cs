using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class CriticalEffect : Effect
    {
        public int Strenght { get; set; }

        public CriticalEffect(string name, int strenght, int chance)
        {
            this.Name = name;
            this.Strenght = strenght;
            this.Chance = chance;
        }

        public int GetDamage()
        {
            Random rn = new Random();

            return rn.Next(1, this.Strenght + 1);
        }

        public override bool isEffect()
        {
            Random rn = new Random();

            if (rn.Next(0, 101) <= this.Chance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
