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
    public GameObject playerToCreate;
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
        Vector3 roomOffset = new Vector3(RoomID * 100,0,RoomID * 100);
        OuterWalls(roomOffset, roomSizeX, roomSizeZ);
        for (int i = 0/*-(roomSizeX / 2)*/; i < roomSizeX; i++)
        {
            for (int j = 0/*-(roomSizeZ / 2)*/; j < roomSizeZ; j++)
            {
                if (enemies[i,j] == true)
                {
                    CreateEnemy(i, j, roomOffset, roomSizeX, roomSizeZ);
                }
                if (walls[i,j] == true)
                {
                    CreateWall(i, j, roomOffset, roomSizeX, roomSizeZ);
                }else if (RoomID == 0 && Player == null)
                {
                    GameObject go = Instantiate(playerToCreate);
                    go.transform.position = roomOffset + new Vector3((i * offsetX) - (roomSizeX / 2), 1, (j * offsetZ) - (roomSizeZ / 2));
                    Player = go;
                }
                if (!doors[i, j].Equals("0"))
                {
                    CreateDoor(i, j, roomOffset, roomSizeX, roomSizeZ, doors[i, j]);
                }
            }
        }
        created = true;
    }
    /// <summary>
    /// Spawns the outer bounds of the room
    /// </summary>
    /// <param name="roomOffset"></param>
    /// <param name="roomSizeX"></param>
    /// <param name="roomSizeZ"></param>
    public void OuterWalls(Vector3 roomOffset, int roomSizeX, int roomSizeZ)
    {
        for (int i = -1; i < roomSizeX + 1; i++)
        {
            for (int j = -1; j < roomSizeZ + 1; j++)
            {
                if (i == -1)
                    CreateWall(i, j, roomOffset, roomSizeX, roomSizeZ);
                else if (j == -1)
                    CreateWall(i, j, roomOffset, roomSizeX, roomSizeZ);
                if (i == roomSizeX)
                    CreateWall(i, j, roomOffset, roomSizeX, roomSizeZ);
                else if (j == roomSizeZ)
                    CreateWall(i, j, roomOffset, roomSizeX, roomSizeZ);
            }
        }
    }

    public void CreateWall(int x, int z, Vector3 roomOffset, int roomSizeX, int roomSizeZ)
    {
        GameObject go;
        Vector3 offset = roomOffset + new Vector3((x * offsetX) - (roomSizeX / 2), 1, (z * offsetZ) - (roomSizeZ / 2));
        go = Instantiate(wallPiece);
        go.transform.position = /*transform.position + */offset;
        go.transform.SetParent(gameObject.transform);
    }
    public void CreateEnemy(int x, int z, Vector3 roomOffset, int roomSizeX, int roomSizeZ)
    {
        Enemy go;
        Vector3 offset = roomOffset + new Vector3((x * offsetX) - (roomSizeX / 2), 1, (z * offsetZ) - (roomSizeZ / 2));
        go = Instantiate(enemy) as Enemy;
        go.transform.position = /*transform.position +*/ offset;
        go.transform.SetParent(gameObject.transform);
    }
    public void CreateDoor(int x, int z, Vector3 roomOffset, int roomSizeX, int roomSizeZ, string doorString)
    {

    }
}
