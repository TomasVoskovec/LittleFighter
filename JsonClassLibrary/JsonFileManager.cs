using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Core;

namespace JsonClassLibrary
{
    public class JsonFileManager
    {
        public Dictionary<string, string> JsonFilePaths { get; set; } = new Dictionary<string, string>();

        public JsonFileManager()
        {
            //string startupPath = Environment.CurrentDirectory;

            JsonFilePaths.Add("elements", @"../../../AppData/Elements.json");
            JsonFilePaths.Add("mobs", @"../../../AppData/Mobs.json");
        }

        public void SendElement(Element data)
        {
            string filePath = this.JsonFilePaths["elements"];

            List<Element> elements = JsonConvert.DeserializeObject<List<Element>>(File.ReadAllText(filePath));

            if (elements == null)
            {
                elements = new List<Element>();
            }

            elements.Add(data);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(elements));
        }

        public void UpdateElementsList (List<Element> elements)
        {
            string filePath = this.JsonFilePaths["elements"];

            File.WriteAllText(filePath, JsonConvert.SerializeObject(elements));
        }

        public List<Element> LoadElements()
        {
            string filePath = this.JsonFilePaths["elements"];

            return JsonConvert.DeserializeObject<List<Element>>(File.ReadAllText(filePath));
        }

        public List<Enemy> LoadMobs()
        {
            string filePath = this.JsonFilePaths["mobs"];

            return JsonConvert.DeserializeObject<List<Enemy>>(File.ReadAllText(filePath));
        }

        public void SendMob(Enemy data)
        {
            string filePath = this.JsonFilePaths["mobs"];

            List<Enemy> elements = JsonConvert.DeserializeObject<List<Enemy>>(File.ReadAllText(filePath));

            if (elements == null)
            {
                elements = new List<Enemy>();
            }

            elements.Add(data);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(elements));
        }
    }
}