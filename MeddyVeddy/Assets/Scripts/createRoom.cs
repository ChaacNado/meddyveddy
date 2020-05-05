using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createRoom : MonoBehaviour
{
    public GameObject wallPiece;
    public GameObject door;
    public Enemy enemy;
    public GameObject Treasure;

    public int offsetX = 1, offsetZ = 1;
    List<GameObject> walls = new List<GameObject>();
    List<Enemy> enemies = new List<Enemy>();
    GameObject Player;
    public GameObject playerToCreate;
    public int RoomID;
    bool created = false;
    public List<GameObject> doors = new List<GameObject>();
    public Vector2 roomSize;
    float toAdd = 1.5f;
    public Vector3 roomOffset;
    public string XmlID;
    public Dictionary<Vector2, Vector3> doorsIndexed = new Dictionary<Vector2, Vector3>();


    void Start()
    {
        //Test
        int roomSizeX = 11, roomSizeZ = 11;
        bool[,] testWalls = new bool[roomSizeX, roomSizeZ], testEnemies = new bool[roomSizeX, roomSizeZ];
        string[,] testDoors = new string[roomSizeX, roomSizeZ];
        for (int i = 0; i < testWalls.GetLength(0); i++)
        {
            for (int j = 0; j < testWalls.GetLength(1); j++)
            {
                if (i == 0 || j == 0 || i == testWalls.GetLength(0) - 1 || j == testWalls.GetLength(1) - 1)
                {
                    testWalls[i, j] = true;
                } else
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
        //else
        //{
        //    //Debug.Log(RoomID + "    " + Player.GetComponent<Player>().currentRoomID);
        //    if (Player.GetComponent<Player>().currentRoomID != RoomID)
        //    {
        //        //Debug.Log("inactivate");
        //        gameObject.SetActive(false); /* Breaks the game as of now */
        //    }
        //}

        if (Player != null)
        {
            foreach (Enemy e in enemies)
            {
                if (e != null)
                {
                    if (e.roomID == Player.GetComponent<Player>().currentRoomID)
                    {
                        e.inSameRoom = true;
                    } else
                    {
                        e.inSameRoom = false;
                    }
                }
            }
        }

        //Debug.Log("currentRoomID: " + Player.GetComponent<Player>().currentRoomID + ", RoomID: " + RoomID);
    }

    public void Create(int roomNbr, int roomSizeX, int roomSizeZ, bool[,] walls, bool[,] enemies, string[,] doors, bool[,] treasure, bool[,] bossCenter)
    {
        RoomID = roomNbr;
        roomOffset = new Vector3(RoomID * (roomSizeX * LoadMapStatic.roomOffsetGlobal.x), 0, RoomID * (roomSizeZ * LoadMapStatic.roomOffsetGlobal.y) * (RoomID%2)*-1);
        roomSize = new Vector2(roomSizeX, roomSizeZ);
        OuterWalls(/*roomOffset,*/ roomSizeX, roomSizeZ);
        for (int i = 0/*-(roomSizeX / 2)*/; i < roomSizeX; i++)
        {
            for (int j = 0/*-(roomSizeZ / 2)*/; j < roomSizeZ; j++)
            {
                int x = i, y = roomSizeZ - j;
                if (enemies[i, j] == true)
                {
                    CreateEnemy(x, y,/* roomOffset,*/ roomSizeX, roomSizeZ, false);
                }
                else if (walls[i, j] == true)
                {
                    CreateWall(x, y, /*roomOffset,*/ roomSizeX, roomSizeZ);
                }
                else if (treasure[i, j])
                {
                    CreateTreasure(x, y, roomSizeX, roomSizeZ);
                }
                else if (!doors[i, j].Equals("0"))
                {
                    CreateDoor(x, y, /*roomOffset,*/ roomSizeX, roomSizeZ, doors[i, j]);
                } else if (RoomID == 0 && Player == null && LoadMapStatic.player == null)
                {
                    GameObject go = Instantiate(playerToCreate);
                    go.transform.position = new Vector3(((x * toAdd) * offsetX) - (roomSizeX / 2), 1, (((y * toAdd) * offsetZ) - (roomSizeZ / 2)));/*roomOffset + new Vector3((x * offsetX) - (roomSizeX / 2), 1, ((y * offsetZ) - (roomSizeZ / 2)) * -1); */                    Player = go;
                    LoadMapStatic.player = go;
                }
                if (bossCenter[i, j])
                {
                    CreateEnemy(x, y,/* roomOffset,*/ roomSizeX, roomSizeZ, true);
                }
            }
        }
        transform.position = roomOffset;
        created = true;
    }
    /// <summary>
    /// Spawns the outer bounds of the room
    /// </summary>
    /// <param name="roomOffset"></param>
    /// <param name="roomSizeX"></param>
    /// <param name="roomSizeZ"></param>
    public void OuterWalls(/*Vector3 roomOffset,*/ int roomSizeX, int roomSizeZ)
    {
        for (int i = -1; i < roomSizeX + 1; i++)
        {
            for (int j = -1; j < roomSizeZ + 1; j++)
            {
                if (i == -1)
                    CreateWall(i, j + 1, /*roomOffset,*/ roomSizeX, roomSizeZ);
                else if (j == -1)
                    CreateWall(i, j + 1,/* roomOffset,*/ roomSizeX, roomSizeZ);
                if (i == roomSizeX)
                    CreateWall(i, j + 1, /*roomOffset,*/ roomSizeX, roomSizeZ);
                else if (j == roomSizeZ)
                    CreateWall(i, j + 1, /*roomOffset,*/ roomSizeX, roomSizeZ);
            }
        }
    }
    public void CreateWall(int x, int z/*, Vector3 roomOffset*/, int roomSizeX, int roomSizeZ)
    {
        string holderName = "Generated Walls " + RoomID;
        /// Creates a child for "Room" during runtime, which will hold all the room's walls
        if (!transform.Find(holderName))
        {
            Transform wallHolder = new GameObject(holderName).transform;
            wallHolder.SetParent(transform);
        }

        GameObject go;
        //Vector3 offset = /*roomOffset + */new Vector3((x * offsetX) - (roomSizeX / 2), 1, (z * offsetZ) - (roomSizeZ / 2));
        go = Init(x,z,roomSizeX,roomSizeZ,wallPiece);
        //go.transform.position = /*transform.position + */offset;
        //go.transform.SetParent(gameObject.transform);
        go.transform.parent = transform.Find(holderName);
    }
    public void CreateEnemy(int x, int z, /*Vector3 roomOffset,*/ int roomSizeX, int roomSizeZ, bool boss)
    {
        string holderName = "Generated Enemies " + RoomID;
        /// Creates a child for "Room" during runtime, which will hold all the room's enemies
        if (!transform.Find(holderName))
        {
            Transform enemyHolder = new GameObject(holderName).transform;
            enemyHolder.parent = transform;
        }

        Enemy go;
        Vector3 offset =/* roomOffset +*/ new Vector3(((x * toAdd) * offsetX) - (roomSizeX / 2), 1, (((z * toAdd) * offsetZ) - (roomSizeZ / 2)));
        go = Instantiate(enemy) as Enemy;
        //go.isBoss = true;
        go.transform.position = offset;
        go.roomID = RoomID;
        enemies.Add(go);
        //go.transform.position = /*transform.position +*/ offset;
        //go.transform.SetParent(gameObject.transform);
        go.transform.parent = transform.Find(holderName);
        if (boss)
        {
            //Debug.Log("should spawn boss");
            go.GetComponent<Enemy>().isBoss = boss;
        }
    }
    public void CreateDoor(int x, int z, /*Vector3 roomOffset,*/ int roomSizeX, int roomSizeZ, string doorString)
    {
        string holderName = "Generated Doors" + RoomID;
        if (!transform.Find(holderName))
        {
            Transform doorHolder = new GameObject(holderName).transform;
            doorHolder.SetParent(transform);
        }

        GameObject go;
        //Vector3 offset = /*roomOffset + */new Vector3((x * offsetX) - (roomSizeX / 2), 1, (z * offsetZ) - (roomSizeZ / 2));
        go = Init(x,z,roomSizeX,roomSizeZ,door);
        //go.transform.position = /*transform.position + */offset;
        //go.transform.SetParent(gameObject.transform);
        go.transform.parent = transform.Find(holderName);
        string[] splitDoorString = doorString.Split(',');

        //foreach(GameObject r in LoadMapStatic.rooms)
        //{
        //    if (r.GetComponent<createRoom>().XmlID.Equals(splitDoorString[3]))
        //        go.GetComponent<DoorScript>().targetRoomID = r.GetComponent<createRoom>().RoomID;
        //}
        go.GetComponent<DoorScript>().XmlTargetId = splitDoorString[3];
        go.GetComponent<DoorScript>().posOfTarget = new Vector2(int.Parse(splitDoorString[1])/* * toAdd*/, int.Parse(splitDoorString[2])/* * toAdd*/);
        doorsIndexed.Add(new Vector2(x, roomSizeZ - z), go.transform.position);
        doors.Add(go);
    }
    public void CreateTreasure(int x, int z, int roomSizeX, int roomSizeZ)
    {
        string holderName = "Generated Treasure" + RoomID;
        if (!transform.Find(holderName))
        {
            Transform treasureHolder = new GameObject(holderName).transform;
            treasureHolder.SetParent(transform);
        }

        GameObject go;
        //Vector3 offset = new Vector3((x * offsetX) - (roomSizeX / 2), 1, (z * offsetZ) - (roomSizeZ / 2));
        go = Init(x, z, roomSizeX, roomSizeZ, Treasure);
        //go.transform.position = offset;
        go.transform.parent = transform.Find(holderName);

    }
   

    public GameObject Init(int x, int z, int roomSizeX, int roomSizeZ, GameObject toInit)
    {
        GameObject toReturn;

       
        Vector3 offset = new Vector3(((x * toAdd) * offsetX) - (roomSizeX / 2), 1, (((z * toAdd) * offsetZ) - (roomSizeZ / 2)));
        toReturn = Instantiate(toInit);
        toReturn.transform.position = offset;

        return toReturn;
    }
}
