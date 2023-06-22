using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameObject PanelMode;
    [SerializeField] private GameObject audioSettPnl;
    [SerializeField] private GameObject groupBtns;

    [SerializeField] private AudioClip menuSong;

    private void Start()
    {
        groupBtns.SetActive(true);
        PanelMode.SetActive(false);
        audioSettPnl.SetActive(false);

        Audio_Controller.instance.PlayMusic(menuSong, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PanelMode.activeSelf)
                PanelMode.SetActive(false);


            if (audioSettPnl.activeSelf)
                CloseOption();
        }
    }

    public void Play_ChooseMode()
    {
        PanelMode.SetActive(true);
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene(1);
    }

    //multiplayer


    public void OpenOptions()
    {
        groupBtns.SetActive(false);
        audioSettPnl.SetActive(true);

        audioSettPnl.GetComponent<Audio_Controller_UI>().UpdateSliders();
    }

    public void CloseOption()
    {
        groupBtns.SetActive(true);
        audioSettPnl.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
