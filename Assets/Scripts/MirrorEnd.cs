using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEnd : MonoBehaviour
{

    float _timer = 1f;
    float _castDuration = 0.02f;

    public Door door;

    void Start()
    {

    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer <= _castDuration * 2f)
        {
            door.setDoor(true);
        }
        else
        {
            door.setDoor(false);
        }
    }

    public void ReflectRay(int chain)
    {
        if (chain > 5) return;
        _timer = 0.0f;
    }
}
