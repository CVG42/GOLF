using System.IO;
using UnityEngine;
using Newtonsoft.Json;
namespace Golf
{
    public static class SaveSystem
    {
        private static string savePath = Application.persistentDataPath + "SaveData";

        public static void Save(GameData data)
        { 
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(savePath, json);
        }

        public static GameData Load()
        {
            if (!File.Exists(savePath))
            { 
                string json = File.ReadAllText(savePath);
                return JsonConvert.DeserializeObject<GameData>(json);
            }
            return new GameData();
        }
    }
}
