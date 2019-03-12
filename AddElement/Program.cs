using System;
using System.Collections.Generic;
using JsonClassLibrary;
using Little_Fighter;

namespace AddElement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Element> loadedElements = new JsonFileManager().LoadElements();

            string elementName = getElementName();

            
        }

        static string getElementName()
        {
            Console.Write("Element name: ");
            string elementName = Console.ReadLine();
            Console.Clear();

            return elementName;
        }
    }
}
