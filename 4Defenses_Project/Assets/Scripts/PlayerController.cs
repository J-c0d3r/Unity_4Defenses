using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isAlive;

    [Header("Attributes")]
    [SerializeField] private float totalLife;
    [SerializeField] private float currentLife;
    [SerializeField] private float speed;


    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField] private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLife = totalLife;
    }


    void Update()
    {
        Move();
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
        currentLife -= dmg;

        //lifeBar.fillAmount = currentLife / totalLife;

        if (currentLife <= 0)
        {
            Debug.Log("morri");
        }
    }

}
