using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    [SerializeField] private GameObject eventControl;
    private bool dragging = false;
    [SerializeField] private float inventoryZ = 1;
    
    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector3 inputMousePosi = Input.mousePosition;
            inputMousePosi.z = inventoryZ;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputMousePosi);
            Debug.Log(mousePosition);
            this.transform.position = mousePosition;
        }
    }
}
