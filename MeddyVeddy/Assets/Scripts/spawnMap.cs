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
        string path = Path.Combine(Application.dataPath, "Maps/" + "testMap.xml");
        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists)
        {
            //Debug.Log("funkar");
            LoadMapStatic.LoadMap(path, Dungeon, Room);
        }
        // LoadMapStatic.LoadMap(System.IO.Directory.GetCurrentDirectory() + "/Assets/Maps/testMap.xml");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
