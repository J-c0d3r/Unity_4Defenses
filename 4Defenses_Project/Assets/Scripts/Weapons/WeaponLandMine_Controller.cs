using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLandMine_Controller : MonoBehaviour
{
    public bool canToPosition;

    private WeaponsController weaponControllerScrp;
    [SerializeField] private GameObject landMinePrefab;
    [SerializeField] private GameObject weaponsControllerObj;

    [SerializeField] private AudioClip dropedSound;

    void Start()
    {
        canToPosition = true;
        weaponControllerScrp = weaponsControllerObj.GetComponent<WeaponsController>();
    }

    void Update()
    {
        ToPositionLandMine();
    }

    private void ToPositionLandMine()
    {
        if (Input.GetMouseButtonDown(0) && canToPosition)
        {
            if (weaponControllerScrp.GetCurrentAmount() > 0)
            {
                Audio_Controller.instance.PlaySFX(dropedSound);
                weaponControllerScrp.UpdateCurrentAmount();
                Instantiate(landMinePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
