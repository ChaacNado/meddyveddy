using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootStyle { Single, Burst }

public class RangedWeapon : MonoBehaviour
{
    public ShootStyle shootStyle;

    public Transform front;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float initialVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {
        if (shootStyle == ShootStyle.Single)
        {
            SingleShot();
        }
        else if (shootStyle == ShootStyle.Burst)
        {
            BurstFire();
        }
    }

    void SingleShot()
    {
        /// Shoot when the current time is greater than the nextShotTime
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, front.position, front.rotation) as Projectile;
            newProjectile.SetSpeed(initialVelocity);
        }
    }

    void BurstFire()
    {
        SingleShot();
        Invoke("SingleShot", 0.2f);
        Invoke("SingleShot", 0.4f);
    }
}
