using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int maxSalud = 2;
    int currentSalud;
    void Start()
    {
        currentSalud = maxSalud;
    }

    public void TakeDamage(int damage)
    {
        currentSalud -= damage;

        if(currentSalud <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
