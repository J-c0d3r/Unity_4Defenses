using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{

    [SerializeField] private float totalLife;
    [SerializeField] private float currentLife;

    public Image barlife;
    private SpriteRenderer spriteR;


    void Start()
    {
        Time.timeScale = 1f;
        spriteR = GetComponent<SpriteRenderer>();
        currentLife = totalLife;
    }

    public void GetDamage(float dmg)
    {
        StopCoroutine("DmgEffect");
        StartCoroutine("DmgEffect");
        currentLife -= dmg;

        barlife.fillAmount = currentLife / totalLife;

        if (currentLife <= 0)
        {            
            Time.timeScale = 0f;
            FindObjectOfType<GameManager>().ShowGameOver();
        }
    }

    IEnumerator DmgEffect()
    {
        spriteR.color = new Color32(255, 32, 0, 255);
        yield return new WaitForSeconds(0.1f);
        spriteR.color = new Color32(255, 255, 255, 255);
    }
}
