using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 15f;
    public Transform rangeVisuals;

    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform shootingPoint;

    float lastShot;
    List<Transform> enemies = new();
    SphereCollider rangeArea;

    void Start()
    {
        rangeArea = GetComponent<SphereCollider>();
        rangeArea.radius = range;
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
         var target = FindNearest();

        if(target != null)
        {
            var bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity).GetComponent<Bullet>();

            bullet.target = target;
        }
       
    }

    Transform FindNearest()
    {
        if (enemies.Count == 0) return null;

        var maxDistance = Vector3.Distance(transform.position, enemies[0].transform.position);
        var target = enemies[0].transform;

        foreach(var hit in enemies)
        {
            var distance = Vector3.Distance(transform.position, hit.transform.position);

            if(maxDistance < distance)
            {
                maxDistance = distance;
                target = hit.transform;
            }
        }

        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && enemies.Contains(other.transform))
        {
            enemies.Remove(other.transform);
        }
    }

    public void FallDown()
    {
        //TODO: vfx explode into parts
        Destroy(gameObject);
    }
}
