using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;


public class Player : MonoBehaviour
{
    public Tilemap MyTileMap;
    public TileBase Playertile;
    public TileBase groundTile;
    public TileBase wallTile;
    public static Player player;
    public bool Turn = true;
    Vector3Int PlayerPosition;
    int x =1;
    int y =1 ;

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
    void Controller()
    {
        PlayerPosition = new Vector3Int(x , y,0);
        MyTileMap.SetTile(PlayerPosition, Playertile);
        if (Turn == true)
        {

            if (Input.GetKeyDown(KeyCode.W) && MyTileMap.GetTile(new Vector3Int(x, y + 1, 0)) == groundTile)
            {
                Turn = false;
                if (true)
                {
                    MyTileMap.SwapTile(Playertile, groundTile);
                    y++;
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), Playertile);
                }
                //MyTileMap.SetTile(new Vector3Int(x, y, 0), Playertile);
            }
            else if (Input.GetKeyDown(KeyCode.A) && MyTileMap.GetTile(new Vector3Int(x - 1, y, 0)) == groundTile)
            {
                Turn = false;
                if (true)
                {
                    MyTileMap.SwapTile(Playertile, groundTile);
                    x--;
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), Playertile);
                }
                //MyTileMap.SetTile(new Vector3Int(x, y, 0), Playertile);
            }
            else if (Input.GetKeyDown(KeyCode.S) && MyTileMap.GetTile(new Vector3Int(x, y - 1, 0)) == groundTile)
            {
                Turn = false;
                if (true)
                {
                    MyTileMap.SwapTile(Playertile, groundTile);
                    y--;
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), Playertile);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) && MyTileMap.GetTile(new Vector3Int(x + 1, y, 0)) == groundTile)
            {
                Turn = false;
                if (true)
                {
                    MyTileMap.SwapTile(Playertile, groundTile);
                    x++;
                    MyTileMap.SetTile(new Vector3Int(x, y, 0), Playertile);
                }
            }
        }
    }
}
