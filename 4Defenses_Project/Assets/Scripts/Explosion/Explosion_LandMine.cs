using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_LandMine : MonoBehaviour
{

    [SerializeField] private float damage;

    [SerializeField] private AudioClip explosionClip;

    void Start()
    {
        Audio_Controller.instance.PlaySFX(explosionClip);
        Destroy(gameObject, 0.34f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage(damage / 2);
        }

        if (collision.gameObject.CompareTag("Base"))
        {
            collision.gameObject.GetComponent<Base>().GetDamage(damage);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossEntity>().GetDamage(damage);
        }
    }

}
