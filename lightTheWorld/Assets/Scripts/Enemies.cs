using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public int maxSalud = 2;
    int currentSalud;
    bool isHurting, isDead;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public Transform target;
    private bool mirandoDerecha = true;
    private float timeBtwShots;
    public float startTimeBtwShots;
    void Start()
    {
        currentSalud = maxSalud;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            animator.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (timeBtwShots <= 0)
        {
            Atacar();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (speed > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (speed < 0 && mirandoDerecha)
        {
            Girar();
        }
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

    private void Atacar()
    {
        animator.SetTrigger("isAtacando");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<playerMovimiento>().TakeDamage(1);
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}