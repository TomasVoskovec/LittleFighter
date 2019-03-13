using System;
using System.Collections.Generic;
using System.Text;
using Little_Fighter;

namespace AddElement
{
    class Input
    {
        public string GetElementName()
        {
            Console.Write("Element name: ");
            string elementName = Console.ReadLine();
            Console.Clear();

            return elementName;
        }

        void elementSelectionUi (List<Element> loadedElements)
        {
            if (loadedElements != null)
            {
                int i = 0;

                foreach (Element loadedElement in loadedElements)
                {
                    i++;
                    Console.WriteLine("[{0}] - {1}", i, loadedElement.Name);
                }
            }

            Console.WriteLine("[0] - Continue");
        }

        public List<Element> GetSelectedElements (List<Element> loadedElements)
        {
            List<Element> elements = new List<Element>();

            if (loadedElements != null)
            {
                bool selection = true;

                while (selection)
                {
                    elementSelectionUi(loadedElements);

                    string s = Console.ReadLine();

                    bool success = Int32.TryParse(s, out int value);

                    if (success && value <= loadedElements.Count)
                    {
                        elements.Add(loadedElements[value]);
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Wrong value");
                    }
                }
            }
            elements = null;

            return elements;
        }

        public Element AddElement(List<Element> loadedElements)
        {
            string elementName = GetElementName();

            Console.Clear();
            Console.WriteLine("###### SELECT HIGH EFECTIVITY ELEMENT ######\n");
            List<Element> heighEfectivityElements = GetSelectedElements(loadedElements);

            Console.Clear();
            Console.WriteLine("###### SELECT LOW EFECTIVITY ELEMENT ######\n");
            List<Element> lowEfectivityElements = GetSelectedElements(loadedElements);

            return new Element(elementName, heighEfectivityElements, lowEfectivityElements);
        }
    }
}
