using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastBoton : MonoBehaviour
{
    public GameObject fuegos;
    public GameObject text;
    public GameObject textSeguir;
    public void Start()
    {
        fuegos.SetActive(false);
        text.SetActive(false);
        textSeguir.SetActive(false);
    }

    public void LightTree()
    {
        fuegos.SetActive(true);
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        Destroy(text);
        textSeguir.SetActive(true);
    }
}
