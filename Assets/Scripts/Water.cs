using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject iceTile;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Freeze()
    {
        var go = Instantiate(iceTile, transform.position, transform.rotation);
        go.transform.localScale = transform.localScale;
        Destroy(this.gameObject);
    }
}
