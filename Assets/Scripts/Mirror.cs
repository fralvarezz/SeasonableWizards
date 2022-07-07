using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    float _timer = 1f;
    float _castDuration = 0.02f;
    LineRenderer _lineRenderer;
    LayerMask _layerMask;

    public Transform emissionPoint;
    public Door door;
    public Transform impactEffect;
    public Rail rail;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _layerMask = LayerMask.GetMask("Wall", "Mirror", "Enemy");
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer <= _castDuration * 2f)
        {
            door.setDoor(true);
            _lineRenderer.enabled = true;
        }
        else
        {
            door.setDoor(false);
            _lineRenderer.enabled = false;
            impactEffect.gameObject.SetActive(false);
        }
    }

    public void ReflectRay(int chain)
    {
        if (chain > 5) return;
        _timer = 0.0f;

        RaycastHit2D hit = Physics2D.Raycast(emissionPoint.position, emissionPoint.right, 10, _layerMask);

        if (hit.collider != null)
        {
            impactEffect.position = hit.point;
            impactEffect.gameObject.SetActive(true);

            Debug.Log("hit " + hit.collider.gameObject.tag);
            float distance = Vector3.Distance(hit.point, emissionPoint.position);
            _lineRenderer.SetPositions(new Vector3[2] { emissionPoint.position, hit.point });

            var other = hit.transform.gameObject;

            if (other.CompareTag("Mirror"))
            {
                other.GetComponent<Mirror>().ReflectRay(chain+1);
            }
            if (other.CompareTag("MirrorEnd"))
            {
                other.GetComponent<MirrorEnd>().ReflectRay(0);
            }
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(1);
            }

        }
        else
        {
            _lineRenderer.SetPositions(new Vector3[2] { emissionPoint.position, emissionPoint.position + emissionPoint.right * 10});
        }
        _lineRenderer.enabled = true;
    }

    public void MoveOnRail(int dir)
    {
        rail.Move(dir);
    }
}
