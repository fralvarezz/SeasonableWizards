using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPingPong : MonoBehaviour
{
    SineWave wave;
    void Start()
    {

        wave = GetComponent<SineWave>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1 + wave.sine, 1 + wave.sine, 1 + wave.sine);
    }
}
