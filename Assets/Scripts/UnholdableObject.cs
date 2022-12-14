using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnholdableObject : InteractiveObject
{
    [SerializeField] private int useTriggerDialogueIndex = -1;
    [SerializeField] private bool changeMaterial;
    [SerializeField] private Material newMaterial;
    [SerializeField] private List<GameObject> triggerObjects;
    [SerializeField] private List<GameObject> deactivateOnTrigger;
    private bool _isUsed = false;

    protected override void ActivationWrapper()
    {
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (_isUsed) return;
        
        if (changeMaterial)
        {
            gameObject.GetComponent<Renderer>().material = newMaterial;
        }
        triggerObjects.ForEach(activation);
        deactivateOnTrigger.ForEach(obj => obj.SetActive(false));
        EventController.Instance.DisplayDialogue(useTriggerDialogueIndex);
        _isUsed = true;
    }
}
