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
    int enemyX = 5;
    int enemyY = 5;
    public int[,] EnemyZone = new int [4,4];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player.player.Turn == false)
        {
            enemyPosition = new Vector3Int(enemyX, enemyY);
            int randomDirection = Random.RandomRange(0, 4);
            switch (randomDirection)
            {
                case 0:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    enemyY++;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;
                case 1:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    enemyX--;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;
                case 2:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    enemyY--;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;
                case 3:
                    MyTileMap.SwapTile(enemyTile, groundTile);
                    enemyX++;
                    MyTileMap.SetTile(enemyPosition, enemyTile);
                    break;
            }
            
            Player.player.Turn = true;
            Debug.Log($"Enemy Position{enemyPosition}");
            Range(enemyX, enemyY);
        }

    }
    void Range(int x, int y)
    {
        EnemyZone = new int[x, y];
        for (int check_x = x - 1; check_x <= x + 1; check_x++)
        {
            if (check_x >= 0 && check_x < EnemyZone.GetLength(0))
            {
                for (int check_y = y - 1; check_y <= y + 1; check_y++)
                {
                    if (check_y >= 0 && check_y < EnemyZone.GetLength(1))
                    {
                        if (check_x == Player.player.x && check_y == Player.player.y)
                        {
                            Debug.Log($"playerX{Player.player.x}|playerY{Player.player.y}");
                        }
                        Debug.Log($"[X{x}] [Y{y}]");
                        Debug.Log($"[CheckX{check_x}] [CheckY{check_y}]");
                    }
                }
            }
        }
    }
}
