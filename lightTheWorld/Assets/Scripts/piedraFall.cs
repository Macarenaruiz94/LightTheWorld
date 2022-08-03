using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piedraFall : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
        }
    }
}
