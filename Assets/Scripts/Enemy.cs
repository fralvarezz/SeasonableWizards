using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public int _health;
    public Transform player;

    protected enum State
    {
        PASSIVE,
        AGGRESIVE
    }

    protected State enemyState;
    public int damage;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void TakeDamage(int dmg)
    {
        _health -= dmg;
        BopEnemy();
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual int DealDamage()
    {
        return damage;
    }

    void BopEnemy()
    {
        float rng = Random.Range(0.25f, 0.3f);
        if (!DOTween.IsTweening(transform))
            transform.DOPunchScale(Vector3.one * rng, 0.225f, 5, 1);
    }

    public virtual void SetPlayer(Transform p)
    {
        player = p;
    }
}
