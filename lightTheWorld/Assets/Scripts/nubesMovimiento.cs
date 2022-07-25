using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nubesMovimiento : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float deactivateTimer = 10f;

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
