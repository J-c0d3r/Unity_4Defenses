using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointer : MonoBehaviour
{
    public bool isStoped;    

    private SpriteRenderer spriteR;
    

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();        
    }

    private void FixedUpdate()
    {
        if (!isStoped && EventSystem.current.IsPointerOverGameObject())
        {            
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (!isStoped)
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = 0.5f;            
            transform.position = Camera.main.ScreenToWorldPoint(mouse);
        }        
    }

    public void ChangeSpriteAim(SpriteRenderer sr)
    {
        spriteR.sprite = sr.sprite;
    }
}
