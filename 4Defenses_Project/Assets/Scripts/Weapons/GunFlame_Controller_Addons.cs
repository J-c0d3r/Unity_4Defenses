using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlame_Controller_Addons : MonoBehaviour
{

    [SerializeField] private AudioClip fireStartSound;
    [SerializeField] private AudioClip fireContinuosSound;
    [SerializeField] private AudioClip fireEndsSound;

    private bool isPressing;
    private bool wasDisable;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isPressing && gameObject.GetComponent<WeaponController>().canShoot && wasDisable)
        {
            wasDisable = false;
            isPressing = true;
            StartCoroutine(PlaySoundOfFire());
        }

        if (((Input.GetMouseButtonUp(0) && isPressing) || !gameObject.GetComponent<WeaponController>().canShoot) && !wasDisable)
        {
            wasDisable = true;
            isPressing = false;
            StopAllCoroutines();
            Audio_Controller.instance.StopSFXLoop();
            Audio_Controller.instance.PlaySFX(fireEndsSound);
        }
    }

    IEnumerator PlaySoundOfFire()
    {
        Audio_Controller.instance.PlaySFXLoop(fireStartSound, false);
        yield return new WaitForSeconds(1.02f);
        Audio_Controller.instance.PlaySFXLoop(fireContinuosSound, true);
    }

    public void DisableAudios()
    {
        wasDisable = true;
        isPressing = false;
        StopAllCoroutines();
        Audio_Controller.instance.StopSFXLoop();
        //Audio_Controller.instance.PlaySFX(fireEndsSound);
    }

}
