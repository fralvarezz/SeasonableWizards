using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSpell : GenericSpell
{
    public GameObject projectilePrefab;
    public float colliderXOffset;
    public float colliderYOffset;

    private SoundController _soundController;

    GameObject projectile;
    public Transform wand;
    Transform wandTip;
    bool isShooting = false;
    bool isSlinging = false;
    bool seasonOver = false;


    float startTime;
    float journeyLength = 0.8f;

    Vector3 destination;



    LineRenderer line;

    // Start is called before the first frame update
    protected override void Start()
    {

        //wand = GetComponentInChildren<WandScript>().transform;
        wandTip = wand.GetChild(0);
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        //line.startWidth = 0.2f;
        //line.endWidth = 0.2f;
        //line.startColor = Color.green;
        //line.endColor = Color.green;
        _soundController = GetComponent<SoundController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(isShooting && projectile.GetComponent<VineProjectile>().hitEnemy())
        {
            isShooting = false;
            Destroy(projectile);
            line.enabled = false;
            _soundController.Play("vine_contact", 1);
        }
        if (!isShooting && Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            shoot();
            _soundController.Play("vine_no_contact",1);
            line.enabled = true;

        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isShooting)
            {
                isShooting = false;
                Destroy(projectile);
                line.enabled = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isShooting)
            {
                if (projectile.GetComponent<VineProjectile>().GetCollision())
                {
                    Vector2 collisionSize = projectile.GetComponent<VineProjectile>().GetColliderSize();
                    destination = projectile.transform.position;
                    if (destination.x > transform.position.x)
                    {
                        destination.x -= collisionSize.x + 0.05f;
                    }
                    else
                    {
                        destination.x += collisionSize.x + 0.05f;

                    }
                    if (destination.y > transform.position.y)
                    {
                        destination.y -= collisionSize.y + 0.05f;
                    }
                    else
                    {
                        destination.y += collisionSize.y + 0.05f;
                    }
                    startTime = Time.time;
                    isSlinging = true;
                    _soundController.PlayIfNotPlaying("vine_contact",1);
                }


                isShooting = false;
                Destroy(projectile);
                line.enabled = false;
            }

        }



        if (projectile != null)
        {
            line.SetPosition(0, wandTip.transform.position);
            line.SetPosition(1, projectile.transform.position);
        }
        //Ray2D ray = new Ray2D(wandTip.position, wand.transform.right * 10000.0f);
        //Physics2D.Raycast(ray.origin, ray.direction);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (isSlinging)
        {
            float distCovered = (Time.time - startTime) * 5.0f;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, destination, fractionOfJourney);
            if (Vector3.Distance(transform.position, destination) < 0.001f)
            {
                isSlinging = false;
            }
        }

        if (seasonOver)
        {
            if(!isSlinging)
            {
                isShooting = false;
                Destroy(projectile);
                line.enabled = false;
                this.enabled = false;
            }
        }
    }

    void shoot()
    {
        projectile = Instantiate(projectilePrefab, new Vector3(wand.transform.position.x, wand.transform.position.y, wand.transform.position.z), wand.transform.rotation);
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;
        projectile.GetComponent<VineProjectile>().SetTarget(mousePos);
    }

    public override void finishSpell()
    {
        seasonOver = true;
    }

    private void OnEnable()
    {
        seasonOver = false;
    }

}
