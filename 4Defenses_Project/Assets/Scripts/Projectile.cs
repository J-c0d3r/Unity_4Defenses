using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToSelfDestroy;
    //private float timeCount;
    //[SerializeField] private float reloadShoot;


    //[SerializeField] private GameObject proj;
    //private Animator anim;
    private Rigidbody2D rig;
    private SpriteRenderer sr;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rig.velocity = transform.right * speed;


        if (transform.rotation.eulerAngles.z == 270f)
        {
            sr.sortingOrder = 7;
        }

        Destroy(gameObject, timeToSelfDestroy);

    }


    void Update()
    {



    }

    //public void SetDirectionVelocity(Transform point)
    //{
    //    rig.velocity = transform.up * speed;
    //}
}
