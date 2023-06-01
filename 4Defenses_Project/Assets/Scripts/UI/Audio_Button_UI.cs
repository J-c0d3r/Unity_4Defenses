using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Button_UI : MonoBehaviour
{

    [SerializeField] private AudioClip buttonPressedClip;


    public void PressSoundButton()
    {
        Audio_Controller.instance.PlaySFX(buttonPressedClip);
    }

}
