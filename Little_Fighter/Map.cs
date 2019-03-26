using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Little_Fighter
{
    public class Map
    {
        public string Name { get; set; }
        public string BackgroundUri { get; set; }

        public List<Enemy> Enemies { get; set; }

        public Map (string name, string backgroundUri, List<Enemy> enemies)
        {
            this.Name = name;
            this.BackgroundUri = backgroundUri;
            this.Enemies = enemies;
        }
    }
}
