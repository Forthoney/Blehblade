using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private float force;
    private Rigidbody _rigidbody;
    private bool _inEndSequence = false;

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
        if (_inEndSequence) return;
        AddRandomForce(EventController.Instance.remainingTime / EventController.Instance.time * force);
    }

    private void AddRandomForce(float velocity)
    {
        var randomForce = Random.insideUnitCircle * velocity;
        _rigidbody.AddForce(new Vector3(randomForce.x, 0, randomForce.y));
    }

    public void Decelerate()
    {
        _inEndSequence = true;
        _rigidbody.AddForce(-_rigidbody.velocity);
    }

    public void PushTo(Vector3 direction)
    {
        _inEndSequence = true;
        _rigidbody.AddForce(direction);
    }
}
