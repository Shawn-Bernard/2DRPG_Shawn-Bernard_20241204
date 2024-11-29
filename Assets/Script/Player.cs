using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;


public class Player : MonoBehaviour
{
    public Tilemap MyTileMap;
    public TileBase Playertile;
    public static Player player;
    public bool Turn = true;
    public Vector3Int PlayerPosition;
    public int x =1;
    public int y =1;

    // Start is called before the first frame update
    void Start()
    {
        player = this;
        PlayerPosition = new Vector3Int(x, y, 0);

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }
    bool MovePlayer(Vector3Int Direction)
    {
        //Getting the player position and then add what direction were going and checking the position 
        Vector3Int checkPosition = PlayerPosition + Direction;
        checkPosition.z = 0;
        //Checking tile at the check position
        TileBase checkTile = MyTileMap.GetTile(checkPosition);

        if (MyTileMap.GetTile(checkPosition) != MapGeneration.Map.groundTile)
        {
            Debug.Log("Can't move to this tile");
            return false;
        }
        checkPosition.z = 1;
        //Swapping my player tile to the check tile
        MyTileMap.SwapTile(Playertile, checkTile);
        //Making my player position equal to check position
        PlayerPosition = checkPosition;
        //Placing my player tile at the new position and placing player
        MyTileMap.SetTile(PlayerPosition,Playertile);
        Debug.Log(PlayerPosition);
        return true;
    }
    void Controller()
    {
        MyTileMap.SetTile(PlayerPosition, Playertile);
        
        if (Turn == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Turn = false;
                MovePlayer(new Vector3Int(0, 1, 0));
                //MyTileMap.SetTile(new Vector3Int(x, y, 0), Playertile);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Turn = false;
                MovePlayer(new Vector3Int(-1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Turn = false;
                MovePlayer(new Vector3Int(0, -1, 0));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Turn = false;
                MovePlayer(new Vector3Int(1, 0, 0));
            } 
        }
    }
}
