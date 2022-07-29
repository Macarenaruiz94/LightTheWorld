using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int maxSalud = 2;
    int currentSalud;
    public Animator animator;
    bool isHurting, isDead;
    void Start()
    {
        currentSalud = maxSalud;
    }

    public void TakeDamage(int damage)
    {
        currentSalud -= damage;

        animator.SetTrigger("isTakingDamage");
        StartCoroutine("Hurt");

        if (currentSalud <= 0)
        {
            Die();
        }
    }

    IEnumerator Hurt()
    {
        isHurting = true;

        yield return new WaitForSeconds(0.5f);

        isHurting = false;
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("isDying");
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
