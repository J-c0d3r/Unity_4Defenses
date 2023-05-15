using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_Effect : MonoBehaviour
{
    private SpriteRenderer sr;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        sr.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        sr.enabled = false;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    sr.enabled = true;
    //}
}
