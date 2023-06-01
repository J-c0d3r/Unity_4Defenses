using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_MachineGun : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
        //}

        //if (collision.gameObject.CompareTag("Boss"))
        //{
        //    collision.gameObject.GetComponent<BossEntity>().GetDamage(damage);
        //}

        //Destroy(gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);            
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossEntity>().GetDamage(damage);
        }

        Instantiate(touchExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }
}
