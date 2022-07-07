using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rail : MonoBehaviour
{
    private bool _moving = false;
    private int _index = 0;
    private float _timer = 1f;

    public Transform mirror;
    public List<Transform> pos;
    public bool horisontal = true;
    public int startIndex = 0;

    void Start()
    {
        _index = startIndex;
        mirror.transform.position = pos[_index].position;
    }

    
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f) _moving = false;
    }

    public void Move(int dir)
    {
        if (_moving) return;

        _moving = true;
        _timer = 1f;

        if (horisontal)
        {
            if (dir == 2) // right
            {
                if (_index < pos.Count - 1)
                {
                    _index++;
                    mirror.DOMove(pos[_index].position, 1).SetEase(Ease.OutQuint);
                }
            }
            if (dir == 4) // left
            {
                if (_index > 0)
                {
                    _index--;
                    mirror.DOMove(pos[_index].position, 1).SetEase(Ease.OutQuint);
                }
            }
        }
        else // vertical
        {
            if (dir == 1) // up
            {
                if (_index < pos.Count - 1)
                {
                    _index++;
                    mirror.DOMove(pos[_index].position, 1).SetEase(Ease.OutQuint);
                }
            }
            if (dir == 3) // down
            {
                if (_index > 0)
                {
                    _index--;
                    mirror.DOMove(pos[_index].position, 1).SetEase(Ease.OutQuint);
                }
            }
        }
        
    }
}
