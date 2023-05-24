using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool isSonOfBoss;
    private bool isAlive;    
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
    //private WaveSystem_Controller movePosition;?

    [Header("Associations")]
    [SerializeField] private Transform baseTarget;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosion0;


    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        baseTarget = GameObject.FindGameObjectWithTag("Base").transform;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        speed = Random.Range(minSpeed, maxSpeed);
        currentLife = totalLife;
        isAlive = true;

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
                    movePosition = (playerTarget.position - transform.position).normalized;
                    timeCountToFollowPlayer = 0f;
                    //Debug.Log("minor area");
                }

                //intermediate/between area/s
                if (timeCountToFollowPlayer == 0f)
                {
                    movePosition = (playerTarget.position - transform.position).normalized;
                    //Debug.Log("inter area");
                }

                //major area
                if (distance > majorPlayerArea)
                {
                    movePosition = (baseTarget.position - transform.position).normalized;
                    timeCountToFollowPlayer += Time.deltaTime;
                    //Debug.Log("major area");
                }
            }
            else
            {
                movePosition = (playerTarget.position - transform.position).normalized;
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
        if (isAlive)
        {
            rig.velocity = new Vector2(movePosition.x, movePosition.y) * speed;
        }
    }

    public void GetDamage(float dmg)
    {
        currentLife -= dmg;

        lifeBar.fillAmount = currentLife / totalLife;

        if (currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        rig.velocity = Vector2.zero;
        canvasBar.gameObject.SetActive(false);
        anim.SetTrigger("die");
        Destroy(gameObject, 0.5f);
    }

    private void ExplosionItSelf()
    {
        isAlive = false;
        rig.velocity = Vector2.zero;
        canvasBar.gameObject.SetActive(false);
        anim.SetTrigger("explosion");
        Destroy(gameObject, 0.7f);
    }

    public void ExplosionEffect()
    {
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

}
