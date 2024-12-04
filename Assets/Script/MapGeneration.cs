using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.IO.Compression;

public class MapGeneration : MonoBehaviour
{
    public TextAsset MapOne;
    public TextAsset MapTwo;
    public TextAsset MapThree;

    public Tilemap MyTileMap;
    public TileBase groundTile;
    public TileBase catusTile;
    public TileBase wallTile;
    public TileBase weaponTile;
    public TileBase healTile;
    TileBase Tile;
    string tile;
    public static MapGeneration Map;
    string Path = $"{Application.dataPath}/Map/LevelOne.txt";
    //string Paths = $"{Application}/Map/LevelOne.txt";


    // Start is called before the first frame update
    void Start()
    {

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
                    Player.player.playerPosition = new Vector3Int(x, y, 1);
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

}
