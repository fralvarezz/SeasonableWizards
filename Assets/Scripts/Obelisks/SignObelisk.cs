using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignObelisk : MonoBehaviour
{
    public CenterObelisk centerObelisk;
    public Color color;

    public void Activate()
    {
        centerObelisk.Activate(color);
    }

}
