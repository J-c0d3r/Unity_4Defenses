using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    private bool weaponsCanSwitch;
    [SerializeField] private int currentWeapon = -1;

    [SerializeField] private List<GameObject> gunsList = new List<GameObject>();


    void Start()
    {
        DeactivateAllWeapons();
        weaponsCanSwitch = true;
    }


    void Update()
    {
        if (weaponsCanSwitch)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchingWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchingWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchingWeapon(2);
        }
    }

    private void SwitchingWeapon(int id)
    {
        gunsList[currentWeapon].SetActive(false);
        currentWeapon = id;
        gunsList[currentWeapon].SetActive(true);
    }

    private void DeactivateAllWeapons()
    {
        for (int i = 0; i < gunsList.Count; i++)
        {
            gunsList[i].SetActive(false);
        }
    }

    public void UnlockAllWeapons(bool state)
    {
        weaponsCanSwitch = state;
        for (int i = 0; i < gunsList.Count; i++)
        {
            gunsList[i].GetComponent<WeaponController>().canShoot = state;
        }
    }
}
