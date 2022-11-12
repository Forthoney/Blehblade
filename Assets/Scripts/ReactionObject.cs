using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionObject : MonoBehaviour, IInteractiveObject
{
    [SerializeField] private bool changeMaterial;
    [SerializeField] private Material newMaterial;
    [SerializeField] private bool changePosition;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private int numTriggersNeeded = 1;

    private int _triggered = 0;
    public void Activate()
    {
        Debug.Log(_triggered);
        if (++_triggered != numTriggersNeeded) return;
        gameObject.SetActive(true);
        if (changeMaterial)
        {
            gameObject.GetComponent<Renderer>().material = newMaterial;
        }

        if (changePosition)
        {
            Debug.Log("Activating new position");
            gameObject.transform.localPosition = newPosition;
        }
    }
}
