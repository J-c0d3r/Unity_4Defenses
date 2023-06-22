using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BossEntity : MonoBehaviour
{

    protected bool isAlive;

    [Header("Attributes")]
    [SerializeField] protected float totalLife;
    [SerializeField] protected float currentLife;
    [SerializeField] protected float minSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float speed;
    [SerializeField] protected float dmgExplosion;
    [SerializeField] protected float dmgCollision;


    [Header("Life Bar")]
    public Image lifeBar;
    public GameObject canvasBar;


    protected Animator anim;
    protected Rigidbody2D rig;
    protected Vector2 movePosition;
    protected Collider2D boxCollider;

    [Header("Associations")]
    [SerializeField] protected Transform baseTarget;
    [SerializeField] protected Transform playerTarget;
    [SerializeField] protected GameObject explosionAtk;
    [SerializeField] protected AudioClip explosionAtkClip;
    [SerializeField] protected List<Transform> spawnPoints;
    [SerializeField] protected List<GameObject> minionsList;

    public DamageOverTime dotObj;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<Collider2D>();
        baseTarget = GameObject.FindGameObjectWithTag("Base").transform;

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        //speed = Random.Range(minSpeed, maxSpeed);

        currentLife = totalLife;
        isAlive = true;
    }


    public abstract void GetDamage(float dmg);

    protected abstract void Die();

    protected abstract void OnTriggerEnter2D(Collider2D collision);
    protected abstract void OnCollisionEnter2D(Collision2D collision);    
}
