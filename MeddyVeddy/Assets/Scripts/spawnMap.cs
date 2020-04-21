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
        string folderPath = Path.Combine(Application.dataPath, "Maps/" + "TestMap" + "/dungeon/" );
        string path = folderPath + "HopefullyRightFile.xml";
        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists)
        {
            LoadMapStatic.LoadMap(path, Dungeon, Room, folderPath);
        }else
        {
            Debug.Log(path + "                  ERROR PATH");
        }
        // LoadMapStatic.LoadMap(System.IO.Directory.GetCurrentDirectory() + "/Assets/Maps/testMap.xml");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
