using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeCharacter : MonoBehaviour
{

    [SerializeField] private Animator anim;

    public void ChangeCharacter(RuntimeAnimatorController animParam)
    {
        anim.runtimeAnimatorController = animParam;
    }

}
