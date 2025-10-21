using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class MapSetting
{
    public TileData[] tileData;
}

[System.Serializable]
public class TileData
{
    public string tileName;
    public int indexID;
    public string tilePath;
}

[System.Serializable]
public class ActorData
{
    public string name;
    public int maxHealth;
    public int health;
    public int damage;
}
public class GameSettings : MonoBehaviour
{
    private string dataPath = Application.dataPath;

    /// <summary>
    /// Gets tile base from tile palette folder in resources  
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public TileBase GetTileBase(string path)
    {
        return Resources.Load<TileBase>($"TilePalette/{path}");
    }

    /// <summary>
    /// Returns data from a json file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public T GetDataFromJsonFile<T>(string filePath)
    {
        if (File.Exists(dataPath + filePath))
        {
            Debug.Log("file does exist");
            string json = File.ReadAllText(dataPath + filePath);
            T actorData = JsonConvert.DeserializeObject<T>(json);
            return actorData;
        }
        else
        {
            Debug.Log($"{dataPath + filePath} doesn't exist");
        }

        return default(T);
    }

    /// <summary>
    /// Takes in an actor data and actor, to apply data to actor 
    /// </summary>
    /// <param name="_actorData"></param>
    /// <param name="actor"></param>
    public void LoadActorData(ActorData _actorData, Actor actor)
    {
        actor.Name = _actorData.name;
        actor.healthSystem.MaxHealth = _actorData.maxHealth;
        actor.healthSystem.Health = _actorData.health;
        actor.Damage = _actorData.damage;
    }

    /// <summary>
    /// Returns data from a json file text asset
    /// </summary>
    /// <param name="textAsset"></param>
    /// <returns></returns>
    public T GetDataFromTextJsonFile<T>(TextAsset textAsset)
    {
        if (textAsset != null)
        {
            Debug.Log("text asset does exist");
            T mapSet = JsonConvert.DeserializeObject<T>(textAsset.text);
            return mapSet;
        }
        else
        {
            Debug.Log($"{textAsset.text} is null");
        }

        return default(T);
    }
}
