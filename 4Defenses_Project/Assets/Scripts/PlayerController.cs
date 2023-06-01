using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool wasGameFinished;
    public bool canMove;
    private bool isAlive;
    private bool wasDisableShoot;
    private float timecountCanGetDmg;

    [Header("Attributes")]
    [SerializeField] private float timeCanGetDmg;
    [SerializeField] private float totalLife;
    [SerializeField] private float currentLife;
    [SerializeField] private float speed;


    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Image lifeBar;
    [SerializeField] private SpriteRenderer spriteR;
    [SerializeField] private GameObject weaponsCont;
    [SerializeField] private AudioClip powerUpLifeClip;

    [SerializeField] private List<AudioClip> hitClipList = new List<AudioClip>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLife = totalLife;
        canMove = true;
        isAlive = true;
        wasDisableShoot = true;
    }


    void Update()
    {
        timecountCanGetDmg += Time.deltaTime;
        if (isAlive && canMove)
        {
            Move();
            if (wasDisableShoot)
            {
                wasDisableShoot = false;
                weaponsCont.GetComponent<WeaponsController>().UnlockAllWeapons(true);
            }

        }
        else
        {
            movement = Vector2.zero;
            if (!wasDisableShoot)
            {
                wasDisableShoot = true;
                weaponsCont.GetComponent<WeaponsController>().UnlockAllWeapons(false);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed * Time.fixedDeltaTime;

        if (movement == Vector2.zero)
        {
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }
    }

    private void Move()
    {
        //PlayerMovimentation
        //Vector2 moveDirect = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //moveDirect.Normalize();
        //transform.position += moveDirect * speed * Time.deltaTime;


        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
        //Debug.Log(rb.velocity);
        ////if (moveDirect.x == 0 && moveDirect.y == 0)
        //if (rb.velocity == Vector2.zero)
        //{
        //    anim.SetFloat("Speed", 0);

        //    //rb.velocity = Vector2.zero;
        //}
        //else
        //{
        //    //rb.velocity = moveDirect * speed * Time.deltaTime;

        //    anim.SetFloat("Speed", 1);
        //}

        //PlayerMovimentation
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal_Keyboard") || Input.GetButton("Vertical_Keyboard"))
        {
            movement.x = Input.GetAxisRaw("Horizontal_Keyboard");
            movement.y = Input.GetAxisRaw("Vertical_Keyboard");
            movement.Normalize();
        }





        //if (Input.GetButton("Horizontal_Keyboard") || Input.GetButton("Vertical_Keyboard"))
        //{
        //    movement.x = Input.GetAxis("Horizontal_Keyboard");
        //    movement.y = Input.GetAxis("Vertical_Keyboard");
        //    //movement.Normalize();
        //    Debug.Log("IF");
        //    //Debug.Log(movement);
        //}
        //else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        //{
        //    movement.x = Input.GetAxisRaw("Horizontal");
        //    movement.y = Input.GetAxisRaw("Vertical");
        //    Debug.Log("ELSE");
        //}
        //else
        //{
        //    movement = Vector2.zero;
        //}






        //MousePosition
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        direction.Normalize();

        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);


    }

    public void GetDamage(float dmg)
    {
        if (timecountCanGetDmg >= timeCanGetDmg && isAlive && !wasGameFinished)
        {
            Audio_Controller.instance.PlaySFX(hitClipList[Random.Range(0, hitClipList.Count)]);
            timecountCanGetDmg = 0f;
            StartCoroutine(DmgEffect());
            currentLife -= dmg;

            lifeBar.fillAmount = currentLife / totalLife;

            if (currentLife <= 0)
            {
                wasGameFinished = true;
                isAlive = false;
                Time.timeScale = 0f;
                //FindObjectOfType<GameManager>().ShowGameOver();
                GameManager.instance.ShowGameOver();
            }
        }
    }

    IEnumerator DmgEffect()
    {
        spriteR.color = new Color32(255, 88, 66, 255);
        yield return new WaitForSeconds(0.2f);
        spriteR.color = new Color32(255, 255, 255, 255);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "gameOver")
        {
            FindObjectOfType<GameManager>().ShowGameOver();
        }

        if (collision.gameObject.name == "victory")
        {
            FindObjectOfType<GameManager>().ShowVictory();
        }

        if (collision.gameObject.CompareTag("Life"))
        {
            if (currentLife < totalLife)
            {
                Audio_Controller.instance.PlaySFX(powerUpLifeClip);
                float lifeDiff = totalLife - currentLife;
                if (lifeDiff < 10)
                {
                    currentLife += lifeDiff;
                }
                else
                {
                    currentLife += 10;
                }

                lifeBar.fillAmount = currentLife / totalLife;

                Destroy(collision.gameObject);
            }
        }
    }

}
