using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineProjectile : MonoBehaviour
{

    public float projectileSpeed = 2f;

    Vector2 tar;
    bool hasCollided = false;
    bool hasHitEnemy = false;
    Vector2 colliderSize;
    public int projectileDamage = 30;

    // Start is called before the first frame update
    void Start()
    {
        //col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, tar) > 0.001f && !hasCollided)
        {
            float step = projectileSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, tar, step);
        }

    }

    public void SetTarget(Vector2 target)
    {
        tar = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.tag == "Grabable")
        {
            hasCollided = true;
            //Debug.Log("Collision");
            transform.position = collision.gameObject.transform.position;
            //colliderSize.x = collision.size;
            colliderSize = collision.bounds.size;
        }
        if (collision.tag == "Enemy") {
            //Debug.Log("hit enemy");
            hasHitEnemy = true;
            collision.GetComponent<Enemy>().TakeDamage(projectileDamage);
        }
    }

    public bool GetCollision()
    {
        return hasCollided;
    }

    public Vector2 GetColliderSize()
    {
        return colliderSize;
    }

    public bool hitEnemy()
    {
        return hasHitEnemy;
    }

}
