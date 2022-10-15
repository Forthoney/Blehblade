using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private float inventoryZ = 1;
    [SerializeField] private string itemName;
    [SerializeField] private string targetColliderName;
    [SerializeField] private List<GameObject> triggerObjects;
    [SerializeField] private float speedDuringActivation = 1.0f;
    [SerializeField] private float speedDuringDragging = 2.0f;
    
    public Vector3 inventoryPos;
    private bool _inTargetObject = false;
    private readonly Vector3 _centerOfScreen = new Vector3(0.0f, 0.8f, -10.0f);
    private (Status, Movement) _state = (Status.Inactive, Movement.Stationary);

    private enum Status
    {
        Inactive,
        Activating,
        Stashing,
        Active
    }

    private enum Movement
    {
        Dragging,
        Moving,
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
            (Status.Active, Movement.Moving) => Move(CalcMousePos(), speedDuringDragging),
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
                _state.Item2 = Movement.Moving;
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
        if (_state == (Status.Active, Movement.Dragging))
        {
            _state.Item2 = Movement.Returning;
            if (!_inTargetObject) return;
        
            EventController.Instance.PlayerUse(gameObject);
            foreach (var obj in triggerObjects)
            {
                obj.SetActive(true);
            }
            Debug.Log("Trigger Event!");
        }
    }

    private Vector3 Move(Vector3 dest, float speed)
    {
        if (Vector3.Distance(transform.position, dest) < 0.001f)
        {
            _state = _state switch
            {
                (Status.Activating, _) => (Status.Stashing, _state.Item2),
                (Status.Stashing, _) => (Status.Active, _state.Item2),
                (_, Movement.Moving) => (_state.Item1, Movement.Dragging),
                (_, Movement.Returning) => (_state.Item1, Movement.Stationary),
                _ => _state
            };
            return transform.position;
        }
        
        var step = speed * Time.deltaTime;
        return Vector3.MoveTowards(transform.position, dest, step);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.name == targetColliderName)
        {
            _inTargetObject = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == targetColliderName)
        {
            _inTargetObject = false;
        }
    }

    private Vector3 CalcMousePos()
    {
        var inputMousePos = Input.mousePosition;
        inputMousePos.z = inventoryZ;
        var mousePosition = Camera.main.ScreenToWorldPoint(inputMousePos);
        //Debug.Log(mousePosition);
        return mousePosition;
    }
}
