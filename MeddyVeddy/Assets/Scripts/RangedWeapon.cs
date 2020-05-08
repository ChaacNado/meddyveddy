using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootStyle { Auto, Burst, Spread }

public class RangedWeapon : MonoBehaviour
{
    public ShootStyle shootStyle;

    public Transform front;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float initialVelocity = 35;
    public float burstInterval = 0.2f;
    public int bulletAmount = 6;
    public float spread = 30f;

    float nextShotTime;
    bool triggerReleasedSinceLastShot;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            if (shootStyle == ShootStyle.Auto)
            {
                SingleShot();
            }
            else if (shootStyle == ShootStyle.Burst)
            {
                BurstFire();
            }
            else if (shootStyle == ShootStyle.Spread)
            {
                SpreadShot();
            }
        }
    }

    void SingleShot()
    {
        /// Shoot when the current time is greater than the nextShotTime
        nextShotTime = Time.time + msBetweenShots / 1000;
        Projectile newProjectile = Instantiate(projectile, front.position, front.rotation) as Projectile;
        newProjectile.SetSpeed(initialVelocity);
    }

    void BurstFire()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            Invoke("SingleShot", i * burstInterval);
        }
        //Invoke("SingleShot", burstInterval + burstInterval);
    }

    void SpreadShot()
    {
        /// Shoot when the current time is greater than the nextShotTime
        nextShotTime = Time.time + msBetweenShots / 1000;
        for (int i = 0; i < bulletAmount; i++)
        {
            Projectile newProjectile = Instantiate(projectile, front.position, front.rotation * Quaternion.Euler(0, Random.Range(-spread, spread), 0)) as Projectile;
            newProjectile.SetSpeed(initialVelocity);
        }
    }

    public void OnTriggerHold()
    {
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;
    }
}
