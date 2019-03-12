using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Fighter
{
    public class Element
    {
        public string Name { get; set; }

        public List<Element> HeighEfectivityElements { get; set; }
        public List<Element> LowEfectivityElements { get; set; }

        public Element(string name, List<Element> heighEfectivityElements = null, List<Element> lowEfectivityElements = null)
        {
            this.Name = name;
            this.HeighEfectivityElements = heighEfectivityElements;
            this.LowEfectivityElements = lowEfectivityElements;
        }
    }
}