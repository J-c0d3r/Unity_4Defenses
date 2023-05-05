using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private float timeCount;
    [SerializeField] private float reloadShoot;


    [SerializeField] private GameObject proj;
    [SerializeField] private Transform projPoint;


    void Start()
    {
        
    }

    
    void Update()
    {
        ToShoot();
    }

    private void ToShoot()
    {
        timeCount += Time.deltaTime;
        if (Input.GetMouseButton(0) && gameObject.activeSelf)
        {            
            if (timeCount >= reloadShoot)
            {
                timeCount = 0f;
                Instantiate(proj, projPoint.position, projPoint.rotation);
                
            }
        }
    }
}
