using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject: MonoBehaviour
{
    [Header("Triggered Settings")]
    [SerializeField] protected float activationDelay = 0f;
    [SerializeField] protected int numTriggersNeeded = 1;
    private int _triggered = 0;
    
    protected readonly Action<GameObject> Activation = obj => { obj.GetComponent<InteractiveObject>().Activate(); };
    
    public void Activate()
    {
        if (++_triggered != numTriggersNeeded) return;
        Invoke(nameof(ActivationWrapper), activationDelay);
    }

    protected abstract void ActivationWrapper();
}
