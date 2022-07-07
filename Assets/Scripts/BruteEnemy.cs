using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BruteEnemy : Enemy
{
    private Rigidbody2D rb2d;

    public float moveSpeed;

    //public Transform player;

    private Vector2 _movement;
    private Vector3 _randomness;

    [Range(0.1f, 0.8f)]
    public float closeToAttack;
    private Vector3 _closeToAttack;

    private bool _isAttacking = false;
    private CircleCollider2D _aoe;

    public bool wasHit = false;
    private float hitTimer = 0.2f;

    public Animator animator;

    public float aggroRange = 4f;

    private SoundController _soundController;

    private bool _shouldDie;
    private float _dieTimer = 1.0f;


    // Start is called before the first frame update
    protected override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _randomness = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(0.25f, 0.25f));

        _closeToAttack = new Vector3(closeToAttack, closeToAttack);
        _aoe = this.GetComponent<CircleCollider2D>();

        enemyState = State.PASSIVE;

        _soundController = GetComponent<SoundController>();
        
        /*
        _soundController.MakeSpatial(0.3f,0.8f,"move1");
        _soundController.MakeSpatial(0.3f,0.8f,"move2");
        _soundController.MakeSpatial(0.3f,0.8f,"move3");
        */
        _soundController.MakeSpatial(1f,2f,"move1");
        _soundController.MakeSpatial(1f,2f,"move2");
        _soundController.MakeSpatial(1f,2f,"move3");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!_shouldDie)
        {
            if (Vector3.Distance(transform.position, player.position) < aggroRange)
            {
                enemyState = State.AGGRESIVE;
            }
            else
            {
                enemyState = State.PASSIVE;
            }

            //find out vector to player position
            Vector3 direction = player.position + _randomness - transform.position;

            //move character
            direction.Normalize();
            _movement = direction;

            //check attack threshold
            Vector3 directionAttack = player.position - transform.position;
            directionAttack.x = Mathf.Abs(directionAttack.x);
            directionAttack.y = Mathf.Abs(directionAttack.y);

            if (directionAttack.x < _closeToAttack.x && directionAttack.y < _closeToAttack.y & !_isAttacking)
            {
                StartCoroutine(Atk());
            }

            if (wasHit)
            {
                hitTimer -= Time.deltaTime;
                if (hitTimer <= 0)
                {
                    wasHit = false;
                }
            }
        }
        else
        {
            _dieTimer -= Time.deltaTime;
            if (_dieTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }

    }

    private void FixedUpdate()
    {
        if (!_isAttacking && !wasHit && enemyState == State.AGGRESIVE)
        {
            MoveCharacter(_movement);
        }

    }

    void MoveCharacter(Vector2 direction)
    {
        rb2d.MovePosition((Vector2)transform.position + (direction * (moveSpeed * Time.deltaTime)));
        _soundController.PlayRandomSoundIfNone("move1", "move2", "move3");
    }

    IEnumerator Atk()
    {
        _isAttacking = true;

        animator.SetBool("IsAttacking", true);

        //Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 2, 0);
        yield return new WaitForSeconds(1f);
        _aoe.enabled = true;

        yield return new WaitForSeconds(0.2f);

        _aoe.enabled = false;

        //var cubeRenderer = this.gameObject.GetComponent<Renderer>();

        animator.SetBool("IsAttacking", false);
        _isAttacking = false;
    }

    public override void TakeDamage(int dmg)
    {
        wasHit = true;
        hitTimer = 0.35f;
        _health -= dmg;
        if (_health <= 0)
        {
            int randomSoundIndex = Random.Range(1, 2);
            _soundController.Stop("hit" + 1);
            _soundController.Stop("hit" + 2);
            _soundController.Play("die" + randomSoundIndex, 1);
            _shouldDie = true;

            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            int randomSoundIndex = Random.Range(1, 2);
            _soundController.PlayIfNotPlaying("hit" + randomSoundIndex, 1);
        }
    }
}
