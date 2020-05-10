using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootStyle { Auto, Burst, AngledBurst, Spread }

public class RangedWeapon : MonoBehaviour
{
    public ShootStyle shootStyle;

    public Transform front;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float initialVelocity = 35;
    public float burstInterval = 0.2f;
    public int bulletAmount;
    public float spread;

    float nextShotTime;
    bool triggerReleasedSinceLastShot;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            if (shootStyle == ShootStyle.Auto)
            {
                StableSingleShot();
            }
            else if (shootStyle == ShootStyle.Burst)
            {
                BurstFire();
            }
            else if (shootStyle == ShootStyle.AngledBurst)
            {
                BurstFireWithRandomAngle();
            }
            else if (shootStyle == ShootStyle.Spread)
            {
                SpreadShot();
            }
        }
    }

    void StableSingleShot()
    {
        /// Shoot when the current time is greater than the nextShotTime
        nextShotTime = Time.time + msBetweenShots / 1000;
        Projectile newProjectile = Instantiate(projectile, front.position, front.rotation) as Projectile;
        newProjectile.SetSpeed(initialVelocity);
    }

    void SingleShotWithRandomAngle()
    {
        /// Shoot when the current time is greater than the nextShotTime
        nextShotTime = Time.time + msBetweenShots / 1000;
        Projectile newProjectile = Instantiate(projectile, front.position, front.rotation * Quaternion.Euler(0, Random.Range(-spread, spread), 0)) as Projectile;
        newProjectile.SetSpeed(initialVelocity);
    }


    void BurstFire()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            Invoke("StableSingleShot", i* burstInterval);
        }
        //Invoke("SingleShot", burstInterval + burstInterval);
    }

    void BurstFireWithRandomAngle()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            Invoke("SingleShotWithRandomAngle", i * burstInterval);
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
