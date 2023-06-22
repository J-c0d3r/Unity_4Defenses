using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage;

    private void Start()
    {
        Destroy(gameObject, 0.8f);
    }

    public void ReceivingDmg(float dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage(damage);
        }

        if (collision.gameObject.CompareTag("Base"))
        {
            collision.gameObject.GetComponent<Base>().GetDamage(damage);
        }
    }
}
