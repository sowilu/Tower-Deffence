using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    float health;

    public UnityEvent<float, float, float> onDamage;
    public UnityEvent onDeath;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        onDamage.Invoke(health, damage, maxHealth);

        //print(gameObject.name + " took " + damage + " damage");

        if(health <= 0)
        {
            onDeath.Invoke();
        }
    }
}
