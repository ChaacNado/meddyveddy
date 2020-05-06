using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    public Transform weaponPos;
    public RangedWeapon weapon;
    RangedWeapon equippedRw;

    void Start()
    {
        if (weapon != null)
        {
            EquipWeapon(weapon);
        }
    }

    public void EquipWeapon(RangedWeapon rwToEquip)
    {
        if (equippedRw != null)
        {
            Destroy(equippedRw.gameObject);
        }
        equippedRw = Instantiate(rwToEquip, weaponPos.position, weaponPos.rotation) as RangedWeapon;
        equippedRw.transform.parent = weaponPos;
    }

    public void OnTriggerHold()
    {
        if (equippedRw != null)
        {
            equippedRw.OnTriggerHold();
        }
    }

    public void OnTriggerRelease()
    {
        if (equippedRw != null)
        {
            equippedRw.OnTriggerRelease();
        }
    }
}
