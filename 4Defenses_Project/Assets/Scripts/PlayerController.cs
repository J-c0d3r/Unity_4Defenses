using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    [SerializeField] private float speed;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject gunMG;
    [SerializeField] private GameObject gunFlame;

    void Start()
    {

    }


    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            gunMG.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            gunFlame.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            gunMG.SetActive(false);
            gunFlame.SetActive(false);
        }

    }

    private void Move()
    {
        Vector3 moveDirect = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        // moveDirect.Normalize();
        transform.position += moveDirect * speed * Time.deltaTime;
        if (moveDirect.x == 0 && moveDirect.y == 0)
        {
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }


        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        direction.Normalize();

        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);


        // Debug.Log(transform.position.x);
        // Debug.Log("Before: " + mousePos.x);
        // Debug.Log("After: " + direction.x);


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("colidiu");
    }
}
