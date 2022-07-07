using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindConeScript : MonoBehaviour
{

    List<Collider2D> colliders = new List<Collider2D>();

    Vector3 wandPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        foreach(var col in colliders)
        {
            wandPos = this.transform.parent.position;
            Vector3 dir = col.transform.position - wandPos;
            col.GetComponent<Rigidbody2D>().AddForce(dir * 50f);
            
            if (col.gameObject.CompareTag("Mirror") == true)
            {
                int d = 1;                                  // 1 = north, 2 = east, 3 = south, 4 = west
                if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))    // y is larger
                {
                    if (Mathf.Sign(dir.y) == 1) d = 1;
                    else d = 3;
                }
                else                                        // x is larger
                {
                    if (Mathf.Sign(dir.x) == 1) d = 2;
                    else d = 4;
                }
                col.gameObject.GetComponent<Mirror>().MoveOnRail(d);
            }
            
            
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Moveable" || collision.tag == "Enemy" || collision.tag == "Mirror")
        {
            colliders.Add(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision);
    }
}
