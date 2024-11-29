using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();

    public Tilemap MyTileMap;
    public TileBase enemyTile;

    Vector3Int enemyPosition;

    int enemyX = 5;
    int enemyY = 5;
    int range = 1;
    int Damage = 10;

    public static Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this;
        enemyPosition = new Vector3Int(enemyX, enemyY);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player.player.Turn == false)
        {
            if (RangeCheck(enemyPosition))
            {
                MoveEnemy(new Vector3Int(0, 0, 0));
            }
            else
            {
                int randomDirection = Random.RandomRange(0, 4);
                switch (randomDirection)
                {
                    case 0:
                        MoveEnemy(new Vector3Int(0, 1, 0));
                        break;
                    case 1:
                        MoveEnemy(new Vector3Int(-1, 0, 0));
                        break;
                    case 2:
                        MoveEnemy(new Vector3Int(0, -1, 0));
                        break;
                    case 3:
                        MoveEnemy(new Vector3Int(1, 0, 0));
                        break;
                }

                
            }
            Player.player.Turn = true;
            Debug.Log($"Enemy Position{enemyPosition}");
            Debug.Log(healthSystem.health);
        }

    }
    bool RangeCheck(Vector3Int EnemyPosition)
    {
        //Left side equal to -1, middle equal to 0, right equal to 1
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                //Than add my enemy position with my x and y
                Vector3Int checkRange = EnemyPosition + new Vector3Int(x, y, 0);

                if (checkRange == Player.player.PlayerPosition)
                {
                    Debug.Log($"Player found at {checkRange}");
                    return true;
                }
            }
        }
        return false;
    }
    bool MoveEnemy(Vector3Int Direction)
    {
        //Getting the enemy position and then add what direction were going and checking the position 
        Vector3Int checkPosition = enemyPosition + Direction;

        Vector3Int distance = enemyPosition - Player.player.PlayerPosition;

        checkPosition.z = 0;
        //Checking tile at the check position
        TileBase checkTile = MyTileMap.GetTile(checkPosition);

        if (MyTileMap.GetTile(checkPosition) != MapGeneration.Map.groundTile)
        {
            Debug.Log("Can't move to this tile");
            return false;
        }
        
        checkPosition.z = 1;
        //Swapping my enemy tile to the check tile
        MyTileMap.SwapTile(enemyTile, checkTile);
        //Making my player position equal to check position
        enemyPosition = checkPosition;
        //Placing my enemy tile at the new position and placing enemy
        MyTileMap.SetTile(enemyPosition, enemyTile);
        return true;
    }
}
