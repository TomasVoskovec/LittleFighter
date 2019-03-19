using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Enemy
    {
        public Element Element { get; set; }
        public Dictionary<string, Uri> Anims { get; set; }
        public Dictionary<string, EnemyAttack> Attacks { get; set; } = new Dictionary<string, EnemyAttack>();

        public string Name { get; set; }

        public float MaxHP { get; set; }
        public int HP { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Speed { get; set; }
        public float Size { get; set; }

        public Enemy(string name, Element element, Dictionary<string, Uri> anims, Dictionary<string, EnemyAttack> attacks, float maxHP, float attack, float defense, float speed, float size)
        {
            this.Name = name;

            this.Element = element;
            this.Anims = anims;
            this.Attacks = attacks;

            this.MaxHP = maxHP;
            this.HP = Convert.ToInt32(this.MaxHP);
            this.Attack = attack;
            this.Defense = defense;
            this.Speed = speed;
            this.Size = size;
        }
    }
}
