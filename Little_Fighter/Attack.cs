using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class Attack
    {
        Random rn = new Random();

        public int Strenght { get; set; }
        public int ChanceToMiss { get; set; }
        
        public bool CriticalEffect { get; set; } = false;

        public Attack(int strenght, int chanceToMiss, bool criticalEffect = false)
        {
            this.Strenght = strenght;
            this.ChanceToMiss = chanceToMiss;
            this.CriticalEffect = criticalEffect;
        }

        public int Damage(Player player, Enemy enemy)
        {
            int missChance = rn.Next(0, 100);

            if (missChance >= 100 - missChance)
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