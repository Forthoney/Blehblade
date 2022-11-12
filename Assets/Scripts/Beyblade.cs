using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private float force;
    // How often the force is applied in Hz
    [SerializeField] private float forceFrequency;
    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void StartBeyblade()
    {
        _rigidbody.isKinematic = false;
        // divide 1 sec by force frequency to get time interval to apply force
        InvokeRepeating(nameof(AddRandomForce), 0f, 1f/forceFrequency);
    }

    public void EndBeyblade()
    {
        _rigidbody.isKinematic = true;
    }
    
    private void AddRandomForce()
    {
        var velocity = EventController.Instance.remainingTime / EventController.Instance.time * force;
        var randomForce = Random.insideUnitCircle * velocity;
        _rigidbody.AddForce(new Vector3(randomForce.x, 0, randomForce.y));
    }
}
