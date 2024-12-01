using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();

    public Tilemap MyTileMap;
    public TileBase playerTile;
    public Vector3Int playerPosition;

    int damage = 20;

    public bool Turn = true;

    public static Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = this;

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
        
    }
    void InteractOrMove(Vector3Int Direction)
    {
        if(!MovePlayer(Direction, out TileBase checkTile))
        {
            if (checkTile == Enemy.enemy.enemyTile)
            {
                Enemy.enemy.healthSystem.TakeDamage(damage);
            }
        }
    }
    bool MovePlayer(Vector3Int Direction,out TileBase checkTile)
    {
        //Getting the player position and then add what direction were going and checking the position 
        Vector3Int checkPosition = playerPosition + Direction;

        //So we can check the tile base layer
        checkPosition.z = 0;

        //Checking tile at the check position
        checkTile = MyTileMap.GetTile(checkPosition);


        if (checkTile != MapGeneration.Map.groundTile)
        {
            Debug.Log("Can't move to this tile");
            return false;
        }

        //Swapping my player tile to the check tile
        MyTileMap.SwapTile(playerTile, checkTile);

        //Change it so we can check top layer
        checkPosition.z = 1;

        //Returning this tile to check the top layer 
        checkTile = MyTileMap.GetTile(checkPosition);

        if (checkTile == Enemy.enemy.enemyTile)
        {
            Debug.Log("Theres a enemy here");
            return false;
        }

        //Making my player position equal to check position
        playerPosition = checkPosition;

        //Placing my player tile at the new position and placing player
        MyTileMap.SetTile(playerPosition,playerTile);
        return true;
    }
    void Controller()
    {
        MyTileMap.SetTile(playerPosition, playerTile);
        
        if (Turn == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Turn = false;
                InteractOrMove(new Vector3Int(0, 1, 0));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Turn = false;
                InteractOrMove(new Vector3Int(-1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Turn = false;
                InteractOrMove(new Vector3Int(0, -1, 0));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Turn = false;
                InteractOrMove(new Vector3Int(1, 0, 0));
            }
            
        }
    }
}
