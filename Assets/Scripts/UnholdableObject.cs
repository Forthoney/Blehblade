using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnholdableObject : MonoBehaviour, IInteractiveObject
{
    [SerializeField] private bool changeMaterial;
    [SerializeField] private Material newMaterial;
    [SerializeField] private List<GameObject> triggerObjects;

    public void Activate()
    {
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (changeMaterial)
        {
            gameObject.GetComponent<Renderer>().material = newMaterial;
        }
        triggerObjects.ForEach(obj => obj.GetComponent<IInteractiveObject>().Activate());
    }
}
