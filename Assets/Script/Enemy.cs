using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public GameObject winScreen;
    public HealthSystem healthSystem = new HealthSystem();

    public TMP_Text enemyUI;
    public Tilemap MyTileMap;
    public TileBase enemyTile;

    //enemy position wasn't static so it didn't exist
    public static Vector3Int enemyPosition;
    private Vector3Int up = Vector3Int.up;
    private Vector3Int left = Vector3Int.left;
    private Vector3Int down = Vector3Int.down;
    private Vector3Int right = Vector3Int.right;
    int range = 2;
    int damageAmount = 20;

    public static Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        UI();
        //if the enemy death returns true than show win screen
        if (healthSystem.Death())
        {
            winScreen.SetActive(true);
            //getting the tile under the enemy and swap the tile with under tile
            enemyPosition.z = 0;
            TileBase underTile = MyTileMap.GetTile(enemyPosition);
            MyTileMap.SwapTile(enemyTile, underTile);

        }
        else
        {
            MyTileMap.SetTile(enemyPosition,enemyTile);
            if (Player.player.Turn == false)
            {
                //If this is returns true go into attack mode
                if (RangeCheck())
                {
                    AttackMode();
                }
                else
                {
                    //picking a random number
                    int randomDirection = Random.Range(0, 4);
                    //if my random number is one of these go in that direction
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
    void UI()
    {
        enemyUI.text = $"Enemy | Health: {healthSystem.health} | Damage: {damageAmount}";
    }
    void AttackMode()
    {
        Vector3Int distance =  enemyPosition - Player.player.playerPosition;
        //Checking if the player is one tile up, left, down, right 
        if (distance == up || distance == left || distance == down || distance == right)
        {
            //Not moving the enemy and then damage the player
            MoveEnemy(Vector3Int.zero);
            Player.player.healthSystem.TakeDamage(damageAmount);
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
        //For loop that looks for the player by -2 tile to the left and also on the y
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {

                //Than add my enemy position with my x and y
                Vector3Int checkRange = enemyPosition + new Vector3Int(x, y, 0);
                // if my player is in check range than return true
                if (checkRange == Player.player.playerPosition)
                {
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

        //If my check tile isn't a ground tile than returns false
        if (checkTile != MapGeneration.Map.groundTile)
        {
            Debug.Log("Can't move to this tile");
            return false;
        }

        //Swapping my enemy tile to the check tile
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
}
