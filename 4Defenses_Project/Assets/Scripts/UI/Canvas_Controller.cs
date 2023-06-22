using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private GameObject configBtn;
    [SerializeField] private GameObject configPnl;
    [SerializeField] private GameObject aimPnl;
    [SerializeField] private GameObject configGrpBtns;
    [SerializeField] private GameObject mouseSprite;
    [SerializeField] private GameObject mouseCnvUI;
    [SerializeField] private GameObject audioSettBtn;
    [SerializeField] private GameObject audioSettPnl;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (player == null)
                player = FindObjectOfType<PlayerController>();

            if (!player.wasGameFinished)
            {
                configPnl.SetActive(!configPnl.activeSelf);

                if (configPnl.activeSelf)
                    OpenEsc();

                if (!configPnl.activeSelf)
                    CloseEsc();
            }
        }
    }

    private void OpenEsc()
    {
        configGrpBtns.SetActive(true);
        audioSettPnl.SetActive(false);
        aimPnl.SetActive(false);
        configBtn.SetActive(false);

        player.canMove = false;
        mouseSprite.gameObject.GetComponent<MousePointer>().isStoped = true;
        mouseCnvUI.SetActive(true);

        Time.timeScale = 0f;
    }

    private void CloseEsc()
    {
        configGrpBtns.SetActive(false);
        audioSettPnl.SetActive(false);
        aimPnl.SetActive(false);
        configBtn.SetActive(true);

        player.canMove = true;
        mouseSprite.gameObject.GetComponent<MousePointer>().isStoped = false;
        mouseCnvUI.SetActive(false);

        Time.timeScale = 1f;
    }


    public void OpenConfig()
    {
        player = FindObjectOfType<PlayerController>();
        player.canMove = false;
        configBtn.SetActive(false);
        mouseSprite.gameObject.GetComponent<MousePointer>().isStoped = true;

        configGrpBtns.SetActive(true);
        mouseCnvUI.SetActive(true);
        configPnl.SetActive(true);

        Time.timeScale = 0f;
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

        Time.timeScale = 1f;
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


    public void OpenAudioSettings()
    {
        configGrpBtns.SetActive(false);

        audioSettPnl.SetActive(true);
        audioSettPnl.GetComponent<Audio_Controller_UI>().UpdateSliders();
    }

    public void CloseAudioSettings()
    {
        audioSettPnl.SetActive(false);

        configGrpBtns.SetActive(true);
    }
}
