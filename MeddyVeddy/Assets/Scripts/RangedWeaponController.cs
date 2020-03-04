using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    public Transform weaponHold;
    public RangedWeapon startingWeapon;
    RangedWeapon equippedRw;

    void Start()
    {
        if (startingWeapon != null)
        {
            EquipWeapon(startingWeapon);
        }
    }

    public void EquipWeapon(RangedWeapon rwToEquip)
    {
        if (equippedRw != null)
        {
            Destroy(equippedRw.gameObject);
        }
        equippedRw = Instantiate(rwToEquip, weaponHold.position, weaponHold.rotation) as RangedWeapon;
        equippedRw.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedRw != null)
        {
            equippedRw.Shoot();
        }
    }
}
