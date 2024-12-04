using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();

    public TMP_Text PlayerUI;
    public GameObject GameOverScreen;

    public Tilemap MyTileMap;
    public TileBase playerTile;
    public Vector3Int playerPosition;
    int catusAmount = 10;
    int damageAmount = 5;
    int healAmount = 10;

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

        UI();
        if (healthSystem.Death())
        {
            onDeath();
        }
        else
        {
            Controller();
        }
        
        
    }
    void onDeath()
    {
        playerPosition.z = 0;
        TileBase underTile = MyTileMap.GetTile(playerPosition);
        MyTileMap.SwapTile(playerTile, underTile);
        GameOverScreen.SetActive(true);
    }
    void UI()
    {
        PlayerUI.text = $" Health: {healthSystem.health} | Damage: {damageAmount}";
    }
    void InteractOrMove(Vector3Int Direction)
    {
        if(!MovePlayer(Direction, out TileBase checkTile))
        {
            if (checkTile == Enemy.enemy.enemyTile)
            {
                Enemy.enemy.healthSystem.TakeDamage(damageAmount);
            }
            else if (checkTile == MapGeneration.Map.weaponTile)
            {
                Vector3Int checkPosition = playerPosition + Direction;
                MyTileMap.SetTile(checkPosition, null);
                damageAmount += 5;
            }
            else if (checkTile == MapGeneration.Map.healTile)
            {
                Vector3Int checkPosition = playerPosition + Direction;
                MyTileMap.SetTile(checkPosition, null);
                healthSystem.Heal(healAmount);
            }
            else if (checkTile == MapGeneration.Map.catusTile)
            {
                healthSystem.TakeDamage(catusAmount);
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
        else if (checkTile == MapGeneration.Map.catusTile)
        {
            return false;
        }

        //Swapping my player tile to the check tile
        MyTileMap.SwapTile(playerTile, checkTile);

        //Change it so we can check top layer
        checkPosition.z = 1;

        //Returning this tile to check the top layer 
        checkTile = MyTileMap.GetTile(checkPosition);

        if (checkTile == Enemy.enemy.enemyTile || checkTile == MapGeneration.Map.weaponTile || checkTile == MapGeneration.Map.healTile)
        {
            Debug.Log("Theres a Interactable tile here ");
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
