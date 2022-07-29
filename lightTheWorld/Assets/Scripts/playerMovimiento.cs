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
    private int Health = 5;
    public string sceneName;
    Rigidbody2D rb;
    public Animator animator;
    bool isHurting, isDead;
    private bool mirandoDerecha = true;
    private int item;
    public Text itemText;
    public GameObject textObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        textObject.SetActive(false);
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

        if (movimiento > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (movimiento < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f && !isDead && rb.velocity.y == 0)
        {
            Jump();
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
        if (Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Health -= 1;

            if (Health == 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(sceneName);
            }
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
