using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Little_Fighter
{
    public class GameData
    {
        public Map Map { get; set; }
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }

        public List<CriticalEffect> PlayerCriticalEffects { get; set; }
        public List<CriticalEffect> EnemyCriticalEffects { get; set; }

        public List<Map> UnlockedMaps { get; set; }

        public GameData(Map map, Player player, Enemy enemy, List<CriticalEffect> playerCriticalEffects, List<CriticalEffect> enemyCriticalEffects)
        {
            this.Map = map;
            this.Player = player;
            this.Enemy = enemy;
            this.PlayerCriticalEffects = playerCriticalEffects;
            this.EnemyCriticalEffects = enemyCriticalEffects;
        }
    }
}
