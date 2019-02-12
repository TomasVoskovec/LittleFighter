using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class Map
    {
        public enum mapName
        {
            nightField,
        }

        public mapName Name { get; set; }
        public string BackgroundUri { get; set; }

        public List<Enemy> Enemies { get; set; }
    }
}
