using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public void UpdateHealthBar(float health, float damage, float maxHealth)
    {
        transform.localScale = new Vector3(health / maxHealth, 1, 1);
    }
}
