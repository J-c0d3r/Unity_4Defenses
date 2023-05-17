using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{

    [SerializeField] private float totalLife;
    [SerializeField] private float currentLife;

    public Image barlife;

    
    void Start()
    {
        currentLife = totalLife;
    }

    
    void Update()
    {
        
    }

    public void GetDamage(float dmg)
    {
        currentLife -= dmg;

        barlife.fillAmount = currentLife / totalLife;

        if (currentLife <= 0)
        {
            //call gameover()
        }
    }
}
