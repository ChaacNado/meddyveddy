using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string KeyID = "unlocked";
    public GameObject TargetRoom = null;
    public int targetRoomID;
    public Vector2 posOfTarget;
    public bool doorEnabled = true;
    double timeLeft = 2;
    public string XmlTargetId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            TryEnter(other.gameObject);
    }

    public void TryEnter(GameObject caller)
    {
        if (caller.GetComponent<Player>().disableNextDoor)
        {
            caller.GetComponent<Player>().disableNextDoor = false;
            doorEnabled = false;
        }
        if (TargetRoom == null)
            return;
        if (KeyID.Equals("unlocked") && doorEnabled)
            Enter(caller);
        else
        {
            if (caller.GetComponent<Player>().Keys.Contains(KeyID) && doorEnabled)
            {
                Enter(caller);
            }
        }
    }

    public void Enter(GameObject caller)
    {
        TargetRoom.SetActive(true);
        caller.GetComponent<Player>().currentRoomID = TargetRoom.GetComponent<createRoom>().RoomID;
        //Vector3 targetPos = new Vector3(TargetRoom.transform.position.x + posOfTarget.x/2,0, TargetRoom.transform.position.z + posOfTarget.y/2);
        //Vector3 targetPos = new Vector3((posOfTarget.x + TargetRoom.transform.position.x) - (int)(TargetRoom.GetComponent<createRoom>().roomSize.x / 2), 1, 
        //                                (posOfTarget.y + TargetRoom.transform.position.z)  - ((int)(TargetRoom.GetComponent<createRoom>().roomSize.y / 2)));
        //Debug.Log(posOfTarget);
        
        //foreach(Vector2 v in TargetRoom.GetComponent<createRoom>().doorsIndexed.Keys)
        //{
        //    Debug.Log("     " + v);
        //}
        Vector3 targetPos = new Vector3(TargetRoom.transform.position.x + TargetRoom.GetComponent<createRoom>().doorsIndexed[posOfTarget].x,
                                        1,
                                        TargetRoom.transform.position.z + TargetRoom.GetComponent<createRoom>().doorsIndexed[posOfTarget].z);
        caller.transform.position = targetPos;
        //Debug.Log(posOfTarget + "         " + targetPos);

        caller.GetComponent<Player>().disableNextDoor = true;
    }

    public void Timer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
            doorEnabled = true;
    }
}
