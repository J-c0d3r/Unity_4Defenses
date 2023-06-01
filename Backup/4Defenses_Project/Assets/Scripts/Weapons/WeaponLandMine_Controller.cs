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
        //if (currentAmount < totalAmount)
        //{
        //    countTime += Time.deltaTime;
        //    barTime.fillAmount = countTime / chargingTime;

        //    if (countTime >= chargingTime)
        //    {
        //        countTime = 0f;
        //        barTime.fillAmount = 0f;
        //        currentAmount++;
        //        qtyImgList[currentAmount - 1].color = new Color32(255, 255, 255, 255);
        //    }
        //}

        ToPositionLandMine();
    }

    private void ToPositionLandMine()
    {
        if (Input.GetMouseButtonDown(0) && canToPosition)
        {
            //var weaponsController2= weaponsController.GetComponent<WeaponsController>();
            if (weaponControllerScrp.GetCurrentAmount() > 0)
            {
                Audio_Controller.instance.PlaySFX(dropedSound);
                weaponControllerScrp.UpdateCurrentAmount();
                Instantiate(landMinePrefab, transform.position, Quaternion.identity);
            }            
        }
    }
}
