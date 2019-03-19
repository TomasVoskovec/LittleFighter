using System;
using System.Collections.Generic;
using JsonClassLibrary;
using Core;

namespace AddStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new Input();
            JsonFileManager fileManager = new JsonFileManager();

            /*class Bat : Enemy
    {
        public Bat(int lvl = 1)
        {
            this.Element = new Element("sss");
            this.Anims = new Dictionary<string, Uri>();
            this.Anims.Add("idle", new Uri("img/anim/bat_idle.gif", UriKind.Relative));
            this.Anims.Add("hurt", new Uri("img/anim/bat_hurt.gif", UriKind.Relative));

            this.MaxHP = 20 + (20 / 10 * lvl);
            this.HP = Convert.ToInt32(this.MaxHP);
            this.Attack = 4 + (20 * lvl / 100);
            this.Defense = 1 + (20 * lvl / 100);
            this.Speed = 30 + (20 * lvl / 100);
            this.Size = 1 + (20 * lvl / 100);

            this.Attacks.Add("Bite", new EnemyAttack("Bite", 1, 25, new List<CriticalEffect> { new CriticalEffect("Poison", 2, 100) }, new Uri("img/anim/bat_attack.gif", UriKind.Relative)));
        }
    }*/

            Dictionary < string, Uri > batAnims= new Dictionary<string, Uri>();
            batAnims.Add("idle", new Uri(@"../../../../AppData/img/anim/bat_idle.gif", UriKind.Relative));
            batAnims.Add("hurt", new Uri(@"../../../../AppData/img/anim/bat_hurt.gif", UriKind.Relative));

            Dictionary<string, EnemyAttack> batAttacks = new Dictionary<string, EnemyAttack>();
            batAttacks.Add("Bite", new EnemyAttack("Bite", 1, 25, new List<CriticalEffect> { new CriticalEffect("Poison", 2, 100) }, new Uri("img/anim/bat_attack.gif", UriKind.Relative)));

            Element batElement = new Element("");
            List<Element> loadedElements = fileManager.LoadElements();

            if (loadedElements != null)
            {
                foreach (Element element in loadedElements)
                {
                    if (element.Name == "Night")
                    {
                        batElement = element;
                    }
                }
            }
            Enemy bat = new Enemy("Bat", batElement, batAnims, batAttacks, 20, 4, 1, 30, 1);

            fileManager.SendMob(bat);

            bool selection = true;

            while (selection)
            {
                Console.WriteLine("#### ELEMENTS ####");
                Console.WriteLine("");
                Console.WriteLine("[1] - Add element");
                Console.WriteLine("[2] - Edit element");
                Console.WriteLine("");
                /*Console.WriteLine("#### MOBS ####");
                Console.WriteLine("");
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
