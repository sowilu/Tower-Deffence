using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float range;
    public GameObject bullet;
    public float fireRate;
    public LayerMask towerMask;
    public Transform shootingPoint;

    void Start()
    {
        
    }

    void Update()
    {
        var towers = Physics.SphereCastAll(transform.position, range, transform.forward, towerMask);

        Transform target = null;
        if(towers != null && towers.Length > 0)
        {
            target = towers[0].transform;
        }

        print(target.name);

        if(target != null && !target.CompareTag(transform.tag))
        {
            Instantiate(bullet, shootingPoint.position, Quaternion.identity).GetComponent<Bullet>().target = target;
        }
    }

    public void Die()
    {
        //TODO: animations, vfx
        Destroy(gameObject);
    }
}
