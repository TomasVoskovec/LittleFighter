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
        public GameData Data { get; set; }

        public Attack(Player player, Enemy enemy, bool criticalEffect)
        {
            this.Data = new GameData(player, enemy);
            this.CriticalEffect = criticalEffect;
        }

        public int Damage(bool criticalEffect)
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