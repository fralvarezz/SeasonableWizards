using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuice_Dust : MonoBehaviour
{
    ParticleSystem walkDust;
    Rigidbody2D playerRb;
    [SerializeField] bool runningDust = false;
    [SerializeField] float mangiuteGate = 0.2f;

    private void Start()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
        walkDust = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (playerRb.velocity.magnitude > mangiuteGate)
            runningDust = true;
        else
            runningDust = false;

        var emit = walkDust.emission;
        emit.enabled = runningDust;
    }
}
