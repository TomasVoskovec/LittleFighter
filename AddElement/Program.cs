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

            Input input = new Input();
            JsonFileManager fileManager = new JsonFileManager();

            bool selection = true;

            while (selection)
            {
                Console.WriteLine("[1] - Add element");
                Console.WriteLine("[0] - Exit");

                char selectedItem = Console.ReadKey().KeyChar;

                switch (selectedItem)
                {
                    case '1':
                        Console.Clear();
                        fileManager.SendElement(input.AddElement(loadedElements));
                        Console.WriteLine("Element added");
                        break;
                    case '0':
                        selection = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("ERROR: Wrong value");
                        break;
                }
            }

            
        }
    }
}
