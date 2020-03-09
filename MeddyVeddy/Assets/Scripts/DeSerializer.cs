using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public static class DeSerializer
{

    public static T DeserializeXMLFileToObject<T>(string XmlFilename)
    {
        T returnObject = default(T);
        if (string.IsNullOrEmpty(XmlFilename)) return default(T);

        try
        {
            //Debug.Log("kommer iaf hit");
            StreamReader xmlStream = new StreamReader(XmlFilename);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            returnObject = (T)serializer.Deserialize(xmlStream);
        } catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
        return returnObject;
    }

}

[Serializable]
public class DungeonModel
{
    public DungeonModel() { }

    public int InitialRoomID { get; set; }

    public Vector2 InitialPos { get; set; }

    //Required markup for collections
    [XmlElement(ElementName = "Rooms")]
    public List<RoomModel> Rooms { get; set; }
}

[Serializable]
public class RoomModel
{
    public RoomModel() { }

    public int RoomID { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public double EntranceSafety { get; set; }

    [XmlElement(ElementName = "ConnectingRooms")]
    public RoomEdgeModel[] ConnectingRoomIDs { get; set; }

    //Required markup for collections
    [XmlElement(ElementName = "Tiles")]
    public TileModel[] Tiles { get; set; }
}

public class RoomEdgeModel
{
    public RoomEdgeModel() { }

    public Vector2 StartDoorPosition { get; set; }

    public Vector2 TargetDoorPosition { get; set; }

    public int ToRoomID { get; set; }
}

[Serializable]
[XmlRoot(ElementName = "Tile")]
public class TileModel
{
    public TileModel() { }

    public TileModel(Vector2 Position, bool Immutable)
    {
        this.Position = Position;
        this.Immutable = Immutable;
    }

    public Vector2 Position { get; set; }

    public bool Immutable { get; set; }

    public TileType Type { get; set; }
}

[Serializable]
public enum TileType
{
    FLOOR,
    WALL,
    TREASURE,
    ENEMY,
    DOOR,
    DOORENTER,
    NONE
}