using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoreCheck : MonoBehaviour
{
    private PolygonCollider2D _collider2D;
    public int expectedNumBalls;
    private int numBalls;
    private bool puzzleFinished;

    public Sprite[] signSprites;
    private SignObelisk signObelisk;

    private GameObject sign;

    private SoundController _soundController;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider2D = GetComponent<PolygonCollider2D>();
        sign = this.transform.GetChild(0).gameObject;
        sign.GetComponent<SpriteRenderer>().sprite = signSprites[numBalls];
        _soundController = GetComponent<SoundController>();
        signObelisk = GetComponent<SignObelisk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numBalls == expectedNumBalls)
        {
            puzzleFinished = true;
            //turn on totem or something idk
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!puzzleFinished)
        {
            if (other.gameObject.CompareTag("Moveable"))
            {
                //change color
                numBalls++;
                sign.GetComponent<SpriteRenderer>().sprite = signSprites[numBalls];
                _soundController.Play("puzzlecompleted",1);
                signObelisk.Activate();
            }
        }
    }
    
}
