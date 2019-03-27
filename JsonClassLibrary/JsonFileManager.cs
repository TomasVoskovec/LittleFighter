using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Core;

namespace JsonClassLibrary
{
    public class JsonFileManager
    {
        public void SendElement(Element data, string filePath)
        {
            List<Element> elements = JsonConvert.DeserializeObject<List<Element>>(File.ReadAllText(filePath));

            if (elements == null)
            {
                elements = new List<Element>();
            }

            elements.Add(data);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(elements));
        }

        public void UpdateElementsList (List<Element> elements, string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(elements));
        }

        public List<Element> LoadElements(string filePath)
        {
            return JsonConvert.DeserializeObject<List<Element>>(File.ReadAllText(filePath));
        }

        public List<Enemy> LoadMobs(string filePath)
        {
            return JsonConvert.DeserializeObject<List<Enemy>>(File.ReadAllText(filePath));
        }

        public void SendMob(Enemy data, string filePath)
        {
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