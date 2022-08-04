using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
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
    [SerializeField] float agroRange;
    Rigidbody2D rb;
    public GameObject textGanar;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSalud = maxSalud;
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, target.position);

        if (distToPlayer < agroRange)
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

            LookPlayer();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void LookPlayer()
    {
        if ((target.position.x > transform.position.x && !mirandoDerecha) || (target.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
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
}
