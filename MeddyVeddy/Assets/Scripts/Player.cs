using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(RangedWeaponController))]
public class Player : LivingEntity 
{
    public float moveSpeed = 5;
    //public List<string> Keys = new List<string>();
    Camera viewCamera;
    PlayerController controller;
    RangedWeaponController rwController;
    public int currentRoomID = 0;
    public bool disableNextDoor = false;
    public int points = 0;
    public List<int> Keys = new List<int>();

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        rwController = GetComponent<RangedWeaponController>();
        viewCamera = Camera.main;
    }

    
    void Update()
    {
        // Pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Break();
        }

        /// Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        /// Look input
        #region Raycast from viewCamera to groundPlane
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin, point, Color.red); /* Visualize the raycast*/
            controller.LookAt(point);
        }
        #endregion

        /// Weapon input
        if (Input.GetMouseButton(0))/* Left mouse btn held down */
        {
            rwController.OnTriggerHold();
        }
        if (Input.GetMouseButtonUp(0))
        {
            rwController.OnTriggerRelease();
        }
    }

    public float GetHP()
    {
        return health;
    }

}
