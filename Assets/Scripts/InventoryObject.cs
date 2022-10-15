using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    [SerializeField] private float inventoryZ = 1;
    [SerializeField] private string itemName;
    [SerializeField] private float returnSpeed = 0.015f;
    [SerializeField] private string targetObjectName;
    [SerializeField] private List<GameObject> triggerObjects;
    public Vector3 defaultPos;
    private bool _inTargetObject = false;
    private bool _dragging = false;

    private void OnMouseDown()
    {
        _dragging = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnMouseUp()
    {
        _dragging = false;
        if (_inTargetObject)
        {
            EventController.Instance.PlayerUse(gameObject);
            foreach (var interactableObj in triggerObjects)
            {
                interactableObj.SetActive(true);
            }
            Debug.Log("Trigger Event!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging)
        {
            Vector3 inputMousePos = Input.mousePosition;
            inputMousePos.z = inventoryZ;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputMousePos);
            //Debug.Log(mousePosition);
            transform.position = mousePosition;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, defaultPos, returnSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == targetObjectName)
        {
            _inTargetObject = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == targetObjectName)
        {
            _inTargetObject = false;
        }
    }
}
