using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGreenBossDie : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClipDeath;

    public void ExplotionEffects()
    {
        Audio_Controller.instance.PlaySFX(explosionClipDeath);
        CinemachineShake.instance.BossDieShakeCamera(10f, 2f, true);
    }

}
