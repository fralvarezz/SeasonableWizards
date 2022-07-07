using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraScript : MonoBehaviour
{

    Vector3 moveTowards = new Vector3(5f, 10f, 0);
    //float moveSpeed = 0.0005f;
    Vector3 direction;
    Vector3 velocity = Vector3.zero;
    SineWave wave;
    // Start is called before the first frame update
    void Start()
    {
        wave = GetComponent<SineWave>();
        direction = new Vector3(0, 0, 0);
        direction += transform.right;
        direction += transform.up * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, moveTowards, 0.0001f);
        //transform.position += moveTowards * moveSpeed;
        transform.rotation = Quaternion.Euler(0, 0, wave.sine);
    }
}
