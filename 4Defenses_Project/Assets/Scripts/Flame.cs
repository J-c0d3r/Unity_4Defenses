using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private bool first;
    [SerializeField] private bool second;
    [SerializeField] private bool third;
    [SerializeField] private bool fourth;
    
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetInteger("start", Random.Range(0, 2));
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        //if (first)
        //{            
        //    transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(transform.rotation.z, angle, 0.25f));
        //}

        //if (second)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(transform.rotation.z, angle, 0.5f));
        //}

        //if (third)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(transform.rotation.z, angle, 0.75f));
        //}

        //if (fourth)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(transform.rotation.z, angle, 1f));
        //}

    }
}
