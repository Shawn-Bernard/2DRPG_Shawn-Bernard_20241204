using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();

    public TMP_Text playerUI;
    public GameObject GameOverScreen;

    public Tilemap MyTileMap;
    public TileBase playerTile;
    public Vector3Int playerPosition;

    private Vector3Int up = Vector3Int.up;
    private Vector3Int left = Vector3Int.left;
    private Vector3Int down = Vector3Int.down;
    private Vector3Int right = Vector3Int.right;

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
        //If the health system death method returns true do on death method
        if (healthSystem.Death())
        {
            onDeath();
        }
        //if the player isn't dead than control the player
        else
        {
            Controller();
        }
        
        
    }
    void onDeath()
    {
        //Get what tile the player was under than swap the player with the under tile
        playerPosition.z = 0;
        TileBase underTile = MyTileMap.GetTile(playerPosition);
        MyTileMap.SwapTile(playerTile, underTile);
        //Turning on the game over screen
        GameOverScreen.SetActive(true);
    }
    void UI()
    {
        playerUI.text = $"Player | Health: {healthSystem.health} | Damage: {damageAmount}";
    }
    void InteractOrMove(Vector3Int Direction)
    {
        //If this returns false do these
        if(!MovePlayer(Direction, out TileBase checkTile))
        {
            //If my check tile is the enemy deal damage to the enemy
            if (checkTile == Enemy.enemy.enemyTile)
            {
                Enemy.enemy.healthSystem.TakeDamage(damageAmount);
            }
            //If my check tile is the weapon tile add 5 to my damage amount
            else if (checkTile == MapGeneration.Map.weaponTile)
            {
                // checking the position plus direction and setting that tile to nothing
                Vector3Int checkPosition = playerPosition + Direction;
                MyTileMap.SetTile(checkPosition, null);
                damageAmount += 5;
            }
            //If my check tile is the heal tile use health system method to heal 
            else if (checkTile == MapGeneration.Map.healTile)
            {// checking the position plus direction and setting that tile to nothing
                Vector3Int checkPosition = playerPosition + Direction;
                MyTileMap.SetTile(checkPosition, null);
                healthSystem.Heal(healAmount);
            }
            //if my check tile is catus tile take damage using health system method
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
            //if my player does any movement set turn to false and moving player
            if (Input.GetKeyDown(KeyCode.W))
            {
                Turn = false;
                InteractOrMove(up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Turn = false;
                InteractOrMove(left);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Turn = false;
                InteractOrMove(down);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Turn = false;
                InteractOrMove(right);
            }
            
        }
    }
}
