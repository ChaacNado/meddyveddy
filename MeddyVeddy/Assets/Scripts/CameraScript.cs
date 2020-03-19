using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public int currentRoom = 0;
    public GameObject player;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = LoadMapStatic.player;
        }
        else if (currentRoom != player.GetComponent<Player>().currentRoomID)
        {
            currentRoom = player.GetComponent<Player>().currentRoomID;
            transform.position = new Vector3(startPos.x + (currentRoom * LoadMapStatic.roomOffsetGlobal.x), startPos.y, startPos.z 
                + (currentRoom * LoadMapStatic.roomOffsetGlobal.y));
        }
    }
}
