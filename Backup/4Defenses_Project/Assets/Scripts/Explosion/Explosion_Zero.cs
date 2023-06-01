using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Zero : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.25f);
    }
}
