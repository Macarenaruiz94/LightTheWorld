using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nubesMovimiento : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public float deactivateTimer = 4f;

    void Start()
    {
        Invoke("DeactivateNube", deactivateTimer);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    void DeactivateNube()
    {
        gameObject.SetActive(false);
    }
}
