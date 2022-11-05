using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Assertions;

public class InteractableObject : MonoBehaviour, IInteractiveObject
{
    [SerializeField] private float inventoryZ = 1;
    [SerializeField] private string targetColliderName;
    [SerializeField] private List<GameObject> triggerObjects;
    [SerializeField] private float speedDuringActivation = 1.0f;
    [SerializeField] private float speedDuringDragging = 2.0f;

    private Vector3 _inventoryPos; // TODO: Change this to private once the inventory manager script is finished
    private bool _onTarget = false;
    // A tuple of representing the state of the object. It consists of the object's activation status and movement.
    private (Status, Movement) _state = (Status.Inactive, Movement.Stationary);
    private readonly Vector3 _centerOfScreen = new Vector3(0.0f, 0.8f, -10.0f);
    
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
        var transform1 = transform;
        transform1.position = _state switch
        {
            (Status.Activating, _) => Move(_centerOfScreen, speedDuringActivation),
            (Status.Stashing, _) => Move(_inventoryPos, speedDuringActivation),
            (Status.Active, Movement.Dragging) => CalcMousePos(),
            (Status.Active, Movement.Returning) => Move(_inventoryPos, speedDuringDragging),
            _ => (transform1).position
        };
    }
    
    void OnMouseDown()
    {
        switch (_state.Item1)
        {
            case Status.Inactive:
                _state.Item1 = Status.Activating;
                _inventoryPos = EventController.Instance.PlaceInInventory(gameObject.GetInstanceID());
                break;
            case Status.Active:
                _state.Item2 = Movement.Dragging;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                break;
            case Status.Activating:
            case Status.Stashing:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void OnMouseUp()
    {
        if (_state != (Status.Active, Movement.Dragging)) return;
        
        _state.Item2 = Movement.Returning;
        if (_onTarget) Use();
    }

    public void Activate()
    {
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }
    
    private void Use()
    {
        EventController.Instance.PlayerUse(gameObject.GetInstanceID());
        EventController.Instance.removeItem(gameObject.GetInstanceID());
        triggerObjects.ForEach(obj => obj.GetComponent<IInteractiveObject>().Activate());
        Debug.Log("Trigger Event!");
        Destroy(this.gameObject);
    }

    private Vector3 Move(Vector3 dest, float speed)
    {
        if (Vector3.Distance(transform.position, dest) > 0.001f)
        {
            var step = speed * Time.deltaTime;
            return Vector3.MoveTowards(transform.position, dest, step);
        }
        
        TransitionToStationary();
        return transform.position;
    }

    private void TransitionToStationary()
    {
        _state = _state switch
        {
            (Status.Activating, _) => (Status.Stashing, _state.Item2),
            (Status.Stashing, _) => (Status.Active, _state.Item2),
            (_, Movement.Returning) => (_state.Item1, Movement.Stationary),
            _ => _state
        };
    }

    void OnTriggerEnter(Collider other)
    {
        Assert.AreNotEqual("", targetColliderName, "targetColliderName is empty");
        if (other.name == targetColliderName)
        {
            _onTarget = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        Assert.AreNotEqual("", targetColliderName, "targetColliderName is empty");
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
