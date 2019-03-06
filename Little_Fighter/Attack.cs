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

        public bool CriticalEffect { get; set; } = false;

        public int Damage(Player player, Enemy enemy)
        {
            int missChance = rn.Next(0, 100);

            if (missChance >= 100 - player.Speed)
            {
                return 0;
            }
            else
            {
                return rn.Next(2 * Convert.ToInt32(enemy.Attack), 5 * Convert.ToInt32(enemy.Attack));
            }
        }
    }
}