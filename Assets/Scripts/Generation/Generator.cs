using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int rows;
    public int columns;

    public GameObject player;

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] leftCorners;
    public GameObject[] rightCorners;
    public GameObject[] doorTiles;
    public GameObject[] writings;
    public GameObject[] lamps;

    private GameObject boardHolder;
    public enum TileType
    {
        floor, wall, specialWall, door, leftCorner, rightCorner
    }

    private TileType[][] tiles;

    // Start is called before the first frame update
    void Start()
    {
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();

        SetTileValues();

        InstantiateTiles();

        InstantiatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];

        for (int i = 0; i < tiles.Length; i++)
            tiles[i] = new TileType[rows];   
    }

    void SetTileValues()
    {
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
            {
                if (y == 0)
                    tiles[x][y] = TileType.floor;
                else
                {
                    if (x == 0)
                        tiles[x][y] = TileType.leftCorner;
                    else if (x == columns)
                        tiles[x][y] = TileType.rightCorner;
                    else if (x % 5 == 0)
                        tiles[x][y] = TileType.door;
                    else if ((x + 2) % 5 == 0)
                        tiles[x][y] = TileType.specialWall;
                    else
                        tiles[x][y] = TileType.wall;
                }
            }
    }

    void InstantiateTiles()
    {
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
            {
                switch (tiles[x][y])
                {
                    case TileType.floor:
                    {
                        InstantiateFromArray(floorTiles, x, y);
                        break;
                    }
                    case TileType.wall:
                    {
                        InstantiateFromArray(wallTiles, x, y);
                        break;
                    }
                    case TileType.leftCorner:
                    {
                        InstantiateFromArray(leftCorners, x, y);
                        break;
                    }
                    case TileType.rightCorner:
                    {
                        InstantiateFromArray(rightCorners, x, y);
                        break;
                    }
                    case TileType.door:
                    {     
                        InstantiateFromArray(doorTiles, x, y);
                        InstantiateFromArray(lamps, x, y);
                        break;
                        }
                    case TileType.specialWall:
                    {
                        InstantiateFromArray(wallTiles, x, y);
                        InstantiateFromArray(writings, x, y);
                        break;
                    }
                    default: break;
                }
            }
        
    }

    void InstantiatePlayer()
    {
        Instantiate(player, new Vector3Int(0, 1, 0), Quaternion.identity);
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = boardHolder.transform;
    }


}
