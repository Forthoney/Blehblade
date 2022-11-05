using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionObject : MonoBehaviour, IInteractiveObject
{
    [SerializeField] private bool changeMaterial;
    [SerializeField] private Material newMaterial;
    [SerializeField] private bool changePosition;
    [SerializeField] private Vector3 newPosition;
    public void Activate()
    {
        if (changeMaterial)
        {
            gameObject.GetComponent<Renderer>().material = newMaterial;
        }

        if (changePosition)
        {
            Debug.Log("Activating new position");
            gameObject.transform.position = newPosition;
        }
    }
}
