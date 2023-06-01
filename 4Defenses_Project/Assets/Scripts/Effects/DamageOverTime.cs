using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    public float damage;
    private bool canDamage;
    private float countTime;
    [SerializeField] private float amountTimeToDamage;
    private float countTimeDurationDOT;
    [SerializeField] private float timeDurationDOT;

    private GameObject currentObj;
    private SpriteRenderer spriteR;

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        countTime = amountTimeToDamage;
    }

    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime >= amountTimeToDamage)
        {
            canDamage = true;
        }

        countTimeDurationDOT += Time.deltaTime;
        if (countTimeDurationDOT >= timeDurationDOT)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentObj == null)
        {
            currentObj = collision.gameObject;

            if (!currentObj.CompareTag("Player"))
                spriteR.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == currentObj)
            transform.position = collision.transform.position;

        if (canDamage)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
                countTime = 0;
                canDamage = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentObj == collision.gameObject)
        {
            Destroy(gameObject);
        }
    }

    public void ResetTime()
    {
        countTimeDurationDOT = 0f;
    }
}
