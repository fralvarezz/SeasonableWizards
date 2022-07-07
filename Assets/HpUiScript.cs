using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUiScript : MonoBehaviour
{
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;

    private int toRemove;
    // Start is called before the first frame update
    void Start()
    {
        toRemove = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHeart()
    {
        if(toRemove == 3)
        {
            hp3.SetActive(false);
            toRemove--;
        } else if(toRemove == 2)
        {
            hp2.SetActive(false);
            toRemove--;
        }
        else if (toRemove == 1)
        {
            hp1.SetActive(false);
            toRemove--;
        }
    }

    public void ResetHearts()
    {
        hp1.SetActive(true);
        hp2.SetActive(true);
        hp3.SetActive(true);
        toRemove = 3;
    }

}
