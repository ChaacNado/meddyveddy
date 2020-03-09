using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createRoom : MonoBehaviour
{
    public GameObject wallPiece;
    public GameObject door;
    public Enemy enemy;
    public int offsetX = 1, offsetZ = 1;
    List<GameObject> walls = new List<GameObject>();
    GameObject Player;
    public int RoomID;
    bool created = false;

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
        testEnemies[7, 7] = true;
        //Create(0, roomSizeX, roomSizeZ, testWalls, testEnemies, testDoors);
    }

    void Update()
    {
        //-Debug.Log(created);
        if (Player == null && created)
            Player = GameObject.FindGameObjectWithTag("Player");
        else
        {
            //Debug.Log(RoomID + "    " + Player.GetComponent<Player>().currentRoomID);
            if (Player.GetComponent<Player>().currentRoomID != RoomID)
            {
                 //Debug.Log("inactivate");
                 gameObject.SetActive(false);
            }
        } 
        
    }

    public void Create(int roomNbr, int roomSizeX, int roomSizeZ, bool[,] walls, bool[,] enemies, string[,] doors)
    {
        RoomID = roomNbr;
        Vector3 offset;
        Vector3 roomOffset = new Vector3(RoomID * 50,0,RoomID * 50);
        for (int i = 0/*-(roomSizeX / 2)*/; i < roomSizeX; i++)
        {
            for (int j = 0/*-(roomSizeZ / 2)*/; j < roomSizeZ; j++)
            {
                if (enemies[i,j] == true)
                {
                    Enemy go;
                    offset = roomOffset + new Vector3((i * offsetX) - (roomSizeX / 2), 1, (j * offsetZ) - (roomSizeZ / 2));
                    go = Instantiate(enemy) as Enemy;
                    go.transform.position = /*transform.position +*/ offset;
                    go.transform.SetParent(gameObject.transform);
                }
                if (walls[i,j] == true)
                {
                    GameObject go;
                    offset = roomOffset + new Vector3((i * offsetX) - (roomSizeX / 2), 1, (j * offsetZ) - (roomSizeZ / 2));
                    go = Instantiate(wallPiece);
                    go.transform.position = /*transform.position + */offset;
                    go.transform.SetParent(gameObject.transform);
                }
            }
        }
        created = true;
    }
}
