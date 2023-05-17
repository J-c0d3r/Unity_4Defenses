using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBoss : BossEntity
{

    [SerializeField] private GameObject explosionDeath;
    
    new void Start()
    {
        base.Start();
    }

    
    void Update()
    {
        movePosition = (baseTarget.position - transform.position).normalized;
        rig.velocity = new Vector2(movePosition.x, movePosition.y) * speed;
    }

    public override void GetDamage(float dmg)
    {
        currentLife -= dmg;

        lifeBar.fillAmount = currentLife / totalLife;

        if (currentLife <= 0)
        {
            Die();
        }
    }


    protected override void Die()
    {
        isAlive = false;
        collider.enabled = false;
        rig.velocity = Vector2.zero;
        canvasBar.gameObject.SetActive(false);
        anim.SetTrigger("die");

        Destroy(gameObject, 5.5f);
    }

    public void ExplosionEffectDie()
    {
        explosionDeath.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("passei aqui");
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage(dmgCollision);
        }
    }
}
