using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour
{

    private GameObject _player;
    private SpriteRenderer _sr;
    private float _timer = 0f;
    private bool _entered = false;
    private bool _complete = false;
    private bool _animInProgress = false;

    private SoundController _soundController;
    
    //public Transform centerPos;
    public CenterObelisk centerObelisk;
    public Color color;
    public float animationTime = 2f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _sr = GetComponent<SpriteRenderer>();
        _soundController = GetComponent<SoundController>();
    }

    void Update()
    {
        if (_entered && !_complete)
        {
            _timer += Time.deltaTime;

            if (_animInProgress == false)
            {
                Activate();
                _animInProgress = true;
            }

            if (_timer > animationTime)
            {
                _complete = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" )
        {
            if (!_entered)
            {
                _entered = true;
                _soundController.Play("puzzlecompleted",1);
            }
        }
    }

    private void Activate()
    {
        _sr.DOColor(color, animationTime).SetEase(Ease.OutExpo);
        centerObelisk.Activate(color);
    }

}
