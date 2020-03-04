using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public Transform front;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float initialVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {
        /// Shoot when the current time is greater than the nextShotTime
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, front.position, front.rotation) as Projectile;
            newProjectile.SetSpeed(initialVelocity);
        }
    }
}
