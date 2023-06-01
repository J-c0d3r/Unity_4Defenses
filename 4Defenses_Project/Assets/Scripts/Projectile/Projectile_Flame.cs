using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Flame : Projectile
{
    [SerializeField] private float colliderDmgItSelf;
    [SerializeField] private GameObject burningObj;

    new void Start()
    {
        rig = GetComponent<Rigidbody2D>();


        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float randomAngle = Random.Range(-30f, 30f);

        transform.rotation = Quaternion.Euler(0f, 0f, angle + randomAngle);
        direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * direction;
        rig.velocity = direction * speed;


        DestroyItSelf();
    }

    private void OnDestroy()
    {
        if ((Random.Range(0f, 1f) <= 0.125f) && touchExplosion != null)
            Instantiate(touchExplosion, transform.position, touchExplosion.transform.rotation);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Random.Range(0f, 1f) <= 0.15f)
                collision.GetComponent<Enemy>().GetDamage(colliderDmgItSelf);

            if (collision.GetComponent<Enemy>().dotObj == null)
            {
                GameObject obj = Instantiate(burningObj, collision.transform.position, Quaternion.identity);
                obj.GetComponent<DamageOverTime>().damage = damage;
                collision.GetComponent<Enemy>().dotObj = obj.GetComponent<DamageOverTime>();
            }
            else
            {
                collision.GetComponent<Enemy>().dotObj.ResetTime();
            }
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            if (Random.Range(0f, 1f) <= 0.15f)
                collision.GetComponent<BossEntity>().GetDamage(colliderDmgItSelf);

            if (collision.GetComponent<BossEntity>().dotObj == null)
            {
                GameObject obj = Instantiate(burningObj, collision.transform.position, Quaternion.identity);
                obj.transform.localScale = new Vector3(3f, 3f, 3f);
                obj.GetComponent<DamageOverTime>().damage = damage;
                collision.GetComponent<BossEntity>().dotObj = obj.GetComponent<DamageOverTime>();
            }
            else
            {
                collision.GetComponent<BossEntity>().dotObj.ResetTime();
            }
        }

        if (!collision.gameObject.CompareTag("LandMine"))
        {
            Destroy(gameObject);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision) { }
}
