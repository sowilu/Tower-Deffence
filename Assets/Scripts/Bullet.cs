using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 30f;
    public Vector2 damage = new(5, 10);

    void Start()
    {
        transform.LookAt(target.position);
    }

    
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            var d = Random.Range(damage.x, damage.y);
            health.TakeDamage(d);
        }
        Destroy(gameObject);
    }
}
