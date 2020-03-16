using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string KeyID = "unlocked";
    public GameObject TargetRoom = null;
    public int targetRoomID;
    public Vector2 posOfTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        TryEnter(other.gameObject);
    }

    public void TryEnter(GameObject caller)
    {
        if (TargetRoom == null)
            return;
        if (KeyID.Equals("unlocked"))
            Enter(caller);
        else
        {
            if (caller.GetComponent<Player>().Keys.Contains(KeyID))
            {
                Enter(caller);
            }
        }
    }

    public void Enter(GameObject caller)
    {
        TargetRoom.SetActive(true);
        caller.GetComponent<Player>().currentRoomID = TargetRoom.GetComponent<createRoom>().RoomID;
        Vector3 targetPos = new Vector3(TargetRoom.transform.position.x + posOfTarget.x,0, TargetRoom.transform.position.z + posOfTarget.y);
        caller.transform.position = targetPos;
    }
}
