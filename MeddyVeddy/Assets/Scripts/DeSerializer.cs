using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public static class DeSerializer
{

    public static T DeserializeXMLFileToObject<T>(string XmlFilename, string root)
    {
        T returnObject = default(T);
        if (string.IsNullOrEmpty(XmlFilename)) return default(T);

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = root;
        xRoot.IsNullable = true;
        
        //StreamReader xmlStream = new StreamReader(XmlFilename);
        //XmlSerializer serializer = new XmlSerializer(typeof(T), xRoot);
        //returnObject = (T)serializer.Deserialize(xmlStream);
        try
        {
            //Debug.Log("kommer iaf hit");
            StreamReader xmlStream = new StreamReader(XmlFilename);
            XmlSerializer serializer = new XmlSerializer(typeof(T), xRoot);
            returnObject = (T)serializer.Deserialize(xmlStream);
        } catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
        //Debug.Log(returnObject);
        return returnObject;
    }

}
#region forDungeonFile
[Serializable]
public class NewDungeonModel
{
    public NewDungeonModel() { }
    public string ID {get;set;}
    [XmlElement(ElementName = "Rooms")]
    public RoomList Rooms { get; set; }
    [XmlElement(ElementName = "Connections")]
    public ConnectionList Connections { get; set; }
}
[Serializable]
public class RoomList
{
    public RoomList() { }
    [XmlElement(ElementName = "Room")]
    public List<RoomID> rooms { get; set; }
}
[Serializable]
//[XmlRoot(ElementName = "Connection")]
public class ConnectionList
{
    public ConnectionList() { }
    [XmlElement(ElementName = "Connection")]
    public List<Connection> Connections { get; set; }
}

[Serializable]
[XmlRoot(ElementName = "Room")]
public class RoomID
{
    public RoomID() { }
    //[XmlElement(ElementName = "ID")]
    [XmlAttribute(AttributeName = "ID")]
    public string ID { get; set; }
}

[Serializable]
[XmlRoot(ElementName = "Connection")]
public class Connection
{
    public Connection() { }
    [XmlAttribute(AttributeName = "From")]
    public string From { get; set; }
    [XmlAttribute(AttributeName = "FromPosX")]
    public int FromPosX { get; set; }
    [XmlAttribute(AttributeName = "FromPosY")]
    public int FromPosY { get; set; }
    [XmlAttribute(AttributeName = "ID")]
    public string ID { get; set; }
    [XmlAttribute(AttributeName = "to")]
    public string To { get; set; }
    [XmlAttribute(AttributeName = "toPosX")]
    public int toPosX { get; set; }
    [XmlAttribute(AttributeName = "toPosY")]
    public int toPosY { get; set; }
}
#endregion

#region forRoomFile

[Serializable]
//[XmlRoot(ElementName = "Room")]
public class newRoomModel
{
    public newRoomModel() { }
    [XmlAttribute(AttributeName = "ID")]
    public string ID { get; set; }
    [XmlAttribute(AttributeName = "height")]
    public int height { get; set; }
    [XmlAttribute(AttributeName = "width")]
    public int width { get; set; }

    //[XmlElement(ElementName = "Tile")]
    //public List<Tile> Tiles { get; set; }
    [XmlElement(ElementName = "Tiles")]
    public TileHolder Tiles { get; set; }
}


[Serializable]
[XmlRoot(ElementName = "Room")]
public class TileHolder
{
    public TileHolder() { }
    [XmlElement(ElementName = "Tile")]
    public List<Tile> tiles { get; set; }
}

[Serializable]
[XmlRoot(ElementName = "Tile")]
public class Tile
{
    [XmlAttribute("PosX")]
    public int X { get; set; }
    [XmlAttribute("PosY")]
    public int Y { get; set; }
    [XmlAttribute("value")]
    public TileType Type { get; set; }
}
#endregion


#region Old Loadmodel
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
#endregion
[Serializable]
public enum TileType
{
    FLOOR,
    WALL,
    TREASURE,
    ENEMY,
    DOOR,
    DOORENTER,
    HERO,
    ENEMY_BOSS,
    NONE
}