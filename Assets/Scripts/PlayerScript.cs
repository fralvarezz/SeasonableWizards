using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    Rigidbody2D rb;
    public float acceleration = .2f;
    public float maxSpeed = 0.2f;
    public float friction = 0.9f;

    public Vector2 velocity;
    public int hp = 3;
    public Vector3 playerStartPosition = new Vector3(0, 0, 0);

    public GameObject healthUiObject;
    public GameObject seasonTimerUi;


    private SoundController _soundController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _soundController = GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        velocity = new Vector3(h, v);

        velocity = velocity.normalized * acceleration;

        if (velocity.magnitude > 0.05f)
        {
            _soundController.PlayRandomSoundIfNone( "walk1", "walk2", "walk3", "walk4");
        }

        if (hp <= 0)
        {
            hp = 3;
            this.transform.position = playerStartPosition;
            seasonTimerUi.GetComponent<SliderScript>().ResetTimer();
            healthUiObject.GetComponent<HpUiScript>().ResetHearts();

        }
    }

    private void FixedUpdate()
    {

        this.rb.velocity += velocity;
        Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        this.rb.velocity *= friction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Bullet")
        {
            int dmgTaken = collision.GetComponent<Enemy>().DealDamage();
            hp -= dmgTaken;
            healthUiObject.GetComponent<HpUiScript>().RemoveHeart();
            _soundController.PlayRandomSound(false, "grunt1", "grunt2", "grunt3");
        }
    }

}
