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
        yield return new WaitForSeconds(0.25f);
        anim.SetTrigger("explosion");
        rig.velocity /= 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //StopCoroutine("AnimationsTransitions");
        anim.SetTrigger("explosion");
        rig.velocity /= 10f;
    }

}
