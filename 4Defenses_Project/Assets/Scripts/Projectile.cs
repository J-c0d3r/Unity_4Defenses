using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float timeToSelfDestroy;

  
    protected Rigidbody2D rig;
    protected SpriteRenderer sr;


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

        Destroy(gameObject, timeToSelfDestroy);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

}
