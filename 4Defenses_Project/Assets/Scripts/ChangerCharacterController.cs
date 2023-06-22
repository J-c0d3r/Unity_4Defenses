using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangerCharacterController : MonoBehaviour
{
    private bool canChange;
    private Player_ChangeCharacter playerCharacter;

    [SerializeField] private RuntimeAnimatorController anim;
    [SerializeField] private GameObject messageUI;
    [SerializeField] private Sprite spriteFace;
    [SerializeField] private Image imageUIFace;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canChange)
        {
            if (playerCharacter != null)
            {
                playerCharacter.ChangeCharacter(anim);
                imageUIFace.sprite = spriteFace;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            messageUI.SetActive(true);
            canChange = true;
            playerCharacter = collision.gameObject.GetComponent<Player_ChangeCharacter>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        messageUI.SetActive(false);
        canChange = false;
        playerCharacter = null;
    }
}
