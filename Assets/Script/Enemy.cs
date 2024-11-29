using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public Tilemap MyTileMap;
    public TileBase enemyTile;
    Vector3Int enemyPosition;
    int enemyX = 5;
    int enemyY = 5;
    int Rnage;
    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = new Vector3Int(enemyX, enemyY);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player.player.Turn == false)
        {
            
            int randomDirection = Random.RandomRange(0, 4);
            switch (randomDirection)
            {
                case 0:
                    MoveEnemy(new Vector3Int( 0, 1, 0));
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
            
            Player.player.Turn = true;
            Debug.Log($"Enemy Position{enemyPosition}");
        }

    }
    bool MoveEnemy(Vector3Int Direction)
    {
        //Getting the enemy position and then add what direction were going and checking the position 
        Vector3Int checkPosition = enemyPosition + Direction;
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
