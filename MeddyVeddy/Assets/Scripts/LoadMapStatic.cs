using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public static class LoadMapStatic
{
    public static GameObject Dungeon;
    public static List<GameObject> rooms = new List<GameObject>();
    public static void LoadMap(string filePath, GameObject dungeon, GameObject room)
    {
        DungeonModel dungeonModel = DeSerializer.DeserializeXMLFileToObject<DungeonModel>(filePath);
        Dungeon = GameObject.Instantiate(dungeon);
        foreach(RoomModel r in dungeonModel.Rooms)
        {
            rooms.Add(CallCreateRoom(r, room));
        }
        foreach(GameObject r in rooms)
        {
            foreach(GameObject d in r.GetComponent<createRoom>().doors)
            {
                foreach (GameObject target in rooms)
                {
                    if (target.GetComponent<createRoom>().RoomID == d.GetComponent<DoorScript>().targetRoomID)
                    {
                        d.GetComponent<DoorScript>().TargetRoom = target;
                    }
                }
            }
        }
    }
    public static GameObject CallCreateRoom(RoomModel r, GameObject room)
    {
        int x = r.Width, z = r.Height, roomID = r.RoomID;
        bool[,] walls = new bool[x, z];
        bool[,] enemies = new bool[x, z];
        bool[,] treasure = new bool[x, z];
        string[,] doors = doorsToString(x, z, r);

        for (int i = 0; i < r.Tiles.Length; i++)
        {
            if (r.Tiles[i].Type == TileType.WALL)
            {
                walls[(int)r.Tiles[i].Position.x, (int)r.Tiles[i].Position.y] = true;
            }else if (r.Tiles[i].Type == TileType.ENEMY)
            {
                enemies[(int)r.Tiles[i].Position.x, (int)r.Tiles[i].Position.y] = true;
            }
        }

        GameObject newRoom = GameObject.Instantiate(room);
        newRoom.GetComponent<createRoom>().Create(roomID, x, z, walls, enemies, doors);

        return newRoom;
    }

    public static string[,] doorsToString(int x, int y, RoomModel r)
    {
        string[,] doors = new string[x, y];
        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                doors[i, j] = "0";
            }
        }
        foreach(RoomEdgeModel rem in r.ConnectingRoomIDs)
        {
            int posX = (int)rem.StartDoorPosition.x;
            int posY = (int)rem.StartDoorPosition.y;
            doors[posX, posY] = "1," + rem.TargetDoorPosition.x + "," + rem.TargetDoorPosition.y + "," + rem.ToRoomID;
        }

        return doors;
    }
}
