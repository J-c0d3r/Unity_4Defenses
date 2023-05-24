using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Flame : Projectile
{
    private Animator anim;

    new void Start()
    {
        base.Start();
        //rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        //direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //rig.velocity = direction * speed;


        StartCoroutine(ChangingAnimation());
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    IEnumerator ChangingAnimation()
    {
        //yield return new WaitForSeconds(0.16f);
        anim.SetTrigger("final");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    protected override void DestroyItSelf() { }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(touchExplosion, transform.position, Quaternion.identity);
    }
}
