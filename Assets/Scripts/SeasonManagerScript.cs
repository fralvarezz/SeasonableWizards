using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class SeasonManagerScript : MonoBehaviour
{

    public GameObject playerObj;

    [SerializeField] float delay = 0.45f;

    public Sound[] sounds;

    public Sound[] seasonSounds;
    int actualSongIndex;
    public enum Season
    {
        SPRING,
        SUMMER,
        FALL,
        WINTER
    };

    Dictionary<Season, Tuple<GameObject, GenericSpell>> playerSprite = new Dictionary<Season, Tuple<GameObject, GenericSpell>>();
    public float interval = 15.0f;

    float currentTime;

    Season currentSeason;

    // Start is called before the first frame update
    void Start()
    {
        currentSeason = Season.SPRING;
        
        currentTime = 0;

        //Set player model objects here

        playerSprite.Add(Season.SPRING, new Tuple<GameObject, GenericSpell>(playerObj.transform.Find("PlayerSpriteSpring").gameObject, playerObj.transform.GetComponent<VineSpell>()));
        playerSprite.Add(Season.SUMMER, new Tuple<GameObject, GenericSpell>(playerObj.transform.Find("PlayerSpriteSummer").gameObject, playerObj.transform.GetComponent<SummerSpell>()));
        playerSprite.Add(Season.FALL, new Tuple<GameObject, GenericSpell>(playerObj.transform.Find("PlayerSpriteFall").gameObject, playerObj.transform.GetComponent<WindSpell>()));
        playerSprite.Add(Season.WINTER, new Tuple<GameObject, GenericSpell>(playerObj.transform.Find("PlayerSpriteWinter").gameObject, playerObj.transform.GetComponent<WinterSpell>()));

        //Enabling current season sprite and spell
        playerSprite[currentSeason].Item1.SetActive(true);
        playerSprite[currentSeason].Item2.enabled = true;

        foreach(Sound s in seasonSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        seasonSounds[(int)currentSeason].source.Play();
        seasonSounds[(int)currentSeason].source.DOFade(seasonSounds[(int)currentSeason].source.volume,
            0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //Make sure this is only called once
        //if (interval - currentTime > 0 && interval - currentTime < 0.5f && seasonSounds[(int)currentSeason].source.volume == 1f)
        //{
            //seasonSounds[(int)currentSeason].source.DOFade(0.0f, 0.5f);
        //}

        if (currentTime > interval)
        {
            StartCoroutine(DisableSprite(currentSeason));
            playerSprite[currentSeason].Item2.finishSpell();
            AnimateOut(playerSprite[currentSeason].Item1);
            seasonSounds[(int)currentSeason].source.DOFade(0.0f, 0.2f);
            //seasonSounds[(int)currentSeason].source.Stop();


            currentSeason = getNextSeason();
            ChangeSounds("walk", 4);
            ChangeSounds("grunt", 3);
            playerSprite[currentSeason].Item1.SetActive(true);
            playerSprite[currentSeason].Item2.enabled = true;
            AnimateIn(playerSprite[currentSeason].Item1);

            int randomSongIndex = Random.Range(0, 2);
            actualSongIndex = (int) currentSeason * 3 + randomSongIndex;

            seasonSounds[actualSongIndex].source.Play();
            seasonSounds[actualSongIndex].source.DOFade(0.3f, 0.2f);

            Camera.main.DOShakeRotation(0.15f, 2, 10, 90, false);

            currentTime = 0;
        }
    }

    IEnumerator DisableSprite(Season curSeason)
    {
        yield return new WaitForSeconds(delay);
        playerSprite[curSeason].Item1.SetActive(false);
    }

    Season getNextSeason()
    {
        switch (currentSeason)
        {
            case Season.SPRING:
                return Season.SUMMER;
            case Season.SUMMER:
                return Season.FALL;
            case Season.FALL:
                return Season.WINTER;
            case Season.WINTER:
                return Season.SPRING;
            default:
                return Season.SPRING;
        }
    }

    public Season getCurrentSeason()
    {
        return currentSeason;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
    void AnimateOut(GameObject player)
    {
        player.transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InCubic);
        player.transform.DORotate(new Vector3(0, 0, 359), 0.15f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad);
    }

    void AnimateIn(GameObject newPlayer)
    {
        newPlayer.transform.localScale = Vector3.zero;
        newPlayer.transform.rotation = Quaternion.Euler(0, 0, 359);

        newPlayer.transform.DORotate(new Vector3(0, 0 - 359), 0.25f, RotateMode.LocalAxisAdd).SetEase(Ease.OutBack);
        newPlayer.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
    }

    void ChangeSounds(string action, int numSounds)
    {
        SoundController _soundController = playerObj.GetComponent<SoundController>();
        string name = "";

        for (int i = 0; i < numSounds; i++)
        {
            name = action + (i + 1);
            _soundController.SwitchSound(name,
                Array.Find(sounds, sound => sound.name == name+currentSeason.ToString().ToLower()));
            
            //Debug.Log(Array.Find(sounds, sound => sound.name == name+currentSeason.ToString().ToLower()).name);
        }
        
    }
    
    public Season GetSeason()
    {
        return currentSeason;
    }

    public void ResetSeason()
    {
        playerSprite[currentSeason].Item1.SetActive(false);
        playerSprite[currentSeason].Item2.finishSpell();
        seasonSounds[actualSongIndex].source.DOFade(0f, 0.2f);
        currentSeason = Season.SPRING;

        seasonSounds[(int)currentSeason].source.Play();
        seasonSounds[(int)currentSeason].source.DOFade(0.3f, 0.2f);
        currentTime = 0;
        //Camera.main.DOShakeRotation(0.15f, 2, 10, 90, false);
        playerSprite[currentSeason].Item1.SetActive(true);
        playerSprite[currentSeason].Item2.enabled = true;
        AnimateIn(playerSprite[currentSeason].Item1);
    }
}
