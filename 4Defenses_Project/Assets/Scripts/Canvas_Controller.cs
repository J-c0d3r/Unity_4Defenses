using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour
{
    private bool isOpen;

    private PlayerController player;

    [SerializeField] private GameObject configBtn;
    [SerializeField] private GameObject configPnl;
    [SerializeField] private GameObject aimPnl;
    [SerializeField] private GameObject configGrpBtns;
    [SerializeField] private GameObject mouseSprite;
    [SerializeField] private GameObject mouseCnvUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                OpenConfig();
                isOpen = true;
            }
            else
            {
                CloseConfig();
                isOpen = false;
            }
        }
    }

    public void OpenConfig()
    {
        player = FindObjectOfType<PlayerController>();
        player.canMove = false;
        configBtn.SetActive(false);
        mouseSprite.gameObject.GetComponent<MousePointer>().isStoped = true;
        mouseCnvUI.SetActive(true);
        configPnl.SetActive(true);
    }

    public void CloseConfig()
    {
        player.canMove = true;
        configBtn.SetActive(true);
        mouseSprite.gameObject.GetComponent<MousePointer>().isStoped = false;
        mouseCnvUI.SetActive(false);
        configPnl.SetActive(false);

        configGrpBtns.SetActive(true);
        aimPnl.SetActive(false);
    }


    public void OpenChangeAim()
    {
        aimPnl.SetActive(true);
        configGrpBtns.SetActive(false);
    }

    public void CloseAimOptions()
    {
        aimPnl.SetActive(false);
        configGrpBtns.SetActive(true);
    }
}
