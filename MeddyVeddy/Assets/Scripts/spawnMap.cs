using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Windows;
using UnityEngine.UI;

public class spawnMap : MonoBehaviour
{
    public GameObject Room;
    public GameObject Dungeon;
    // Start is called before the first frame update
    void Start()
    {
        
        // LoadMapStatic.LoadMap(System.IO.Directory.GetCurrentDirectory() + "/Assets/Maps/testMap.xml");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("debug messages:");
    }

    public void SpawnMap(string input = "TestMap")
    {
        string folderPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Maps/" + input + "/dungeon/");
        Debug.Log(System.IO.Directory.GetCurrentDirectory());
        string path = folderPath + "Entry.xml";
        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists)
        {
            LoadMapStatic.LoadMap(path, Dungeon, Room, folderPath);
        } else
        {
            Debug.Log(path + "                  ERROR PATH");
        }
    }
}
