using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string KeyID = "unlocked";
    public GameObject TargetDoor = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryEnter(GameObject caller)
    {
        if (TargetDoor == null)
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

    }
}
