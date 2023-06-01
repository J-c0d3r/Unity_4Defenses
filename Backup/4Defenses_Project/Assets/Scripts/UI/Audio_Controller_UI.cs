using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Controller_UI : MonoBehaviour
{

    public Slider sliderMusic;
    public Slider sliderSFX;
    public Slider sliderMaster;


    public void UpdateSliders()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("Music");
        sliderSFX.value = PlayerPrefs.GetFloat("Sfx");
        sliderMaster.value = PlayerPrefs.GetFloat("Master");
    }

    public void UpdateMusicVolume(float vol)
    {
        Audio_Controller.instance.VolumeControlMusic(vol);
    }

    public void UpdateSFXVolume(float vol)
    {
        Audio_Controller.instance.VolumeControlSFX(vol);
    }

    public void UpdateMasterVolume(float vol)
    {
        Audio_Controller.instance.VolumeControlMaster(vol);
    }

}
