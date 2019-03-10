using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class GameData
    {
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }

        public List<CriticalEffect> PlayerCriticalEffects { get; set; }
        public List<CriticalEffect> EnemyCriticalEffects { get; set; }

        public GameData(Player player, Enemy enemy, List<CriticalEffect> playerCriticalEffects, List<CriticalEffect> enemyCriticalEffects)
        {
            this.Player = player;
            this.Enemy = enemy;
            this.PlayerCriticalEffects = playerCriticalEffects;
            this.EnemyCriticalEffects = enemyCriticalEffects;
        }
    }
}
