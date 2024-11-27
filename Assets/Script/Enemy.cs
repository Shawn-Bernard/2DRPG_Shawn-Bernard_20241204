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
    public TileBase enemyTile;
    Vector3Int enemyPosition;
    int x = 5;
    int y = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = new Vector3Int(x,y);
        if (Player.player.Turn == false )
        {
            int randomDirection = Random.RandomRange(0, 5);
            switch (randomDirection)
            {
                case 0:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    y++;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;
                case 1:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    x--;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;
                case 2:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    y--;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;
                case 3:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    y++;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;

            }
            Player.player.Turn = true;
        }
        else
        {
            Debug.Log("true");
        }
        Debug.Log(MyTileMap.GetSprite(new Vector3Int(1, 1, 0)));
        if(MyTileMap.GetSprite(new Vector3Int(x, y, 0)) == Playertile)
        {

        }
    }



}
