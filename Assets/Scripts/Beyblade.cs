using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private float force;
    [SerializeField] private float forceMultiplier;
    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void StartBeyblade()
    {
        _rigidbody.isKinematic = false;
        force *= forceMultiplier;
        AddRandomForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        if (force < 0.01f) return;
        AddRandomForce(EventController.Instance.remainingTime / EventController.Instance.time * force);
    }

    private void AddRandomForce(float velocity)
    {
        var randomForce = Random.insideUnitCircle * velocity;
        _rigidbody.AddForce(new Vector3(randomForce.x, 0, randomForce.y));
    }
}
