using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    [SerializeField] private GameObject eventControl;
    private bool dragging = false;
    [SerializeField] private float inventoryZ = 1;
    [SerializeField] private string itemName;
    [SerializeField] private float returnSpeed = 0.015f;
    public Vector3 defaultPos;
    public int pickedIndex;
    [SerializeField] private string targetObjectName;
    private bool inTargetObject = false;
    
    
    private void OnMouseDown()
    {
        dragging = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        if (inTargetObject)
        {
            Debug.Log("Trigger Event!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector3 inputMousePosi = Input.mousePosition;
            inputMousePosi.z = inventoryZ;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputMousePosi);
            //Debug.Log(mousePosition);
            this.transform.position = mousePosition;
        }
        else
        {
            this.transform.position = Vector3.Lerp(transform.position, defaultPos, returnSpeed);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision enter");
        if (collider.name == targetObjectName)
        {
            Debug.Log("Collided with target!");
            inTargetObject = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Collision exit");
        if (collider.name == targetObjectName)
        {
            Debug.Log("No longer colliding with target");
            inTargetObject = false;
        }
    }
}
