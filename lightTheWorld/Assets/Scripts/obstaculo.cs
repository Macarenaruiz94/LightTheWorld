using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculo : MonoBehaviour
{
    public int maxSalud = 1;
    int currentSalud;
    public void TakeDamage(int damage)
    {
        currentSalud -= damage;

        if (currentSalud <= 0)
        {
            Destroy(gameObject);
        }
    }
}
