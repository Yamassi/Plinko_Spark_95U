using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;

namespace Tretimi
{
    public class DataProvider : MonoBehaviour
    {
        public static void SaveDataJSON(SaveData saveData)
        {
            Debug.Log("SaveData");
            string json = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(Application.persistentDataPath + "/Data.json", json);
        }
        public static SaveData LoadDataJSON()
        {
            SaveData data;
            if (File.Exists(Application.persistentDataPath + "/Data.json"))
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/Data.json");
                data = JsonConvert.DeserializeObject<SaveData>(json);
            }
            else
            {
                data = null;
            }

            return data;
        }
    }
}

