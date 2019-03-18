using System;
using System.Collections.Generic;
using JsonClassLibrary;
using Little_Fighter;

namespace AddStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new Input();
            JsonFileManager fileManager = new JsonFileManager();

            bool selection = true;

            while (selection)
            {
                Console.WriteLine("#### ELEMENTS ####");
                Console.WriteLine("");
                Console.WriteLine("[1] - Add element");
                Console.WriteLine("[2] - Edit element");
                Console.WriteLine("");
                Console.WriteLine("#### MOBS ####");
                /*Console.WriteLine("");
                Console.WriteLine("[3] - Add mob");
                Console.WriteLine("");*/
                Console.WriteLine("[0] - Exit");

                char selectedItem = Console.ReadKey().KeyChar;

                Console.Clear();

                switch (selectedItem)
                {
                    case '1':
                        Console.WriteLine("#### ADD ELEMENT ####\n");
                        fileManager.SendElement(input.AddElement());
                        Console.WriteLine("Element added");
                        break;
                    case '2':
                        Console.WriteLine("#### EDIT ELEMENT ####\n");
                        input.EditElement();
                        break;
                    case '0':
                        selection = false;
                        break;
                    default:
                        Console.WriteLine("ERROR: Wrong value");
                        break;
                }
            }

            
        }
    }
}
