using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anotherBoton : MonoBehaviour
{
    public GameObject textSeguir;

    private void Start()
    {
        textSeguir.SetActive(false);
    }
    public void DestroyText()
    {
        Destroy(textSeguir);
    }
}
