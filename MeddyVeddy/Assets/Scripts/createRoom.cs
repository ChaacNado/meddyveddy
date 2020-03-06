using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createRoom : MonoBehaviour
{
    public GameObject wallPiece;
    public GameObject door;
    public int offsetX = 1, offsetZ = 1;
    List<GameObject> walls = new List<GameObject>();

    public int RoomID;

    void Start()
    {
        //Test
        int roomSizeX = 11, roomSizeZ = 11;
        bool[,] testWalls = new bool[roomSizeX, roomSizeZ], testEnemies = new bool[roomSizeX, roomSizeZ];
        string [,] testDoors = new string[roomSizeX, roomSizeZ];
        for (int i = 0; i < testWalls.GetLength(0); i++)
        {
            for (int j = 0; j < testWalls.GetLength(1); j++)
            {
                if (i == 0 || j == 0 ||i == testWalls.GetLength(0) -1|| j == testWalls.GetLength(1)-1)
                {
                    testWalls[i, j] = true;
                }
                else
                {
                    testWalls[i, j] = false;
                }
                testEnemies[i, j] = false;
                testDoors[i, j] = "";
            }
        }
        //testWalls[2, 1] = true;
        testWalls[2, 2] = true;
        testWalls[2, 3] = true;
        testWalls[2, 4] = true;
        Create(0, roomSizeX, roomSizeZ, testWalls, testEnemies, testDoors);
    }

    void Update()
    {
        
    }

    public void Create(int roomNbr, int roomSizeX, int roomSizeZ, bool[,] walls, bool[,] enemies, string[,] doors)
    {
        RoomID = roomNbr;
        GameObject go;
        Vector3 offset;
        for (int i = 0/*-(roomSizeX / 2)*/; i < roomSizeX; i++)
        {
            for (int j = 0/*-(roomSizeZ / 2)*/; j < roomSizeZ; j++)
            {
                if (walls[i,j] == true)
                {
                    offset = new Vector3((i * offsetX) - (roomSizeX / 2), 1, (j * offsetZ) - (roomSizeZ / 2));
                    go = Instantiate(wallPiece);
                    go.transform.position = transform.position + offset;
                }

            }
        }
    }
}
