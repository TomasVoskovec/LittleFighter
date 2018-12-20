using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    class GameData
    {
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }

        GameData(Player player, Enemy enemy)
        {
            this.Player = player;
            this.Enemy = enemy;
        }
    }
}
