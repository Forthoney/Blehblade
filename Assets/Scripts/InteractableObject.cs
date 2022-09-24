using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject eventControl;
    private void OnMouseDown()
    {
        BringToForeground();
        eventControl.GetComponent<EventController>().PlayerUse(this);
    }

    private void BringToForeground()
    {
        ChangeSprite();
    }

    //Maybe we won't change the sprite
    private void ChangeSprite()
    {
        throw new NotImplementedException();
    }
}
