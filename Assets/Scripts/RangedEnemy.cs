using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RangedEnemy : Enemy
{

    //public Transform player;
    public GameObject bulletPrefab;

    public float cooldown = 1f;
    public float aggroRange = 4f;

    float distanceToStop = 1.5f;
    float shootRange = 1.9f;
    float lastShot = 0;
    Rigidbody2D rb;
    SpriteRenderer sr;



    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyState = State.PASSIVE;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            enemyState = State.AGGRESIVE;
        }
        else
        {
            enemyState = State.PASSIVE;
        }

        lastShot -= Time.deltaTime;
        if (Vector3.Distance(transform.position, player.position) < shootRange && lastShot < 0.01)
        {
            shoot();
            transform.DOPunchScale(Vector3.one * 0.35f, 0.2f, 4, 1);
            lastShot = cooldown;
        }
    }

    private void FixedUpdate()
    {
        if (enemyState == State.AGGRESIVE && Vector3.Distance(transform.position, player.position) > distanceToStop)
        {
            float step = 0.4f * Time.fixedDeltaTime;
            //Vector3.MoveTowards(transform.position, player.position, step);
            //var dir = player.position - transform.position;
            Vector3 movePos = Vector3.MoveTowards(transform.position, player.position, step);
            rb.MovePosition(movePos);

        }
        rb.velocity *= 0.85f;
    }

    void shoot()
    {
        var go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 dir = player.position - transform.position;
        dir.x = Random.Range(dir.x - 0.3f, dir.x + 0.3f);
        dir.y = Random.Range(dir.y - 0.3f, dir.y + 0.3f);

        go.GetComponent<BulletScript>().setDir(dir.normalized);
    }
}
