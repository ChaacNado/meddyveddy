using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Windows;

public class spawnMap : MonoBehaviour
{
    public GameObject Room;
    public GameObject Dungeon;
    // Start is called before the first frame update
    void Start()
    {
        string path = Path.Combine(Application.dataPath, "Maps/" + "0321f461-81c0-43b3-a72a-2921f20863dd" + "/dungeon/" + "dungeon-94fb1bfa-fd09-40a9-bf57-00fa9c5b3af4_1.xml");
        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists)
        {
            LoadMapStatic.LoadMap(path, Dungeon, Room);
        }
        // LoadMapStatic.LoadMap(System.IO.Directory.GetCurrentDirectory() + "/Assets/Maps/testMap.xml");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
