using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBoss : BossEntity
{
    public bool canMove;

    private bool canStartAtkBase;
    private bool canStartAtkPlayer;
    private bool canAtkBase;
    private string behaviour;
    private float timecountAtk;
    private float timecountSpawn;
    private float timecountSpawnEachEnemy;
    private float timecountExitActualBehaviour;
    private float timecountAtkExplosion;
    private float probability;
    private float snapshotLife;
    [SerializeField] private float timeBoostSpd;
    [SerializeField] private float timeAtk;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float timeSpawnEachEnemy;
    [SerializeField] private float timeExitGoToBaseBehaviour;
    private float timeExitChasePlayerBehaviour;
    [SerializeField] private float timeAtkExplosion;


    private GameManager gm;
    [SerializeField] private GameObject explosionDeath;


    new void Start()
    {
        base.Start();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        behaviour = "Spawn enemies";
    }


    void Update()
    {
        if (isAlive && canMove)
        {
            BossBehaviour();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive && canMove)
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
        gm.BossDied();
        isAlive = false;
        collider.enabled = false;
        rig.velocity = Vector3.zero;
        canvasBar.SetActive(false);
        anim.SetTrigger("die");

        Destroy(gameObject, 5.5f);
    }

    private void OnDestroy()
    {
        var obj = FindObjectOfType<WaveSystem_Controller>();
        if (obj != null)
            obj.UpdateAmountEnemiesInScene();

    }

    public void ExplosionEffectDie()
    {
        explosionDeath.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            canStartAtkBase = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            canStartAtkPlayer = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base") && canAtkBase)
        {
            collision.gameObject.GetComponent<Base>().GetDamage(dmgCollision);
            //canStartAtkBase = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage(dmgCollision);
            canStartAtkPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            canStartAtkBase = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            canStartAtkPlayer = false;
        }
    }

    private void BossBehaviour()
    {
        switch (behaviour)
        {
            case "Spawn enemies":
                SpawnEnemies();
                break;

            case "Go to base":
                GoToBase();
                break;

            case "Chase the player":
                ChaseThePlayer();
                break;

            default:
                break;
        }
    }

    //maybe needs transform it in a coroutine method
    private void SpawnEnemies()
    {
        movePosition = Vector3.zero;
        anim.SetInteger("transition", 2);
        timecountSpawn += Time.deltaTime;
        if (timecountSpawn <= timeSpawn)
        {
            timecountSpawnEachEnemy += Time.deltaTime;
            if (timecountSpawnEachEnemy >= timeSpawnEachEnemy)
            {
                timecountSpawnEachEnemy = 0f;
                //GameObject obj = Instantiate(minionsPrefabList[(int)Random.Range(0f, minionsPrefabList.Count)], spawnPoints[(int)Random.Range(0f, minionsPrefabList.Count)]);
                //obj.transform.localScale = new Vector2(obj.transform.localScale.x / transform.localScale.x, obj.transform.localScale.y / transform.localScale.y);                                
                GameObject obj = Instantiate(minionsList[Random.Range(0, minionsList.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
                obj.GetComponent<Enemy>().isSonOfBoss = true;
            }
        }
        else
        {
            anim.SetTrigger("stopSpawn");
            anim.SetInteger("transition", 0);
            probability = Random.Range(0f, 1f);

            if (probability >= 0.6f)
            {
                behaviour = "Go to base";
                snapshotLife = currentLife;
            }
            else
            {
                behaviour = "Chase the player";
                timeExitChasePlayerBehaviour = 15f;
            }

            StartCoroutine(BoostSpeed());
            timecountSpawn = 0f;
        }
    }

    IEnumerator BoostSpeed()
    {
        //50% up
        speed *= 1.25f;

        yield return new WaitForSeconds(timeBoostSpd);

        //50% down
        speed *= 0.8f;
    }


    private void GoToBase()
    {
        movePosition = (baseTarget.position - transform.position).normalized;
        timecountAtk += Time.deltaTime;

        if (canStartAtkBase && (timecountAtk >= timeAtk))
        {
            timecountAtk = 0f;
            probability = Random.Range(0f, 1f);

            if (probability >= 0.5f)
            {
                anim.SetInteger("transition", 0);
                StartCoroutine(AtkNormal());
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
        }

        timecountExitActualBehaviour += Time.deltaTime;
        if ((timecountExitActualBehaviour >= timeExitGoToBaseBehaviour) || ((snapshotLife - currentLife) >= (totalLife * 0.1f)))
        {
            timecountExitActualBehaviour = 0f;
            behaviour = "Chase the player";
            timeExitChasePlayerBehaviour = 10f;
        }
    }

    IEnumerator AtkNormal()
    {
        canAtkBase = true;
        yield return new WaitForFixedUpdate();
        canAtkBase = false;
    }

    public void AtkExplosionEffect()
    {
        GameObject obj = Instantiate(explosionAtk, transform);
        obj.GetComponent<Explosion>().ReceivingDmg(dmgExplosion);
    }


    private void ChaseThePlayer()
    {
        //moves to the player more closer
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("boss_green_walking"))
        //{
        //    movePosition = (playerTarget.position - transform.position).normalized;
        //}

        movePosition = (playerTarget.position - transform.position).normalized;

        timecountAtkExplosion += Time.deltaTime;

        if (canStartAtkPlayer)
        {
            probability = Random.Range(0f, 1f);

            if (probability > 0.5f && timecountAtkExplosion >= timeAtkExplosion)
            {
                timecountAtkExplosion = 0f;
                anim.SetInteger("transition", 1);

            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("boss_green_attack_start_explosion"))
            {
                anim.SetInteger("transition", 0);
            }
        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        timecountExitActualBehaviour += Time.deltaTime;
        if (timecountExitActualBehaviour >= timeExitChasePlayerBehaviour)
        {
            timecountExitActualBehaviour = 0f;
            behaviour = "Spawn enemies";
        }
    }
}
