using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandScript : MonoBehaviour
{

    public Transform target;
    public float fRadius = 3.0f;
    public float lerpSpeed;

    private Transform pivot;

    Vector3 oldPosition;
    Quaternion oldRotation;

    // Start is called before the first frame update
    void Start()
    {
        pivot = new GameObject().transform;
        pivot.position = new Vector3(this.transform.position.x, this.transform.position.y-0.136f, this.transform.position.z);
        transform.parent = pivot;
    }

    // Update is called once per frame
    void Update()
    {
        oldPosition = pivot.position;
        oldRotation = pivot.rotation;
        Vector3 v3Pos = Camera.main.WorldToScreenPoint(target.position);
        v3Pos = Input.mousePosition - v3Pos;
        float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        pivot.position = target.position;
        Quaternion to = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        pivot.rotation = Quaternion.Lerp(oldRotation, to, Time.deltaTime * lerpSpeed);
    }

    
}
