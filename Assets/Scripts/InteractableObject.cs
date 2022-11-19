using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Assertions;

public class InteractableObject : InteractiveObject
{
    [SerializeField] private List<GameObject> triggerObjects;
    [SerializeField] private List<GameObject> trapTriggerObjects;
    
    [Header("Dragging Settings")]
    [SerializeField] private float speedDuringActivation = 1.0f;
    [SerializeField] private float speedDuringDragging = 2.0f;
    [SerializeField] private float draggingPlaneZ = 7f;

    // A tuple of representing the state of the object. It consists of the object's activation status and movement.
    private (Status, Movement) _state = (Status.Inactive, Movement.Stationary);
    private Vector3 _inventoryPos;
    private string _targetColliderName;
    private string _trapColliderName;
    private bool _onTarget = false;
    private bool _onTrap = false;

    private readonly Action<GameObject> _activate = obj => { obj.GetComponent<InteractiveObject>().Activate(); };

    private void Awake()
    {
        var objName = gameObject.name;
        _targetColliderName = objName + "Target";
        _trapColliderName = objName + "Trap";
    }

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
            (Status.Activating, _) => Move(EventController.Instance.defaultPos, speedDuringActivation),
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
        if (_onTrap)
        {
            trapTriggerObjects.ForEach(_activate);
        }
    }
    
    protected override void ActivationWrapper()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }
    
    private void Use()
    {
        EventController.Instance.PlayerUse(gameObject.GetInstanceID());
        EventController.Instance.RemoveItem(gameObject.GetInstanceID());
        triggerObjects.ForEach(_activate);
        Destroy(gameObject);
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
        Assert.AreNotEqual("", _targetColliderName, "targetColliderName is empty");
        string otherName = other.name;
        _onTarget = otherName.Equals(_targetColliderName) || _onTarget;
        _onTrap = otherName.Equals(_trapColliderName) || _onTrap;
    }
    
    void OnTriggerExit(Collider other)
    {
        Assert.AreNotEqual("", _targetColliderName, "targetColliderName is empty");
        string otherName = other.name;
        _onTarget = !otherName.Equals(_targetColliderName) && _onTarget;
        _onTrap = !otherName.Equals(_trapColliderName) && _onTrap;
    }

    private Vector3 CalcMousePos()
    {
        var inputMousePos = Input.mousePosition;
        inputMousePos.z = draggingPlaneZ;
        return EventController.Instance.mainCamera.ScreenToWorldPoint(inputMousePos);
    }
}
