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

        if (transform.rotation.eulerAngles.z == 270f || transform.rotation.eulerAngles.z == 0f || transform.rotation.eulerAngles.z == 180)
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

    //quando colidir com um inimigo deverá acionar a animação
    //if para essa checagem

}
