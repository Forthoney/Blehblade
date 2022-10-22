using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private float initialVelocity;
    private float _currentVelocity;
    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void StartBeyblade()
    {
        _rigidbody.isKinematic = false;
        _currentVelocity = isPlayer? initialVelocity / 10 : initialVelocity / 20;
        AddRandomForce(initialVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentVelocity < 0.01f) return;
        AddRandomForce(EventController.Instance.remainingTime / EventController.Instance.time * _currentVelocity);
    }

    private void AddRandomForce(float velocity)
    {
        var randomForce = Random.insideUnitCircle * velocity;
        _rigidbody.AddForce(new Vector3(randomForce.x, 0, randomForce.y));
    }
}
