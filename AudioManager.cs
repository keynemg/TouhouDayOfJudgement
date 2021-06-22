using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SfxSource;
    public AudioSource musicSource;
    public static AudioManager instance = null;
    public bool MusicFade = false;
    public float Fader = 1f;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        MusicFade = false;
        Fader = musicSource.volume;
    }

    private void Update()
    {
        if (MusicFade)
        {
            Fader -= Time.deltaTime/5;
            musicSource.volume = Fader;
        }
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;

        musicSource.Play();
    }


    public void PlaySingle(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
    public void PlaySingle(AudioClip clip,float _volume)
    {
        //SfxSource.clip = clip;
        SfxSource.PlayOneShot(clip, _volume);
    }
}
