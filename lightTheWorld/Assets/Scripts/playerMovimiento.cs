using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovimiento : MonoBehaviour
{
    public float speed;
    public float jump;
    public float movimiento;
    public int maxHealth = 10;
    private int currentHealth;
    Rigidbody2D rb;
    public Animator animator;
    bool isHurting, isDead;
    private bool mirandoDerecha = true;
    private int item;
    public Text itemText;
    public GameObject textObject;
    public GameObject textGanar;
    public GameObject textPerder;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask obstaculosLayer;
    public GameObject fuegos;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        textObject.SetActive(false);
        textGanar.SetActive(false);
        textPerder.SetActive(false);
        fuegos.SetActive(false);
    }
    void Update()
    {
        Movement();
        SetAnimationState();
    }

    private void FixedUpdate()
    {
        if (!isHurting)
            rb.velocity = new Vector2(movimiento, rb.velocity.y);
    }

    private void Movement()
    {
        movimiento = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movimiento, 0, 0) * Time.deltaTime * speed;

        if (movimiento > 0 && !mirandoDerecha && !isDead)
        {
            Girar();
        }
        else if (movimiento < 0 && mirandoDerecha && !isDead)
        {
            Girar();
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f && !isDead && rb.velocity.y == 0)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isDead)
        {
            Atacar();
        }

        if (!isDead)
            movimiento = Input.GetAxisRaw("Horizontal") * speed;
    }
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.001f && !isDead)
        {
            rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
    }

    private void Atacar()
    {
        animator.SetBool("isAtacando", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemies>().TakeDamage(1);
        }

        Collider2D[] hitObstaculos = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, obstaculosLayer);

        foreach (Collider2D obstaculo in hitObstaculos)
        {
            obstaculo.GetComponent<obstaculo>().TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("isTakingDamage");
        StartCoroutine("Hurt");

        if (currentHealth == 0)
        {
            isDead = true;
            animator.SetTrigger("isDying");
            textPerder.SetActive(true);
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            item++;
            itemText.text = "Fire: " + item;

            if(item == 10) { textObject.SetActive(true); }
        }
    }

    IEnumerator Hurt()
    {
        isHurting = true;
        rb.velocity = Vector2.zero;

        if (mirandoDerecha)
            rb.AddForce(new Vector2(-200f, 200f));
        else
            rb.AddForce(new Vector2(200f, 200f));

        yield return new WaitForSeconds(0.5f);

        isHurting = false;
    }

    void SetAnimationState()
    {
        if (movimiento == 0)
        {
            animator.SetBool("isRunning", false);
        }

        if (rb.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }

        if (Mathf.Abs(movimiento) == 5 && rb.velocity.y == 0)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

        if (rb.velocity.y > 0)
            animator.SetBool("isJumping", true);

        if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
    }
}
