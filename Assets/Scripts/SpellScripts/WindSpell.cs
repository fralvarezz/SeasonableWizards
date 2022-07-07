using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpell : GenericSpell
{

    [SerializeField] ParticleSystem windParticles = null;
    bool seasonOver = false;
    public GameObject windCone;
    private SoundController _soundController;

    private bool _contSoundStarted;
    //private float _contSoundDuration = 20.0f;
    private float _contSoundCountdown;

    // Start is called before the first frame update
    protected override void Start()
    {
        _soundController = GetComponent<SoundController>();
        DisableParticles();
    }

    // Update is called once per frame
    protected override void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!_contSoundStarted)
            {
                _soundController.Play("wind_cont", 1);
                _contSoundStarted = true;
                EnableParticles();
            }
        }

        if (Input.GetMouseButton(0))
        {
            windCone.GetComponent<PolygonCollider2D>().enabled = true;
        }
        else
        {
            windCone.GetComponent<PolygonCollider2D>().enabled = false;
            _contSoundStarted = false;
            _soundController.StopFade("wind_cont");
            DisableParticles();
        }
        if (seasonOver)
        {
            windCone.GetComponent<PolygonCollider2D>().enabled = false;
            this.enabled = false;
            DisableParticles();
        }
    }

    public override void finishSpell()
    {
        _soundController.StopFade("wind_cont");
        _contSoundStarted = false;
        seasonOver = true;
        DisableParticles();
    }

    private void OnEnable()
    {
        seasonOver = false;
    }

    void EnableParticles()
    {
        var emitting = windParticles.emission;
        emitting.enabled = true;
    }
    void DisableParticles()
    {
        var emitting = windParticles.emission;
        emitting.enabled = false;
    }

}
