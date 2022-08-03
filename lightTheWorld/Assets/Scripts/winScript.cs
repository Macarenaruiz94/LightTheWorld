using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScript : MonoBehaviour
{
    [SerializeField] GameObject wintext;
    [SerializeField] GameObject boss;
    void Start()
    {
        wintext.SetActive(false);
    }

    private void Update()
    {
        ShowWin();
    }
    public void ShowWin()
    {
        if(!boss)
        {
            wintext.SetActive(true);
        }
    }
}
