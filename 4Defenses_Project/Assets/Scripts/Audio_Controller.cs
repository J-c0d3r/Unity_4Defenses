using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_Controller : MonoBehaviour
{
    public static Audio_Controller instance;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource sfxLoop;


    private void Awake()
    {
        if (instance == null || instance == this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        music.volume = PlayerPrefs.GetFloat("Music");
        sfx.volume = PlayerPrefs.GetFloat("Sfx");
        sfxLoop.volume = PlayerPrefs.GetFloat("Sfx");
        AudioListener.volume = PlayerPrefs.GetFloat("Master");
    }


    public void PlayMusic(AudioClip clip, bool isLoop)
    {
        music.Stop();
        if (isLoop)
        {            
            music.clip = clip;
            music.loop = isLoop;
            music.Play();            
        }
        else
        {
            music.PlayOneShot(clip);
        }
    }

    public void StopMusic()
    {
        //music.clip = null;
        music.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void PlaySFXLoop(AudioClip clip, bool isLoop)
    {
        sfxLoop.Stop();
        if (isLoop)
        {
            sfxLoop.clip = clip;
            sfxLoop.loop = true;
            sfxLoop.Play();
        }
        else
        {
            sfxLoop.clip = clip;
            sfxLoop.loop = false;
            sfxLoop.Play();
        }
    }

    public void StopSFXLoop()
    {
        sfxLoop.clip = null;
        sfxLoop.loop = false;
        sfxLoop.Stop();
    }

    public void VolumeControlMusic(float vol)
    {
        music.volume = vol;
        PlayerPrefs.SetFloat("Music", vol);
    }

    public void VolumeControlSFX(float vol)
    {
        sfx.volume = vol;
        sfxLoop.volume = vol;
        PlayerPrefs.SetFloat("Sfx", vol);
    }

    public void VolumeControlMaster(float vol)
    {
        AudioListener.volume = vol;
        PlayerPrefs.SetFloat("Master", vol);
    }
}
