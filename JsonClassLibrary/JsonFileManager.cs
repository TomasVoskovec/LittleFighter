using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Core;
using Little_Fighter;

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

            if (!elements.Contains(data))
            {
                elements.Add(data);
            }

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
            List<Enemy> mobs = JsonConvert.DeserializeObject<List<Enemy>>(File.ReadAllText(filePath));

            if (mobs == null)
            {
                mobs = new List<Enemy>();
            }

            if (!mobs.Contains(data))
            {
                mobs.Add(data);
            }

            File.WriteAllText(filePath, JsonConvert.SerializeObject(mobs));
        }

        public List<Map> LoadMaps(string filePath)
        {
            return JsonConvert.DeserializeObject<List<Map>>(File.ReadAllText(filePath));
        }

        public void SendMap(Map data, string filePath)
        {
            List<Map> maps = JsonConvert.DeserializeObject<List<Map>>(File.ReadAllText(filePath));

            if (maps == null)
            {
                maps = new List<Map>();
            }

            if (!maps.Contains(data))
            {
                maps.Add(data);
            }

            File.WriteAllText(filePath, JsonConvert.SerializeObject(maps));
        }
    }
}