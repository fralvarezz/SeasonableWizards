using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using DG.Tweening;
using Random = UnityEngine.Random;

public class SoundController : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    public void Play(string name, float _pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.pitch = _pitch;

        //Debug.Log("Source volume " + s.source.volume + " normal volume " + s.volume);
        if (s.source.volume != s.volume)
        {
            //Debug.Log("Fixing volume");
            DOTween.Kill(s.source, true);
            s.source.DOFade(s.volume, 0.01f);

        }

        if (!s.source.isPlaying)
            s.source.Play();
        else if (s.source.isPlaying) //stop and start again if sound is already playing
        { s.source.Stop(); s.source.Play(); }
    }
    
    public void PlayIfNotPlaying(string name, float _pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.pitch = _pitch;

        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void PlayRandomSound(bool pitchRandomiser, params string[] allSounds)
    {
        int soundCap = allSounds.Length;
        int rng = UnityEngine.Random.Range(0, soundCap);

        if (pitchRandomiser)
            Play(allSounds[rng], UnityEngine.Random.Range(0.8f, 1.1f));
        else
            Play(allSounds[rng], 1f);

    }

    
    public void PlayRandomSoundIfNone(params string[] allSounds)
    {
        int soundCap = allSounds.Length;
        int rng = Random.Range(0, soundCap);

        bool anyPlaying = false;
        
        for (int i = 0; i < soundCap; i++)
        {
            Sound s = Array.Find(sounds, sound => sound.name == allSounds[i]);
            if (s.source.isPlaying)
            {
                anyPlaying = true;
            }
        }

        if (!anyPlaying)
        {
            Play(allSounds[rng], 1f);
        }
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void StopFade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Debug.Log("Fading sound " + s.name);
        if (!DOTween.IsTweening(s.source) && s.source.isPlaying)
            s.source.DOFade(0, 1f);

        /*float voleumFade = s.volume;
        DOTween.To(() => voleumFade, x => voleumFade = x, 0, 0.2f);
        s.volume = voleumFade;*/
    }

    public void SwitchSound(string name, Sound newSound)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sounds[i].source.clip = newSound.clip;
                sounds[i].clip = newSound.clip;
            }
        }
    }
    public void MakeSpatial(float minDistance, float maxDistance, string soundName)
    {

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                sounds[i].source.spatialBlend = 1;
                sounds[i].source.minDistance = minDistance;
                sounds[i].source.maxDistance = maxDistance;
                //sounds[i].source.rolloffMode = maxDistance;
            }
        }
    }
    

}
