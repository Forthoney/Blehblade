using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private float inventoryZ = 1;
    [SerializeField] private string targetColliderName;
    [SerializeField] private List<GameObject> triggerObjects;
    [SerializeField] private float speedDuringActivation = 1.0f;
    [SerializeField] private float speedDuringDragging = 2.0f;

    public Vector3 inventoryPos; // TODO: Change this to private once the inventory manager script is finished
    private bool _onTarget = false;
    // A tuple of representing the state of the object. It consists of the object's activation status and movement.
    // You can tell I was certainly inspired by CS1010's Finite State Machines
    private (Status, Movement) _state = (Status.Inactive, Movement.Stationary);
    private readonly Vector3 _centerOfScreen = new Vector3(0.0f, 0.8f, -10.0f);

    // Inactive and Active are self explanatory. Activating and Stashing represent the status of the object while
    // it is first being presented to the player.
    private enum Status
    {
        Inactive,
        // Flying to the foreground for the user to inspect when first clicking on the object
        Activating,
        // Flying to the inventory after the user first inspected it
        Stashing,
        Active
    }

    private enum Movement
    {
        Dragging,
        // Returning to inventory
        Returning,
        Stationary,
    }
    
    void Update()
    {
        transform.position = _state switch
        {
            (Status.Activating, _) => Move(_centerOfScreen, speedDuringActivation),
            (Status.Stashing, _) => Move(inventoryPos, speedDuringActivation),
            (Status.Active, Movement.Dragging) => CalcMousePos(),
            (Status.Active, Movement.Returning) => Move(inventoryPos, speedDuringDragging),
            _ => transform.position
        };
    }
    
    private void OnMouseDown()
    {
        switch (_state.Item1)
        {
            case Status.Inactive:
                _state.Item1 = Status.Activating;
                break;
            case Status.Active:
                _state.Item2 = Movement.Dragging;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                break;
            case Status.Stashing:
            case Status.Activating:
                break;
            default:
                Debug.LogError("Unexpected case in update");
                break;
        }
    }
    
    private void OnMouseUp()
    {
        if (_state != (Status.Active, Movement.Dragging)) return;
        
        _state.Item2 = Movement.Returning;
        if (!_onTarget) return;
        
        EventController.Instance.PlayerUse(gameObject);
        triggerObjects.ForEach(obj => obj.SetActive(true));
        Debug.Log("Trigger Event!");
    }

    private Vector3 Move(Vector3 dest, float speed)
    {
        if (Vector3.Distance(transform.position, dest) > 0.001f)
        {
            var step = speed * Time.deltaTime;
            return Vector3.MoveTowards(transform.position, dest, step);
        }
        
        // If finished moving, change state but not transform
        _state = _state switch
        {
            (Status.Activating, _) => (Status.Stashing, _state.Item2),
            (Status.Stashing, _) => (Status.Active, _state.Item2),
            (_, Movement.Returning) => (_state.Item1, Movement.Stationary),
            _ => _state
        };
        return transform.position;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Assert.AreNotEqual("", targetColliderName);
        if (other.name == targetColliderName)
        {
            _onTarget = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Assert.AreNotEqual("", targetColliderName);
        if (other.name == targetColliderName)
        {
            _onTarget = false;
        }
    }

    private Vector3 CalcMousePos()
    {
        var inputMousePos = Input.mousePosition;
        inputMousePos.z = inventoryZ;
        return EventController.Instance.mainCamera.ScreenToWorldPoint(inputMousePos);
    }
}
