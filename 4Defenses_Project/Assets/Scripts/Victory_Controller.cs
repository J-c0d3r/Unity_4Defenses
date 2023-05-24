using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory_Controller : MonoBehaviour
{
    private bool fadeCvnGrp1 = false;
    private bool fadeCvnGrp2 = false;

    [SerializeField] private CanvasGroup group1;
    [SerializeField] private CanvasGroup group2;


    private void Update()
    {
        //fade out
        if (fadeCvnGrp1)
        {
            if (group1.alpha > 0)
            {
                group1.alpha -= (Time.deltaTime*2);
                if (group1.alpha <= 0)
                {
                    fadeCvnGrp1 = false;
                    StartCoroutine(WaitForFinalMsg());
                }
            }
        }

        //fade in
        if (fadeCvnGrp2)
        {
            if (group2.alpha < 1)
            {
                group2.alpha += Time.deltaTime;
                if (group2.alpha >= 1)
                {
                    StartCoroutine(RestartGame());
                }
            }
        }

        //fade out
        if (!fadeCvnGrp2)
        {
            if (group2.alpha > 0)
            {
                group2.alpha -= Time.deltaTime;
                if (group2.alpha <= 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void ShowFinalMsgBtn()
    {
        fadeCvnGrp1 = true;
    }

    IEnumerator WaitForFinalMsg()
    {
        yield return new WaitForSeconds(0.5f);
        fadeCvnGrp2 = true;
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3.5f);
        fadeCvnGrp2 = false;
    }
}
