using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    public GameSettings settings;
    private MapSetting mapSetting;
    private TileData[] tilesData;

    public Player player;
    public Enemy enemy;

    public TextAsset MapOne;
    public TextAsset MapTwo;
    public TextAsset MapThree;

    public Tilemap MyTileMap;

    [HideInInspector]
    public TileBase groundTile,wallTile, catusTile,weaponTile,healTile;

    public static MapGeneration Map;

    private Dictionary<char, TileBase> tileBaseDictionary;

    private char[] symbols = { ' ', '#', '*', '^', '+' };

    public TextAsset MapSettingJson;

    void Start()
    {
        LoadMap();

        Map = this;
        TryGetComponent<Tilemap>(out MyTileMap);
        int randomMap = Random.Range(0, 3);
        switch (randomMap)
        {
            case 0:
                ConvertMapToTilemap(MapOne.text);
                break;
            case 1:
                ConvertMapToTilemap(MapTwo.text);
                break;
            case 2:
                ConvertMapToTilemap(MapThree.text);
                break;
            
        }
        
    }

    public void ConvertMapToTilemap(string mapData)
    {
        //Split the map when no text is there
        string[] Map = mapData.Split("\n");
        char tile;

        for (int y = 0; y < Map.Length; y++)
        {
            for (int x = 0; x < Map[y].Length; x++)
            {
                tile = Map[y][x];

                if (tile == '#')//Wall tile
                {
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
                else if (tile == ' ')//Ground tile
                {
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
                else if (tile == '*')//Catus tile
                {
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), catusTile);
                }
                else if (tile == '^')//Weapon tile
                {
                    MyTileMap.SetTile(new Vector3Int(x, y, 1), weaponTile);
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
                else if (tile == '+')//Health pick tile
                {
                    MyTileMap.SetTile(new Vector3Int(x, y, 1), healTile);
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
                else if (tile == '@')//Player position
                {
                    player.playerPosition = new Vector3Int(x, y, 1);
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
                else if (tile == '!')//Enemy position
                {
                    enemy.enemyPosition = new Vector3Int(x, y, 1);
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
            }
        }
    }
    public void LoadPremadeMap(string Path)
    {

        if (File.Exists(Path))
        {
            string mapData = File.ReadAllText(Path);
            ConvertMapToTilemap(mapData);
        }
    }

    /// <summary>
    /// Loads json file text asset and apply tile data from map setting to map gen data
    /// </summary>
    private void LoadMap()
    {
        if (MapSettingJson != null)
        {
            mapSetting = settings.GetDataFromTextJsonFile<MapSetting>(MapSettingJson);
            tilesData = mapSetting.tileData;
        }
        else
        {
            Debug.Log("TextAsset is null! :(");
        }

        InitializeTileData();
    }

    /// <summary>
    ///  Uses tile data to get tile base 
    /// </summary>
    private void InitializeTileData()
    {
        if (tilesData != null)
        {
            tileBaseDictionary = new Dictionary<char, TileBase>();

            for (int i = 0; i < mapSetting.tileData.Length; i++)
            {
                TileData tileInfo = tilesData[i];
                //Gets a return tile base from json file
                TileBase tile = settings.GetTileBase(tileInfo.tilePath);
                if (tile != null)
                {
                    char symbol = symbols[tileInfo.indexID];
                    tileBaseDictionary[symbol] = tile;
                }
                else
                {
                    Debug.Log($"TileBase {tileInfo.tilePath} not found");
                }
            }
            //Apply my tiles base
            groundTile = tileBaseDictionary[' '];
            wallTile = tileBaseDictionary['#'];
            catusTile = tileBaseDictionary['*'];
            weaponTile = tileBaseDictionary['^'];
            healTile = tileBaseDictionary['+'];
        }
    }


}
