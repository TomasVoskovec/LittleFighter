﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class EnemyAttack
    {
        public string Name { get; set; }

        public int Strenght { get; set; }
        public int ChanceToMiss { get; set; }

        public List<CriticalEffect> CriticalEffects { get; set; }
        public Uri Anim { get; set; }

        public EnemyAttack(string name, int strenght, int chanceToMiss, List<CriticalEffect> criticalEffects, Uri anim)
        {
            this.Name = name;
            this.Strenght = strenght;
            this.ChanceToMiss = chanceToMiss;
            this.CriticalEffects = criticalEffects;
            this.Anim = anim;
        }

        public int Damage(Player player, Enemy enemy)
        {
            Random rn = new Random();

            int missChance = rn.Next(0, 101);

            if (missChance <= 100 - this.ChanceToMiss - player.Speed + enemy.Speed)
            {
                int rndValue = Strenght * (Convert.ToInt32(enemy.Attack) - Convert.ToInt32(player.Defense));

                int damage = rn.Next(rndValue, rndValue + 1);
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