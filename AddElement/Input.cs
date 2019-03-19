using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JsonClassLibrary;
using Core;

namespace AddStuff
{
    class Input
    {
        public string GetElementName()
        {
            bool nameSelection = true;
            string elementName = "";

            while (nameSelection)
            {
                nameSelection = false;

                Console.Write("Element name: ");
                elementName = Console.ReadLine();

                Console.Clear();

                List<Element> loadedElements = new JsonFileManager().LoadElements();

                if (loadedElements != null && loadedElements.Any())
                {
                    foreach (Element element in new JsonFileManager().LoadElements())
                    {
                        if (element.Name == elementName)
                        {
                            nameSelection = true;
                            Console.WriteLine("This element already exist");
                        }
                    }
                }
            }

            return elementName;
        }

        void elementSelectionUi (List<Element> elements)
        {
            if (elements != null)
            {
                int i = 0;

                foreach (Element element in elements)
                {
                    i++;
                    Console.WriteLine("[{0}] - {1}", i, element.Name);
                }
            }
        }

        public Element GetSelectedElement(List<Element> elements)
        {
            Element selectedElement = new Element(null);
            List<Element> selectableElements = elements;

            if (selectableElements != null)
            {
                bool selection = selectableElements.Any();

                while (selection)
                {
                    elementSelectionUi(selectableElements);

                    string s = Console.ReadLine();

                    bool success = Int32.TryParse(s, out int value);

                    if (success && value <= selectableElements.Count && value > 0)
                    {
                        Console.Clear();

                        selectedElement = selectableElements[value - 1];

                        selection = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("ERROR: Wrong value");
                    }
                }
            }

            return selectedElement;
        }

        public List<Element> GetSelectedElements (List<Element> elements)
        {
            List<Element> selectedElements = new List<Element>();
            List<Element> selectableElements = elements;

            if (selectableElements != null)
            {
                bool selection = selectableElements.Any();

                while (selection)
                {
                    elementSelectionUi(selectableElements);
                    Console.WriteLine("[0] - Done");

                    string s = Console.ReadLine();

                    bool success = Int32.TryParse(s, out int value);

                    if (value == 0)
                    {
                        selection = false;
                        Console.Clear();
                    }
                    else if (success && value <= selectableElements.Count)
                    {
                        Console.Clear();
                        selectedElements.Add(selectableElements[value - 1]);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("ERROR: Wrong value");
                    }
                }

                if (!selectedElements.Any())
                {
                    selectedElements = null;
                }
            }

            return selectedElements;
        }

        public void showElements (List<Element> elements)
        {
            Console.WriteLine("########################");

            foreach (Element element in elements)
            {
                Console.Write("{0}", element.Name);

                if (element.HeighEfectivityElements != null && element.HeighEfectivityElements.Any())
                {
                    Console.Write(" - Heigh efective elements: ");
                    foreach (Element highEfectiveElement in element.HeighEfectivityElements)
                    {
                        Console.Write("{0} ", highEfectiveElement.Name);
                    }
                }

                if (element.HeighEfectivityElements != null && element.HeighEfectivityElements.Any())
                {
                    Console.Write(" # Low efective elements: ");
                    foreach (Element lowEfectiveElement in element.LowEfectivityElements)
                    {
                        Console.Write("{0} ", lowEfectiveElement.Name);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("########################");
        }

        public Element EditElement()
        {
            List<Element> elements = new JsonFileManager().LoadElements();
            Element selectedElement = GetSelectedElement(elements);
            Element editedElement = new Element(selectedElement.Name, selectedElement.HeighEfectivityElements, selectedElement.LowEfectivityElements);

            bool update = true;

            while (update)
            {
                Console.WriteLine("[1] - Change name");
                Console.WriteLine("[2] - Change high efective elements");
                Console.WriteLine("[3] - Change low efective elements");
                Console.WriteLine("[4] - Delete Element");
                Console.WriteLine("[0] - Update");

                char selectedItem = Console.ReadKey().KeyChar;


                Console.Clear();
                switch (selectedItem)
                {
                    case '1':
                        Console.WriteLine("#### CHANGE NAME ####");
                        editedElement.Name = GetElementName();
                        break;
                    case '2':
                        Console.WriteLine("#### CHANGE HIGH EFECTIVITY ELEMENTS ####");
                        editedElement.HeighEfectivityElements = GetSelectedElements(new JsonFileManager().LoadElements());
                        break;
                    case '3':
                        Console.WriteLine("#### CHANGE LOW EFECTIVITY ELEMENTS ####");
                        editedElement.LowEfectivityElements = GetSelectedElements(new JsonFileManager().LoadElements());
                        break;
                    case '4':
                        elements.RemoveAll(element => element.Name == selectedElement.Name);
                        update = false;
                        break;
                    case '0':
                        update = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("ERROR: Wrong value");
                        break;
                }
            }

            foreach (Element element in elements)
            {
                if (element.Name == selectedElement.Name)
                {
                    element.Name = editedElement.Name;
                    element.HeighEfectivityElements = editedElement.HeighEfectivityElements;
                    element.LowEfectivityElements = editedElement.LowEfectivityElements;

                    Console.WriteLine("Element updated");

                    break;
                }
            }

            Console.Clear();

            new JsonFileManager().UpdateElementsList(elements);

            return editedElement;
        }

        public Element AddElement()
        {
            string elementName = GetElementName();

            Console.Clear();
            Console.WriteLine("###### SELECT HIGH EFECTIVITY ELEMENT ######\n");
            List<Element> heighEfectivityElements = GetSelectedElements(new JsonFileManager().LoadElements());

            Console.Clear();
            Console.WriteLine("###### SELECT LOW EFECTIVITY ELEMENT ######\n");
            List<Element> lowEfectivityElements = GetSelectedElements(new JsonFileManager().LoadElements());

            return new Element(elementName, heighEfectivityElements, lowEfectivityElements);
        }

        // ############ MOBS ############

        /*public Enemy AddMob()
        {
            string mobName = GetMobName();
            Element mobElement = GetSelectedElement(new JsonFileManager().LoadElements());



            Console.Clear();
            Console.WriteLine("###### SELECT HIGH EFECTIVITY ELEMENT ######\n");
            List<Element> heighEfectivityElements = GetSelectedElements(new JsonFileManager().LoadElements());

            Console.Clear();
            Console.WriteLine("###### SELECT LOW EFECTIVITY ELEMENT ######\n");
            List<Element> lowEfectivityElements = GetSelectedElements(new JsonFileManager().LoadElements());

            return new Enemy(elementName, heighEfectivityElements, lowEfectivityElements);
        }

        Uri getAnimationUri()
        {
            bool uriSelection = true;
            string uri = "";
            Uri newUri = new Uri("", UriKind.Relative);

            while (uriSelection)
            {
                uriSelection = false;

                Console.Write("Animation URI: ");
                uri = Console.ReadLine();

                Console.Clear();

                if (File.Exists(uri))
                {
                    newUri = new Uri(uri, UriKind.Relative);
                }
                else
                {
                    uriSelection = true;
                }
            }

            return newUri;
        }

        public string GetMobName()
        {
            bool nameSelection = true;
            string elementName = "";

            while (nameSelection)
            {
                nameSelection = false;

                Console.Write("Element name: ");
                elementName = Console.ReadLine();

                Console.Clear();

                foreach (Enemy mob in new JsonFileManager().LoadMobs())
                {
                    if (mob.Name == elementName)
                    {
                        nameSelection = true;
                        Console.WriteLine("This element already exist");
                    }
                }
            }

            return elementName;
        }*/
    }
}
