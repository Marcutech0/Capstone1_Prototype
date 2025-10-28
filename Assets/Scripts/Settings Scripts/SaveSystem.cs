using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem {

   public static void SavePlayer(Vector3 playerPosition)
   {
 
       string path = Application.persistentDataPath + "/player.json";
        PlayerData data = new PlayerData(playerPosition);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        Debug.Log("Saved to " + path);

    }   

    public static PlayerData LoadPlayer()
    {    
        string path = Application.persistentDataPath + "/player.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Loaded from " + path);
            return data;
            
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
