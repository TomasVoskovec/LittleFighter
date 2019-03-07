﻿using System;
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

        public GameData(Player player, Enemy enemy)
        {
            this.Player = player;
            this.Enemy = enemy;
        }

        Random rn = new Random();

        // returns value of Fast Attack damage
        /*public int FastAttack()
        {
            int damage;
            int missChance;

            missChance = rn.Next(0, 100);

            if (missChance >= 100 - this.Enemy.Speed)
            {
                damage = 0;
            }
            else
            {
                damage = rn.Next(2 * Convert.ToInt32(this.Player.Attack), 5 * Convert.ToInt32(this.Player.Attack)) + rn.Next(1, 3);
            }

            return damage;
        }*/

        //returns value of Jump Attack damage
        /*public int JumpAttack()
        {
            int damage;
            int missChance;

            missChance = rn.Next(0, 100);

            if (missChance >= 50 - this.Enemy.Speed)
            {
                damage = 0;
            }
            else
            {
                damage = rn.Next(5 * Convert.ToInt32(this.Player.Attack), 10 * Convert.ToInt32(this.Player.Attack));
            }

            return damage;
        }*/

        public bool isGameOver()
        {
            return this.Player.HP == 0 || this.Enemy.HP == 0;
        }
    }
}
