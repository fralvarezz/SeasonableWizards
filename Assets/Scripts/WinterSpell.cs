using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterSpell : GenericSpell
{
    private float _snowballCharge;
    public int snowballSpeed;

    private float _currentDelta;

    private PlayerScript _playerScript;

    public float moveSpeedModifier;
    private float _slowedDownMoveSpeed;
    [SerializeField]
    private float _playerMoveSpeed;

    public GameObject snowballPrefab;
    private GameObject _snowball;

    public GameObject wand;
    private Transform _wandTip;

    private SoundController _soundController;
    private int soundPlaying = 5;
    private float soundLength = 0.65f;
    private float soundTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        _playerScript = GetComponent<PlayerScript>();
        _playerMoveSpeed = _playerScript.maxSpeed;
        _slowedDownMoveSpeed = _playerMoveSpeed * moveSpeedModifier;
        _wandTip = wand.transform.GetChild(0);
        _soundController = GetComponent<SoundController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (soundPlaying <= 4)
        {
            soundTimer -= Time.deltaTime;
            if (soundTimer <= 0)
            {
                _soundController.Play("SNOWBALL-Hit" + soundPlaying, 1);
                soundPlaying++;
                soundTimer = soundLength;
            }
        }

        if (Input.GetMouseButton(0))
        {
            //instantiate a snowball is there is none
            if (_snowballCharge == 0)
            {
                //var snowRenderer = snowballPrefab.GetComponent<Renderer>();

                _snowball = Instantiate(snowballPrefab, _wandTip.position, _wandTip.transform.rotation);

                soundPlaying = 1;
                _soundController.Play("SNOWBALL-Hit" + soundPlaying, 1);
                soundPlaying++;

                soundTimer = soundLength;
            }

            _currentDelta = Time.deltaTime;
            _snowballCharge += _currentDelta;

            //slow down player
            _playerScript.maxSpeed = _slowedDownMoveSpeed;

            _snowball.GetComponent<Snowball>().AddSize(Time.deltaTime);

            _snowball.transform.position = _wandTip.position + _wandTip.up * (_snowball.GetComponent<Snowball>().size * 0.1f);
        }


        if (Input.GetMouseButtonUp(0))
        {
            //throw snowball
            ReleaseSnowball();
        }

    }

    private void ReleaseSnowball()
    {
        Vector3 shootDir = (wand.transform.position - this.transform.position).normalized;
        _snowball.GetComponent<Snowball>().Throw(shootDir * snowballSpeed);
        _playerScript.maxSpeed = _playerMoveSpeed;
        _snowballCharge = 0;
        soundTimer = 0.0f;
        soundPlaying = 5;
    }

    public override void finishSpell()
    {
        //base.finishSpell();
        if (_snowball)
        {
            ReleaseSnowball();
        }
        this.enabled = false;
    }
}
