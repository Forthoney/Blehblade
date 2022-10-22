using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private float force;
    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void StartBeyblade()
    {
        _rigidbody.isKinematic = false;
        AddRandomForce(force);
        force = isPlayer? force / 10 : force / 20;
    }

    // Update is called once per frame
    void Update()
    {
        AddRandomForce(EventController.Instance.remainingTime / EventController.Instance.time * force);
    }

    private void AddRandomForce(float velocity)
    {
        var randomForce = Random.insideUnitCircle * velocity;
        _rigidbody.AddForce(new Vector3(randomForce.x, 0, randomForce.y));
    }

    public void Decelerate()
    {
        _rigidbody.AddForce(-_rigidbody.velocity);
        _rigidbody.isKinematic = true;
    }

    public void MoveAwayFrom(Vector3 otherPosition)
    {
        _rigidbody.AddForce(otherPosition - gameObject.transform.position);
        _rigidbody.isKinematic = true;
    }
}
