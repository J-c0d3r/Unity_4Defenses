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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            gunMG.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W))
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
        transform.position += moveDirect * speed * Time.deltaTime;


        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);



    }
}
