using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float size = .5f;
    public float maxSize = 2f;
    private Rigidbody2D rb2d;
    private bool isMoving;

    private bool hit = false;
    public float timeToDisappearOnHit;
    private float hitTimer;

    public float minDamage;
    public float maxDamage;
    private float actualDamage;
    
    private CircleCollider2D _collider2D;

    private SoundController _soundController;
    //private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<CircleCollider2D>();
        _collider2D.enabled = false;
        _soundController = GetComponent<SoundController>();
        //_audioSource = GetComponent<AudioSource>();
        actualDamage = minDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            DiminishVelocitySize();
        }
    }

    public void AddSize(float deltaSize)
    {
        if (size < maxSize)
        {
            size += (deltaSize * (2.0f/3.0f));
            actualDamage += ((maxDamage / minDamage) * deltaSize);
        }

        if (size > maxSize)
        {
            size = maxSize;
            actualDamage = maxDamage;
        }
        this.transform.localScale = new Vector3(size, size);
    }

    private void DiminishVelocitySize()
    {
        size -= Time.deltaTime/2;
        transform.localScale = new Vector3(size, size);
        rb2d.velocity *= 0.985f;

        if (size <= 0.2)
        {
            Destroy(this.gameObject);
        }
    }

    public void Throw(Vector3 direction)
    {
        isMoving = true;
        rb2d.velocity = direction;
        _collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //_audioSource.Play();
            other.gameObject.GetComponent<Enemy>().TakeDamage((int)actualDamage);
            
            //other.gameObject.GetComponent<Rigidbody2D>().AddForce(rb2d.velocity*10);
            
            _soundController.Play("SNOWBALL-Contact",1);


            hit = true;
            hitTimer = timeToDisappearOnHit;

            rb2d.velocity /= 5.0f;
            
            //Debug.Log("Hit");
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            other.gameObject.GetComponent<Water>().Freeze();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Mirror"))
        {

            Vector3 dir = rb2d.velocity.normalized; //(other.transform.position - transform.position).normalized;

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

            other.gameObject.GetComponent<Mirror>().MoveOnRail(d);

            Destroy(this.gameObject);
        }
    }
    
}
