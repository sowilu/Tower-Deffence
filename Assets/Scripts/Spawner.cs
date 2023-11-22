using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 1;
    public float count = 10;
    public float waitTime = 5;
    public Path path;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), waitTime, spawnRate);
    }

    void Spawn()
    {
        if (count == 0) return;

        var e = Instantiate(enemy, transform.position, Quaternion.identity);
        e.GetComponent<Enemy>().Path = path;

        count--;
    }
}
