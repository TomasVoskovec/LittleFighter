using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class PlayerAttack
    {
        public string Name { get; set; }

        public int Strenght { get; set; }
        public int ChanceToMiss { get; set; }

        public List<CriticalEffect> CriticalEffects { get; set; }
        public Element Element { get; set; }
        public Uri Anim { get; set; }

        public PlayerAttack(string name, int strenght, int chanceToMiss, List<CriticalEffect> criticalEffects, Uri anim, Element element)
        {
            this.Name = name;
            this.Strenght = strenght;
            this.ChanceToMiss = chanceToMiss;
            this.CriticalEffects = criticalEffects;
            this.Anim = anim;
            this.Element = element;
        }

        public int Damage(Player player, Enemy enemy)
        {
            Random rn = new Random();

            int missChance = rn.Next(0, 101);

            if (missChance <= 100 - this.ChanceToMiss - enemy.Speed + player.Speed)
            {
                int rndValue = Strenght * (Convert.ToInt32(player.Attack) - Convert.ToInt32(enemy.Defense));

                int damage = rn.Next(rndValue, rndValue + 1);

                if (new Efectivity(this.Element).GetEfectivityValue(enemy.Element) == Efectivity.EfectivityValue.Heigh)
                {
                    damage += rn.Next(1, Strenght);
                }
                else if (new Efectivity(this.Element).GetEfectivityValue(enemy.Element) == Efectivity.EfectivityValue.Low)
                {
                    damage -= rn.Next(1, Strenght);
                }

                if (damage < 0)
                {
                    damage = 0;
                }

                return damage + rn.Next(1, 3);
            }
            else
            {
                return 0;
            }
        }
    }
}
