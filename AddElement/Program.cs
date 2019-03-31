using System;
using System.Collections.Generic;
using JsonClassLibrary;
using Core;
using Little_Fighter;

namespace AddStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new Input();
            JsonFileManager fileManager = new JsonFileManager();

            void createBat()
            {
                Dictionary<string, Uri> batAnims = new Dictionary<string, Uri>();
                batAnims.Add("idle", new Uri("img/anim/bat_idle.gif", UriKind.Relative));
                batAnims.Add("hurt", new Uri("img/anim/bat_hurt.gif", UriKind.Relative));

                Dictionary<string, EnemyAttack> batAttacks = new Dictionary<string, EnemyAttack>();
                batAttacks.Add("Bite", new EnemyAttack("Bite", 1, 25, new List<CriticalEffect> { new CriticalEffect("Poison", 2, 100) }, new Uri("img/anim/bat_attack.gif", UriKind.Relative)));

                Element batElement = new Element("");
                List<Element> loadedElements = fileManager.LoadElements("../../../../AppData/Elements.json");

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

                fileManager.SendMob(bat, "../../../../AppData/Mobs.json");
            }

            void createStoneBat()
            {
                Dictionary<string, Uri> batAnims = new Dictionary<string, Uri>();
                batAnims.Add("idle", new Uri("img/anim/stone_bat_idle.gif", UriKind.Relative));
                batAnims.Add("hurt", new Uri("img/anim/stone_bat_hurt.gif", UriKind.Relative));

                Dictionary<string, EnemyAttack> batAttacks = new Dictionary<string, EnemyAttack>();
                batAttacks.Add("Bite", new EnemyAttack("Rock Punch", 1, 25, new List<CriticalEffect>(), new Uri("img/anim/stone_bat_attack.gif", UriKind.Relative)));

                Element batElement = new Element("");
                List<Element> loadedElements = fileManager.LoadElements("../../../../AppData/Elements.json");

                if (loadedElements != null)
                {
                    foreach (Element element in loadedElements)
                    {
                        if (element.Name == "Rock")
                        {
                            batElement = element;
                        }
                    }
                }
                Enemy bat = new Enemy("Stone Bat", batElement, batAnims, batAttacks, 20, 4, 1, 30, 3);

                fileManager.SendMob(bat, "../../../../AppData/Mobs.json");
            }

            void createNightField()
            {
                List<Enemy> loadedMobs = fileManager.LoadMobs("../../../../AppData/Mobs.json");
                List<Enemy> mapMobs = new List<Enemy>();

                foreach(Enemy mob in loadedMobs)
                {
                    if (/*mob.Name == "Bat" ||*/ mob.Name == "Stone Bat")
                    {
                        mapMobs.Add(mob);
                    }
                }

                Map nightField = new Map("Night Field", "img/maps/nightField.png", mapMobs);

                fileManager.SendMap(nightField, "../../../../AppData/Maps.json");
            }

            // ##### ADD MOBS #####
            createBat();
            createStoneBat();

            // ##### ADD MAPS #####
            createNightField();

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
                        fileManager.SendElement(input.AddElement(), "../../../../AppData/Elements.json");
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
