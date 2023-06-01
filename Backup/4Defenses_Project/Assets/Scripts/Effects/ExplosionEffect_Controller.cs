using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect_Controller : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    [SerializeField] private bool isExplosion0;
    [SerializeField] private AudioClip explosion0;


    void Start()
    {
        if (isExplosion0)
            Audio_Controller.instance.PlaySFX(explosion0);

        Destroy(gameObject, timeToDestroy);
    }
}
