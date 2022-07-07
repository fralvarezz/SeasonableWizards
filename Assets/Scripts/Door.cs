using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    private bool _stayOpen = false;
    private bool _isOpen = false;
    private Vector3 _closedPos;

    public Vector3 openPos;

    void Start()
    {
        _closedPos = transform.position;
        openPos += _closedPos;
    }

    public void setDoor(bool open)
    {
        if (_stayOpen) return;

        if (_isOpen && !open)
        {
            //Debug.Log("closing door");
            _isOpen = open;
            transform.position = _closedPos;
        }
        else if (!_isOpen && open)
        {
            //Debug.Log("opening door");
            transform.position = openPos;
            _isOpen = open;
        }


    }

    public bool GetOpen()
    {
        return _isOpen;
    }

    public void StayOpen()
    {
        _stayOpen = true;
        transform.position = openPos;
    }
}
