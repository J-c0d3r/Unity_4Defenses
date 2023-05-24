using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePointerOptions : MonoBehaviour
{
    public SpriteRenderer mouseSprite;

    public void SelectedChild(int value)
    {
        mouseSprite.sprite = gameObject.GetComponentsInChildren<Image>()[value].sprite;
    }
}
