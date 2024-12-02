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

    public Vector3Int enemyPosition;
    private Vector3Int up = Vector3Int.up;
    private Vector3Int left = Vector3Int.left;
    private Vector3Int down = Vector3Int.down;
    private Vector3Int right = Vector3Int.right;
    int range = 2;
    int damage = 10;

    public static Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.DIE())
        {
            enemyPosition.z = 0;
            TileBase checkTile = MyTileMap.GetTile(enemyPosition);
            MyTileMap.SwapTile(enemyTile, checkTile);

        }
        else
        {
            MyTileMap.SetTile(enemyPosition, enemyTile);
            if (Player.player.Turn == false)
            {
                if (RangeCheck())
                {
                    AttackMode();
                }
                else
                {
                    int randomDirection = Random.RandomRange(0, 4);
                    switch (randomDirection)
                    {
                        case 0:
                            MoveEnemy(up);
                            break;
                        case 1:
                            MoveEnemy(left);
                            break;
                        case 2:
                            MoveEnemy(down);
                            break;
                        case 3:
                            MoveEnemy(right);
                            break;
                    }
                }
            }
        }
        Player.player.Turn = true;

    }
    void AttackMode()
    {
        Vector3Int distance =  enemyPosition - Player.player.playerPosition;
        if (distance == up || distance == left || distance == down || distance == right)
        {
            MoveEnemy(Vector3Int.zero);
            Player.player.healthSystem.TakeDamage(damage);
            Debug.Log($"Player hp {Player.player.healthSystem}");
        }
        else
        {
            //if the enemy y is less than the players y move up 
            if (enemyPosition.y < Player.player.playerPosition.y)
            {
                MoveEnemy(up);//Move up
            }
            //if the enemy x is more than the players x move left 
            else if (enemyPosition.x > Player.player.playerPosition.x)
            {
                MoveEnemy(left);//Move left
            }
            //if the enemy y is less than the players y move up 
            else if (enemyPosition.y > Player.player.playerPosition.y)
            {
                MoveEnemy(down);//Move down
            }
            //if the enemy x is less than the player x move right
            else if (enemyPosition.x < Player.player.playerPosition.x)
            {
                MoveEnemy(right);//Move right
            }
        }
    }
    bool RangeCheck()
    {
        //For loop that goes to left to right
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                //Than add my enemy position with my x and y
                Vector3Int checkRange = enemyPosition + new Vector3Int(x, y, 0);

                if (checkRange == Player.player.playerPosition)
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

        //So we can check the tile base layer
        checkPosition.z = 0;

        //Checking tile at the check position
        TileBase checkTile = MyTileMap.GetTile(checkPosition);


        if (checkTile != MapGeneration.Map.groundTile)
        {
            Debug.Log("Can't move to this tile");
            return false;
        }

        //Swapping my player tile to the check tile
        MyTileMap.SwapTile(enemyTile, checkTile);

        //Change it so we can check top layer
        checkPosition.z = 1;

        //Returning this tile to check the top layer 
        checkTile = MyTileMap.GetTile(checkPosition);

        //Making my enemy position equal to check position
        enemyPosition = checkPosition;

        //Placing my enemy tile at the new position and placing enemy
        MyTileMap.SetTile(enemyPosition, enemyTile);
        return true;
    }
    void death()
    {
        
    }
}
