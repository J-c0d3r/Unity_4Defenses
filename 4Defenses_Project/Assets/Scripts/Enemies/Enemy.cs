using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool isSonOfBoss;
    private bool isAlive;
    private bool isKnockback;
    private float timeCountToFollowPlayer = 3f;
    private float timecountCanRun;

    [Header("Attributes")]
    [SerializeField] private float totalLife;
    [SerializeField] private float currentLife;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float dmgExplosion;
    [SerializeField] private float dmgCollision;
    [SerializeField] private float minorPlayerArea;
    [SerializeField] private float majorPlayerArea;
    [SerializeField] private float timeCanRun;
    //[SerializeField] private float towerArea;



    [Header("Life Bar")]
    public Image lifeBar;
    public GameObject canvasBar;


    private Animator anim;
    private Rigidbody2D rig;
    private Vector2 movePosition;
    private SpriteRenderer spriteR;
    private NavMeshAgent agent;
    //private WaveSystem_Controller movePosition;?

    [Header("Associations")]
    [SerializeField] private Transform baseTarget;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosion0;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip explosionDieClip;


    public DamageOverTime dotObj;

    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        baseTarget = GameObject.FindGameObjectWithTag("Base").transform;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        speed = Random.Range(minSpeed, maxSpeed);
        currentLife = totalLife;
        isAlive = true;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Instantiate(explosion0, transform.position, Quaternion.identity);
        //put here explosion effect when it is created
    }


    void Update()
    {
        timecountCanRun += Time.deltaTime;
        if (isAlive && timecountCanRun >= timeCanRun)
        {
            float distance = Vector2.Distance(playerTarget.position, transform.position);

            if (!isSonOfBoss)
            {
                //minor area
                if (distance <= minorPlayerArea && timeCountToFollowPlayer >= 3f)
                {
                    //movePosition = (playerTarget.position - transform.position).normalized;
                    movePosition = playerTarget.position;
                    timeCountToFollowPlayer = 0f;
                    //Debug.Log("minor area");
                }

                //intermediate/between area/s
                if (timeCountToFollowPlayer == 0f)
                {
                    //movePosition = (playerTarget.position - transform.position).normalized;
                    movePosition = playerTarget.position;
                    //Debug.Log("inter area");
                }

                //major area
                if (distance > majorPlayerArea)
                {
                    //movePosition = (baseTarget.position - transform.position).normalized;
                    movePosition = baseTarget.position;
                    timeCountToFollowPlayer += Time.deltaTime;
                    //Debug.Log("major area");
                }
            }
            else
            {
                //movePosition = (playerTarget.position - transform.position).normalized;
                movePosition = playerTarget.position;
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minorPlayerArea);
        Gizmos.DrawWireSphere(transform.position, majorPlayerArea);
    }


    private void FixedUpdate()
    {
        if (isAlive && !isKnockback)
        {
            //rig.velocity = new Vector2(movePosition.x, movePosition.y) * speed;
            //agent.destination = movePosition;
            agent.SetDestination(movePosition);
            agent.speed = speed;
        }
    }

    public void GetDamage(float dmg)
    {
        currentLife -= dmg;

        lifeBar.fillAmount = currentLife / totalLife;

        StartCoroutine(DmgEffect());

        if (currentLife <= 0)
        {
            Die();
        }
    }

    IEnumerator DmgEffect()
    {
        Color32 currentColor = new Color(spriteR.color.r, spriteR.color.g, spriteR.color.b);

        spriteR.color = new Color32(currentColor.r, currentColor.g, currentColor.b, 125);
        yield return new WaitForSeconds(0.2f);
        spriteR.color = new Color32(currentColor.r, currentColor.g, currentColor.b, 255);
    }

    private void Die()
    {
        //if (dotObj != null)
        //    Destroy(dotObj.gameObject);
        Audio_Controller.instance.PlaySFX(explosionDieClip);
        isAlive = false;
        //agent.isStopped = true;
        rig.velocity = Vector2.zero;
        canvasBar.gameObject.SetActive(false);
        anim.SetTrigger("die");
        Destroy(gameObject, 0.5f);
    }

    private void ExplosionItSelf()
    {
        //if (dotObj != null)
        //    Destroy(dotObj.gameObject);
        isAlive = false;
        agent.isStopped = true;
        rig.velocity = Vector2.zero;        
        canvasBar.gameObject.SetActive(false);
        anim.SetTrigger("explosion");
        Destroy(gameObject, 0.7f);
    }

    public void ExplosionEffect()
    {
        Audio_Controller.instance.PlaySFX(explosionClip);
        GameObject obj = Instantiate(explosion, transform.position, transform.rotation);
        obj.GetComponent<Explosion>().ReceivingDmg(dmgExplosion);
    }

    private void OnDestroy()
    {
        if (!isSonOfBoss)
        {
            //FindObjectOfType<WaveSystem_Controller>().UpdateAmountEnemiesInScene();
            var obj = FindObjectOfType<WaveSystem_Controller>();
            if (obj != null)
                obj.UpdateAmountEnemiesInScene();
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //|| collision.gameObject.CompareTag("Towers")
    //    if (collision.gameObject.CompareTag("Base") || collision.gameObject.CompareTag("Player"))
    //    {
    //        ExplosionItSelf();
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            ExplosionItSelf();
            //agent.isStopped = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage(dmgCollision);
        }

        //if (collision.gameObject.CompareTag("Tower"))
        //{
        //    collision.gameObject.GetComponent<Tower>().GetDamage(dmgCollision);
        //}
    }

    public void HasKnockback(Vector3 collision, float force, float time)
    {
        //Vector2 dir = agent.destination - collision.transform.position;
        Vector2 dir = collision - transform.position;
        //Vector2 dir = transform.position - collision.transform.position;
        StartCoroutine(Knockback(dir, force, time));
    }

    IEnumerator Knockback(Vector2 dir, float force, float time)
    {
        isKnockback = true;
        agent.enabled = false;
        rig.bodyType = RigidbodyType2D.Dynamic;
        rig.AddForce(dir * -force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(time);
        rig.velocity = Vector2.zero;
        rig.bodyType = RigidbodyType2D.Kinematic;
        agent.enabled = true;
        isKnockback = false;
    }

}
