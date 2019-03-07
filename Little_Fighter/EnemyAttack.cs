using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class EnemyAttack
    {
        public string Name { get; set; }

        public int Strenght { get; set; }
        public int ChanceToMiss { get; set; }

        public List<CriticalEffect> CriticalEffects { get; set; }

        public EnemyAttack(string name, int strenght, int chanceToMiss, List<CriticalEffect> criticalEffects)
        {
            this.Name = name;
            this.Strenght = strenght;
            this.ChanceToMiss = chanceToMiss;
            this.CriticalEffects = criticalEffects;
        }

        public int Damage(Player player, Enemy enemy)
        {
            Random rn = new Random();

            int missChance = rn.Next(0, 100);

            if (missChance >= 100 - this.ChanceToMiss)
            {
                return 0;
            }
            else
            {
                return rn.Next(Strenght * (Convert.ToInt32(enemy.Attack) - player.Defense), (Strenght * 2) * (Convert.ToInt32(enemy.Attack) - player.Defense)) + rn.Next(1,3);
            }
        }
    }
}