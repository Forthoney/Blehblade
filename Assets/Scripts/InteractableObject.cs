using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject correspondingInventoryObj;
    [SerializeField] private float speed = 1.0f;
    private bool _movingToForeground = false;
    private readonly Vector3 _centerOfScreen = new Vector3(0.0f, 0.8f, -10.0f);

    void Update()
    {
        if (!_movingToForeground) return;
        MoveToForeground();
    }
    
    private void OnMouseDown()
    {
        Debug.Log("Interactable Object Mouseclick");
        _movingToForeground = true;
    }

    private void MoveToForeground()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _centerOfScreen, step);
        if (Vector3.Distance(transform.position, _centerOfScreen) > 0.001f) return;
        
        correspondingInventoryObj.SetActive(true);
        Destroy(gameObject);
    }
}
