using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsController : MonoBehaviour
{
    private int currentAmount;
    private float countTime;
    private bool weaponsCanSwitch;
    [SerializeField] private int currentWeapon = -1;
    [SerializeField] private int totalAmount;
    [SerializeField] private float chargingTime;


    public Image barTime;
    public List<Image> qtyImgList = new List<Image>();

    [SerializeField] private List<GameObject> gunsList = new List<GameObject>();

    private bool wasDisable;
    [SerializeField] private GameObject gunFlame;
    

    void Start()
    {
        DeactivateAllWeapons();
        weaponsCanSwitch = true;
        currentAmount = totalAmount;
    }

    private void FixedUpdate()
    {
        if (!gunFlame.activeSelf && !wasDisable)
        {
            wasDisable = true;
            gunFlame.GetComponent<GunFlame_Controller_Addons>().DisableAudios();
        }
    }

    void Update()
    {
        if (weaponsCanSwitch)
            SelectWeapon();

        RechargingLandMine();
    }

    private void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchingWeapon(0); //MachineG
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchingWeapon(1); //ShotG
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchingWeapon(2); //FlameG
            wasDisable = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchingWeapon(3); //LandM
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
            if (gunsList[i].GetComponent<WeaponController>())
            {
                gunsList[i].GetComponent<WeaponController>().canShoot = state;
            }

            if (gunsList[i].GetComponent<WeaponLandMine_Controller>())
            {
                gunsList[i].GetComponent<WeaponLandMine_Controller>().canToPosition = state;
            }
        }
    }

    private void RechargingLandMine()
    {
        if (currentAmount < totalAmount)
        {
            countTime += Time.deltaTime;
            barTime.fillAmount = countTime / chargingTime;

            if (countTime >= chargingTime)
            {
                countTime = 0f;
                barTime.fillAmount = 0f;
                currentAmount++;
                qtyImgList[currentAmount - 1].color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public int GetCurrentAmount()
    {
        return currentAmount;
    }

    public void UpdateCurrentAmount()
    {
        currentAmount--;
        qtyImgList[currentAmount].color = new Color32(255, 255, 255, 100);
    }
}
