using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]


public class ReactionObject : MonoBehaviour, IInteractiveObject
{
    [System.Serializable]
    private struct ReactionPair
    {
        public Material mat;
        public bool hasPos;
        public Vector3 pos;
        public bool hasRot;
        public Vector3 rot;
        public float delay;
    }
    
    [SerializeField] private List<ReactionPair> reactions;
    [SerializeField] private float activationDelay = 0f;
    [SerializeField] private int numTriggersNeeded = 1;

    private int _triggered = 0;
    public void Activate()
    {
        if (++_triggered != numTriggersNeeded) return;

        // Need Invoke because we cannot call StartCoroutine on an Inactive GameObject
        Invoke(nameof(RunReactionWrapper), activationDelay);
    }

    private IEnumerator RunReaction()
    {
        var objectRenderer = gameObject.GetComponent<Renderer>();
        while (reactions.Any())
        {
            yield return new WaitForSeconds(reactions[0].delay);
            ChangeCharacteristics(reactions[0], objectRenderer, gameObject.transform);
            reactions.RemoveAt(0);
        }
    }

    private void ChangeCharacteristics(ReactionPair reaction, Renderer objRenderer, Transform objTransform)
    {
        objRenderer.material = reaction.mat ? reaction.mat : objRenderer.material;
        objTransform.localPosition = reaction.hasPos ? reaction.pos : objTransform.localPosition;
        if (reaction.hasRot)
        {
            objTransform.transform.localRotation = Quaternion.Euler(reaction.rot);
        }
    }

    private void RunReactionWrapper()
    {
        gameObject.SetActive(true);
        StartCoroutine(RunReaction());
    }
}
