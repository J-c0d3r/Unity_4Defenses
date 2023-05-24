using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject[] minionsList;

    public GameObject gameoverPnl;
    public GameObject victoryPnl;
    private PlayerController player;
    [SerializeField] private GameObject mouseSprite;
    [SerializeField] private GameObject mouseCnvUI;

    //private void Awake()
    //{

    //}


    public void ShowGameOver()
    {
        player = FindObjectOfType<PlayerController>();
        player.canMove = false;
        mouseSprite.gameObject.GetComponent<MousePointer>().gameObject.SetActive(false);
        mouseCnvUI.SetActive(true);

        gameoverPnl.SetActive(true);
    }

    public void TryAgainBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowVictory()
    {
        mouseSprite.gameObject.GetComponent<MousePointer>().gameObject.SetActive(false);
        mouseCnvUI.SetActive(true);
        victoryPnl.SetActive(true);
    }


    public void BossDied()
    {
        minionsList = GameObject.FindGameObjectsWithTag("Enemy");
        if (minionsList != null)
        {
            for (int i = 0; i < minionsList.Length; i++)
            {
                minionsList[i].GetComponent<Enemy>().GetDamage(9999);
            }
        }
    }

}
