using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    float speed;
    public float damage = 1;

    float lifeTime = 3;
    float rayExtendingLength = 0.1f;  /* Used to extend the length of the ray, in order to hit quick moving objects effectively */

    void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        /// If the ray is hitting that object...
        if (Physics.Raycast(ray, out hit, moveDistance + rayExtendingLength, collisionMask, QueryTriggerInteraction.Collide))
        /* QueryTriggerInteraction allows us to set whether or not this will collide with trigger-colliders */
        {
            OnHitObject(hit.collider, hit.point);
        }
    }

    void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
