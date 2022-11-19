using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnholdableObject : InteractiveObject
{
    [SerializeField] private bool changeMaterial;
    [SerializeField] private Material newMaterial;
    [SerializeField] private List<GameObject> triggerObjects;

    protected override void ActivationWrapper()
    {
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (changeMaterial)
        {
            gameObject.GetComponent<Renderer>().material = newMaterial;
        }
        triggerObjects.ForEach(obj => obj.GetComponent<InteractiveObject>().Activate());
    }
}
