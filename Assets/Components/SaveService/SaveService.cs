using System;
using System.IO;
using UnityEngine;

namespace Components.SaveService
{
    public static class SaveService
    {
        //create file
        private const string FILE_NAME = "InfiniteDiscountSave.json";
        //file ptah
        private static string FilePath => Path.Combine(Application.persistentDataPath, FILE_NAME);

        public static void Save(SaveData data)
        {
            //Access save data and write text in file to save
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(FilePath, json);

            Debug.Log("Data successfully saved at: " + FilePath);
        }
        
        public static SaveData Load()
        {
            try
            {
                // get data from file to load savedata
                string json = File.ReadAllText(FilePath);
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception exception)
            {
                // create new Savedata if none
                Debug.LogWarning("No data found, creating a new one... Details: " + exception);
                return new SaveData();
            }
        }
    }
}