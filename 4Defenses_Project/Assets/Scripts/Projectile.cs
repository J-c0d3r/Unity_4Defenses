using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float timeToSelfDestroy;
    //private float timeCount;
    //[SerializeField] private float reloadShoot;


    //[SerializeField] private GameObject proj;
    private Animator anim;
    protected Rigidbody2D rig;
    protected SpriteRenderer sr;


    protected void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rig.velocity = direction * speed;
        //rig.velocity = transform.right * speed;

        /*
            if (transform.rotation.eulerAngles.z == 270f)
            {
                sr.sortingOrder = 7;
            }
        */

        StartCoroutine(ChangingAnimation());
        //Destroy(gameObject, timeToSelfDestroy);

    }


    void Update()
    {
        //if (!Input.GetMouseButton(0))
        //{
        //    StartCoroutine(ChangingAnimation());
        //}
        

    }

    IEnumerator ChangingAnimation()
    {
        anim.SetTrigger("final");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
