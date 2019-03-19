using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Efectivity
    {
        public Element BaseElement { get; set; }

        public enum EfectivityValue
        {
            Low,
            Normal,
            Heigh
        }

        public Efectivity(Element baseElement)
        {
            this.BaseElement = baseElement;
        }

        public EfectivityValue GetEfectivityValue(Element element)
        {
            if (BaseElement.HeighEfectivityElements.Any())
            {
                foreach (Element heightEfectivityElement in BaseElement.HeighEfectivityElements)
                {
                    if (heightEfectivityElement.Name == element.Name)
                    {
                        return EfectivityValue.Heigh;
                    }
                }
            }
            if (BaseElement.LowEfectivityElements != null)
            {
                foreach (Element lowEfectivityElement in BaseElement.LowEfectivityElements)
                {
                    if (lowEfectivityElement.Name == element.Name)
                    {
                        return EfectivityValue.Low;
                    }
                }
            }
            

            return EfectivityValue.Normal;
        }
    }
}
