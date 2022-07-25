using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollFondo : MonoBehaviour
{
    private void Update()
    {
        transform.position += new Vector3(-5 * Time.deltaTime, 0);

        if(transform.position.x < -18.31)
        {
            transform.position = new Vector3(18.31f, transform.position.y);
        }
    }
}
