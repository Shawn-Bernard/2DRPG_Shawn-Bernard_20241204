using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public Tilemap MyTileMap;
    public TileBase Playertile;
    public TileBase groundTile;
    public TileBase wallTile;
    int x;
    int y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MyTileMap.GetSprite(new Vector3Int(1, 1, 0)));
        Player.player.Playertile;
        if(MyTileMap.GetSprite(new Vector3Int(x, y, 0)) == Playertile)
        {

        }
    }



}
