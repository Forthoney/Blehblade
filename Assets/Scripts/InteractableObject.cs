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
    private Status _status = Status.Inactive;
    private Movement _moving = Movement.Stationary;

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
        switch (_status)
        {
            case Status.Activating:
                Move(_centerOfScreen, speedDuringActivation);
                break;
            case Status.Stashing:
                Move(inventoryPos, speedDuringActivation);
                break;
            case Status.Active:
                switch (_moving)
                {
                    case Movement.Dragging:
                        transform.position = CalcMousePos();
                        break;
                    case Movement.Moving:
                        Move(CalcMousePos(), speedDuringDragging);
                        break;
                    case Movement.Returning:
                        Move(inventoryPos, speedDuringDragging);
                        break;
                    case Movement.Stationary:
                        break;
                }
                break;
            case Status.Inactive:
                return;
            default:
                Debug.LogError("Unexpected case in update");
                break;
        }
    }
    
    private void OnMouseDown()
    {
        switch (_status)
        {
            case Status.Inactive:
                _status = Status.Activating;
                break;
            case Status.Active:
                _moving = Movement.Moving;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                break;
            default:
                Debug.LogError("Unexpected case in update");
                break;
        }
    }
    
    private void OnMouseUp()
    {
        if (_moving != Movement.Dragging || _status != Status.Active) return;

        _moving = Movement.Returning;
        if (!_inTargetObject) return;
        
        EventController.Instance.PlayerUse(gameObject);
        foreach (var interactableObj in triggerObjects)
        {
            interactableObj.SetActive(true);
        }
        Debug.Log("Trigger Event!");
    }

    private void Move(Vector3 dest, float speed)
    {
        if (Vector3.Distance(transform.position, dest) < 0.001f)
        {
            switch (_status)
            {
                case Status.Activating:
                    _status = Status.Stashing;
                    break;
                case Status.Stashing:
                    _status = Status.Active;
                    break;
                default:
                    _moving = _moving switch
                    {
                        Movement.Moving => Movement.Dragging,
                        Movement.Returning => Movement.Stationary,
                        _ => _moving
                    };
                    break;
            }

            return;
        }
        
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, dest, step);
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
