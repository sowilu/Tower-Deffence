using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 15f;
    public Transform rangeVisuals;
    public LayerMask enemyLayer;

    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform shootingPoint;

    private float lastShot;
    
    void Start()
    {
        rangeVisuals.localScale = Vector3.one * range * 2;
    }


    void Update()
    {
        if(lastShot + fireRate < Time.time)
        {
            lastShot = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        var hits = Physics.SphereCastAll(transform.position, range, transform.forward, enemyLayer);

         var target = FindNearest(hits);

        if(target != null)
        {
            var bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity).GetComponent<Bullet>();

            bullet.target = target;
        }
       
    }

    Transform FindNearest(RaycastHit[] hits)
    {
        if (hits.Length == 0) return null;

        var maxDistance = Vector3.Distance(transform.position, hits[0].transform.position);
        var target = hits[0].transform;

        foreach(var hit in hits)
        {
            if(maxDistance < hit.distance)
            {
                maxDistance = hit.distance;
                target = hit.transform;
            }
        }

        return target;
    }
}
