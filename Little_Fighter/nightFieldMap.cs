using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    class NightFieldMap : Map
    {
        public NightFieldMap()
        {
            this.Name = mapName.nightField;
            this.BackgroundUri = "img/enviroment.png";
            this.Enemies = new List<Enemy> { new Bat() };
        }
    }
}
