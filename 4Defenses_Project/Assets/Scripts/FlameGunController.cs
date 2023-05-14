using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGunController : MonoBehaviour
{
    private bool isInstantiated;
    
    [SerializeField] private GameObject proj;
    [SerializeField] private Transform projPoint;

    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !isInstantiated)
        {
            go = Instantiate(proj, projPoint.position, projPoint.rotation);
        }

        if (isInstantiated)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            go.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }


    }
}
