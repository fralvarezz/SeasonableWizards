using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletScript : Enemy
{

    Vector3 direction;
    public float bulletSpeed = 5.5f;
    // Start is called before the first frame update
    protected override void Start()
    {
        transform.DOPunchScale(Vector3.one * 0.15f, 0.25f, 4, 1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    public void setDir(Vector3 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    public override int DealDamage()
    {
        return base.DealDamage();
    }
}
