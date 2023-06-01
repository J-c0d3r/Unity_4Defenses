using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MousePointer_UI : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.visible = false;
        }
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        //mouse.z = 0.5f;
        //pointer.position = Camera.main.ScreenToWorldPoint(mouse);
        //transform.position = Camera.main.ScreenToWorldPoint(mouse);
        //transform.position = Camera.main.ScreenToViewportPoint(mouse);
        transform.position = mouse;
    }
}
