using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private float force;

    [SerializeField] private float forceMultiplier;
    
    private AudioSource _speaker;
    // How often the force is applied in Hz
    [SerializeField] private float forceFrequency;

    private Rigidbody _rigidbody;

    public AudioClip collisionClip;
    
    public bool wallCollisionSound;
    public float collisionVolume;

    public void Start()
    {
        _speaker = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();

        _speaker.playOnAwake = false;
        _speaker.clip = collisionClip;
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

    private void OnCollisionEnter(Collision collision)
    {
         _speaker.volume = collisionVolume;

        if (wallCollisionSound)
        {
            _speaker.Play();
        }
        else if (collision.gameObject.name == "PlayerBeyblade" || collision.gameObject.name == "EnemyBeyblade")
        {
        _speaker.Play();
        }
    }
}
