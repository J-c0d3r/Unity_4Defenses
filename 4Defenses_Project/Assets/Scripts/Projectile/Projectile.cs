using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float timeToSelfDestroy;
    
    protected Rigidbody2D rig;
    protected SpriteRenderer sr;
    [SerializeField] protected GameObject touchExplosion;

    protected void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();        

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rig.velocity = direction * speed;

        DestroyItSelf();
    }

    protected virtual void DestroyItSelf()
    {
        Destroy(gameObject, timeToSelfDestroy);
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected abstract void OnCollisionEnter2D(Collision2D collision);

}
