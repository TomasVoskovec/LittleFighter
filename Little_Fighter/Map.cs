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

        public List<Element> Elements { get; set; }

        public Map (string name, string backgroundUri, List<Element> elements)
        {
            this.Name = name;
            this.BackgroundUri = backgroundUri;
            this.Elements = elements;
        }
    }
}
