using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nubesInstanciar : MonoBehaviour
{
    public GameObject[] nubes;
    public float instanciarTimer = 4f;
    private Vector2 screenBound;
    void Start()
    {
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(NubeWave());
    }

    void InstanciarNube()
    {
        GameObject a = Instantiate(nubes[Random.Range(0, nubes.Length)]) as GameObject;
        a.transform.position = new Vector2(screenBound.x * 2, Random.Range(-screenBound.y, screenBound.y));
    }

    IEnumerator NubeWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(instanciarTimer);
            InstanciarNube();
        }
    }
}
