using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableScript : MonoBehaviour
{

    Rigidbody2D rb;
    public float friction = 0.85f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        this.rb.velocity *= friction;
    }
}
