using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTree : MonoBehaviour
{
    public GameObject fuegos;
    public GameObject text;
    public void Start()
    {
        fuegos.SetActive(false);
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
    }
}
