using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject PanelMode;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PanelMode.activeSelf)
            {
                PanelMode.SetActive(false);
            }
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


    public void ExitGame()
    {
        Application.Quit();
    }
}
