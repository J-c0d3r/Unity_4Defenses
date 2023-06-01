using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGreenBossDie : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClipDeath;

    public void PlaySoundExplodion()
    {
        Audio_Controller.instance.PlaySFX(explosionClipDeath);
    }

}
