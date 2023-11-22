using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public Path Path;
    public float speed = 3;
    public float stoppingDistance = 0.3f;

    [Header("Shooting")]
    public float range;
    public GameObject bullet;
    public float fireRate = 1;
    public Transform shootingPoint;

    [Header("Loot")]
    public Vector2Int loot = new(10, 20);
    public ParticleSystem lootVFX;

    SphereCollider rangeArea;
    List<Transform> towers = new();
    float lastShot;
    int pathIndex = 0;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rangeArea = GetComponent<SphereCollider>();
        rangeArea.radius = range;

        agent.speed = speed;
        //agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        if (pathIndex >= Path.waypoints.Count) return;

        var target = Path.waypoints[pathIndex].position;

        if (agent.remainingDistance < stoppingDistance) pathIndex++;
        //print(target + "---" + agent.remainingDistance);
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        agent.SetDestination(target);
    }

    void Attack()
    {
        Transform target = null;
        if (towers != null && towers.Count > 0)
        {
            target = towers[0].transform;
        }


        if (target != null && !target.CompareTag(transform.tag) && Time.time > lastShot + fireRate)
        {
            Instantiate(bullet, shootingPoint.position, Quaternion.identity).GetComponent<Bullet>().target = target;

            lastShot = Time.time;
        }
    }

    public void Die()
    {
        //TODO: animations, vfx
        var money = Random.Range(loot.x, loot.y);
        MoneyCounter.instance.Money += money;

        Instantiate(lootVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            towers.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            towers.Remove(other.transform);
        }
    }
}
