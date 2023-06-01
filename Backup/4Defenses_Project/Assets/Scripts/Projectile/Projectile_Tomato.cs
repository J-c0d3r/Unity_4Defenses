using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Tomato : Projectile
{
    private Animator anim;

    new void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();

        if ((transform.rotation.eulerAngles.z >= -180f && transform.rotation.eulerAngles.z <= 25f) || transform.rotation.eulerAngles.z >= 155f)
        {
            base.sr.sortingOrder = 7;
        }

        transform.rotation = Quaternion.Euler(Vector3.zero);

        StartCoroutine(AnimationsTransitions());
    }

    IEnumerator AnimationsTransitions()
    {
        yield return new WaitForSeconds(timeToSelfDestroy);
        anim.SetTrigger("explosion");
        rig.velocity /= 10f;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //StopCoroutine("AnimationsTransitions");

        anim.SetTrigger("explosion");
        rig.velocity /= 10f;

        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
        //}

        //if (collision.gameObject.CompareTag("Boss"))
        //{
        //    collision.gameObject.GetComponent<BossEntity>().GetDamage(damage);
        //}
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {        
        StopCoroutine("AnimationsTransitions");
        anim.SetTrigger("explosion");
        rig.velocity /= 10f;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossEntity>().GetDamage(damage);
        }
    }

    protected override void DestroyItSelf() { }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }

}
