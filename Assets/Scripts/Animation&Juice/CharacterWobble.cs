using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterWobble : MonoBehaviour
{
    [SerializeField] Transform playerSprite;
    [SerializeField] SineWave wave;
    Rigidbody2D playerRb;
    [SerializeField] bool alwaysWobble = false;
    private void Start()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float _wave = wave.sine;
        if (playerRb.velocity.magnitude > 0.2f || alwaysWobble)
            playerSprite.transform.rotation = Quaternion.Euler(0, 0, _wave);
        else
            playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
