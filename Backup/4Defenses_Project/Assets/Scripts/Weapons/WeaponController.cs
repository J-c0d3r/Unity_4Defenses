using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool canShoot;
    private float timeCount;
    [SerializeField] private int qtyPerShoot;
    [SerializeField] private float reloadShoot;


    [SerializeField] private GameObject smokeEffect;
    [SerializeField] private GameObject proj;
    [SerializeField] private Transform projPoint;

    [SerializeField] private AudioClip shootSound;

    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        if (canShoot)
            ToShoot();
    }

    private void ToShoot()
    {
        timeCount += Time.deltaTime;
        if (Input.GetMouseButton(0) && gameObject.activeSelf)
        {
            if (timeCount >= reloadShoot)
            {
                if (!(gameObject.name == "GunFlame"))
                    Audio_Controller.instance.PlaySFX(shootSound);

                timeCount = 0f;

                if (Random.Range(0f, 1f) >= 0.2f)
                    Instantiate(smokeEffect, projPoint.position, smokeEffect.transform.rotation);

                for (int i = 0; i < qtyPerShoot; i++)
                {
                    Instantiate(proj, projPoint.position, projPoint.rotation);
                }
            }
        }
    }
}
