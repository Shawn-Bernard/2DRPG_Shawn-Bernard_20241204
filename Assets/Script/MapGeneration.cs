using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using UnityEditor.U2D.Aseprite;

public class MapGeneration : MonoBehaviour
{
    public Tilemap MyTileMap;

    public TileBase groundTile;
    public TileBase wallTile;
    public TileBase Tile;
    string tile;
    public static MapGeneration Map;
    string Path = $"{Application.dataPath}/Map/LevelOne.txt";



    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Tilemap>(out MyTileMap);
        LoadPremadeMap(Path);
        Map = this;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ConvertMapToTilemap(string mapData)
    {
        string[] Map = mapData.Split("\n");
        char tile;

        for (int y = 0; y < Map.Length; y++)
        {
            for (int x = 0; x < Map[y].Length; x++)
            {
                tile = Map[y][x];

                if (tile == '#')
                {
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
                else if (tile == ' ')
                {
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
