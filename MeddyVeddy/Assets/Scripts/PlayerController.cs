using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    /// Look at the mouse position, with a height corrected point
    /// (in order to not look up into the sky)
    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
    }
   
}
