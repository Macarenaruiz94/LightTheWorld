using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anotherBoton : MonoBehaviour
{
    public GameObject textSeguir;
    public void DestroyText()
    {
        Destroy(textSeguir);
    }
}
