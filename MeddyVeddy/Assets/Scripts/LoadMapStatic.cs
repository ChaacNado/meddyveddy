﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public static class LoadMapStatic
{
    public static GameObject Dungeon;
    public static void LoadMap(string filePath, GameObject dungeon, GameObject room)
    {
        DungeonModel dungeonModel = DeSerializer.DeserializeXMLFileToObject<DungeonModel>(filePath);
        Dungeon = GameObject.Instantiate(dungeon);
        foreach(RoomModel r in dungeonModel.Rooms)
        {
            CallCreateRoom(r, room);
        }
    }
    public static GameObject CallCreateRoom(RoomModel r, GameObject room)
    {
        int x = r.Width, z = r.Height, roomID = r.RoomID;
        bool[,] walls = new bool[x, z];
        bool[,] enemies = new bool[x, z];
        bool[,] treasure = new bool[x, z];
        string[,] doors = new string[x, z];

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

        return room;
    }
}