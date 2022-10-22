using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private Vector3 initSpeed;
    private bool gameRunning = false;
    [SerializeField] private GameObject eventControl;
    [SerializeField] private bool isPlayer;
    [SerializeField] private float initialVelocity;
    private float _currentVelocity;

    // Physics
    private Rigidbody _rigidbody;

    // Keystrokes
    private bool spacePressed = false;
    private bool wPressed = false;
    private bool sPressed = false;
    private bool aPressed = false;
    private bool dPressed = false;






    // Start is called before the first frame update

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void StartBeyblade()
    {
        _rigidbody.isKinematic = false;
        _currentVelocity = initialVelocity / 3;
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
        Debug.Log(velocity);
        var randomForce = Random.insideUnitCircle * velocity;
        _rigidbody.AddForce(new Vector3(randomForce.x, 0, randomForce.y));
    }
}
