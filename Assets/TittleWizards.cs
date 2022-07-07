using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TittleWizards : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public Transform target;
    public float timeToMove = 10f;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.DOMove(target.position, timeToMove).SetEase(Ease.InOutQuad);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
