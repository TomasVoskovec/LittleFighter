using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Little_Fighter;

namespace JsonClassLibrary
{
    public class JsonFileManager
    {
        public Dictionary<string, string> JsonFilePaths { get; set; }

        public JsonFileManager()
        {
            JsonFilePaths.Add("elements", @"Elements.json");
        }

        public void SendElement(Element data)
        {
            string filePath = this.JsonFilePaths["elements"];

            List<Element> elements = JsonConvert.DeserializeObject<List<Element>>(File.ReadAllText(filePath));
            elements.Add(data);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(elements));
        }

        public List<Element> LoadElements()
        {
            string filePath = this.JsonFilePaths["elements"];

            return JsonConvert.DeserializeObject<List<Element>>(File.ReadAllText(filePath));
        }
    }
}
