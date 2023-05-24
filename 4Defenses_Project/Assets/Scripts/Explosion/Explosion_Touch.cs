using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Touch : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }
}
