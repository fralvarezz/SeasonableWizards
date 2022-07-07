using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterObelisk : MonoBehaviour
{
    private SpriteRenderer _sr;
    private bool _active = false;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void Activate(Color color)
    {
        _sr.color = color;
        //_sr.DOColor(Color.green, 1);
        _active = true;
    }

    public bool GetActive()
    {
        return _active;
    }

}
