using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject eventControl;
    private void OnMouseDown()
    {
        StartCoroutine(Select());
    }

    private void BringToForeground()
    {
        throw new NotImplementedException();
    }
    
    IEnumerator Select()
    {
        BringToForeground();
        ChangeSprite();
        yield return new WaitForSeconds(0.5f);
        eventControl.GetComponent<EventController>().PlayerUse(gameObject);
    }

    //Maybe we won't change the sprite
    private void ChangeSprite()
    {
        throw new NotImplementedException();
    }
}
