using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SummerSpell : GenericSpell
{
    float _timer = 1f;
    bool _casting = false;
    float _castInterval = 0.01f;
    LayerMask _layerMask;
    bool _seasonOver = false;
    SoundController _soundController;
    [SerializeField] Gradient fixedYellow;
    [SerializeField] Gradient falloffYellow;

    public Transform wandTip;
    public LineRenderer lineRenderer;
    [SerializeField] ParticleSystem rayParticles;

    protected override void Start()
    {
        _layerMask = LayerMask.GetMask("Wall", "Mirror", "Enemy");
        _soundController = gameObject.GetComponent<SoundController>();
        DisableEmission();
    }

    void FixedUpdate()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0.5f) + transform.rotation.eulerAngles);
    }

    protected override void Update()
    {
        if (_seasonOver) return;

        if (_casting)
        {
            _timer += Time.deltaTime;
            if (_timer >= _castInterval)
            {
                CastSunRay();
                _timer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _casting = true;
            _soundController.Play("summer_windup", 1f);
            _soundController.Play("summer_continous", 1f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _casting = false;
            _soundController.Stop("summer_continous");
            _soundController.Play("summer_winddown", 1f);
            lineRenderer.enabled = false;
            DisableEmission();
        }
    }

    void CastSunRay()
    {
        float _lenght = 1.7f;
        RaycastHit2D hit = Physics2D.Raycast(wandTip.position, wandTip.transform.up, _lenght, _layerMask);

        if (hit.collider != null)
        {
            //float distance = Vector3.Distance(hit.point, wandTip.position);
            var other = hit.transform.gameObject;
            if (other.CompareTag("Mirror"))
            {
                other.GetComponent<Mirror>().ReflectRay(0);
            }
            if (other.CompareTag("MirrorEnd"))
            {
                other.GetComponent<MirrorEnd>().ReflectRay(0);
            }
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(1);
                _soundController.PlayIfNotPlaying("laserdmg", 1);
            }

            lineRenderer.SetPositions(new Vector3[2] { wandTip.position, hit.point });
            lineRenderer.colorGradient = fixedYellow;
        }
        else
        {
            lineRenderer.colorGradient = falloffYellow;
            lineRenderer.SetPositions(new Vector3[2] { wandTip.position, wandTip.position + wandTip.transform.up * _lenght });
        }
        lineRenderer.enabled = true;
        EnableEmission();
    }

    void DisableEmission()
    {
        var emitter = rayParticles.emission;
        emitter.enabled = false;
    }
    void EnableEmission()
    {
        var emitter = rayParticles.emission;
        emitter.enabled = true;
    }

    public override void finishSpell()
    {
        if (_casting) _soundController.Play("summer_winddown", 1f);
        _seasonOver = true;
        lineRenderer.enabled = false;
        _casting = false;
        _soundController.Stop("summer_continous");
        DisableEmission();
        this.enabled = false;
    }

    private void OnEnable()
    {
        _seasonOver = false;
    }
}
