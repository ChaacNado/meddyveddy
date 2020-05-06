using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public static class LoadMapStatic
{
    public static Vector2 roomOffsetGlobal = new Vector2(3,3);
    public static GameObject player = null;
    public static GameObject Dungeon;
    public static List<GameObject> rooms = new List<GameObject>();
    static int roomNumber = 0;
    public static void LoadMap(string filePath, GameObject dungeon, GameObject room, string pathOfFolder)
    {
        NewDungeonModel dungeonModel = DeSerializer.DeserializeXMLFileToObject<NewDungeonModel>(filePath, "Dungeon");
        Dungeon = GameObject.Instantiate(dungeon);
        //if (dungeonModel == null)
        //    Debug.Log("ewgbaefdr");
        foreach (RoomID r in dungeonModel.Rooms.rooms)
        {
            //Debug.Log("funkar: " + r.ID);
            string path = Path.Combine(pathOfFolder, "room-" + r.ID + ".xml");
            FileInfo fileInfo = new FileInfo(path);
            //r.ID = "" + roomNumber;
            if (fileInfo.Exists)
            {
                //Debug.Log("finns: " + r.ID);
                newRoomModel roomObject = DeSerializer.DeserializeXMLFileToObject<newRoomModel>(path, "Room");
                rooms.Add(CallCreateRoom(dungeonModel, roomObject, room));
                roomNumber++;
            }else
            {
                Debug.Log(path + "                          ERROR PATH");
            }
        }
        
        foreach (GameObject r in rooms)
        {
            foreach (GameObject d in r.GetComponent<createRoom>().doors)
            {
                foreach (GameObject target in rooms)
                {
                    //Debug.Log(target.GetComponent<createRoom>().XmlID + "   :   " + d.GetComponent<DoorScript>().XmlTargetId);
                    if (target.GetComponent<createRoom>().XmlID.Equals(d.GetComponent<DoorScript>().XmlTargetId))
                    {
                        d.GetComponent<DoorScript>().TargetRoom = target;
                        d.GetComponent<DoorScript>().targetRoomID = target.GetComponent<createRoom>().RoomID;
                    }
                }
            }
        }
    }
    public static GameObject CallCreateRoom(NewDungeonModel d, newRoomModel r, GameObject room)
    {
        int x = r.width, z = r.height, roomID = roomNumber;
        string XmlID = r.ID;
        bool[,] walls = new bool[x, z];
        bool[,] enemies = new bool[x, z];
        bool[,] treasure = new bool[x, z];
        string[,] doors = doorsToString(x, z, r, d);
        bool[,] bossCenter = new bool[x, z];

        if (r.Tiles.tiles == null)
            Debug.Log("r.Tiles.tiles");

        for (int i = 0; i < r.Tiles.tiles.Count; i++)
        {
            if (r.Tiles.tiles[i].Type == TileType.WALL)
            {
                walls[(int)r.Tiles.tiles[i].X, (int)r.Tiles.tiles[i].Y] = true;
            } else if (r.Tiles.tiles[i].Type == TileType.ENEMY)
            {
                enemies[(int)r.Tiles.tiles[i].X, (int)r.Tiles.tiles[i].Y] = true;
            } else if (r.Tiles.tiles[i].Type == TileType.TREASURE)
            {
                treasure[(int)r.Tiles.tiles[i].X, (int)r.Tiles.tiles[i].Y] = true;
            }
        }
        for (int i = 0; i < r.Customs.customs.Count; i++)
        {
            //Debug.Log("Ska ´finnas en boss");
            bossCenter[r.Customs.customs[i].X, r.Customs.customs[i].Y] = true;
        }
        GameObject newRoom = GameObject.Instantiate(room);
        newRoom.GetComponent<createRoom>().Create(roomID, x, z, walls, enemies, doors, treasure, bossCenter);
        newRoom.GetComponent<createRoom>().XmlID = XmlID;
        return newRoom;
    }

    public static string[,] doorsToString(int x, int y, newRoomModel r, NewDungeonModel d)
    {
        string[,] doors = new string[x, y];
        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                doors[i, j] = "0";
            }
        }
        foreach (Connection rem in d.Connections.Connections)
        {
            if (r.ID.Equals(rem.From))
            {
                int posX = (int)rem.FromPosX;
                int posY = (int)rem.FromPosY;
                doors[posX, posY] = "1," + rem.toPosX + "," + rem.toPosY + "," + rem.To;
            }else if (r.ID.Equals(rem.To))
            {
                int posX = (int)rem.toPosX;
                int posY = (int)rem.toPosY;
                doors[posX, posY] = "1," + rem.FromPosX + "," + rem.FromPosY + "," + rem.From;
            }
        }

        return doors;
    }
}
