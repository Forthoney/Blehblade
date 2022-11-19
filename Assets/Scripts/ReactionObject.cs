using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]


public class ReactionObject : InteractiveObject
{
    [System.Serializable]
    private struct ReactionPair
    {
        // Can't make Vector3 nullable because we need it to be Serializable
        public Material mat;
        public bool hasPos;
        public Vector3 pos;
        public bool hasRot;
        public Vector3 rot;
        public float delay;
    }
    
    [SerializeField] private List<ReactionPair> reactions;
    public AudioSource audioSource;
    [SerializeField ]public AudioClip clip;
    public float volume = 0.9f;

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

    protected override void ActivationWrapper()
    {
        gameObject.SetActive(true);
        StartCoroutine(RunReaction());
        audioSource.PlayOneShot(clip, volume);
    }
}
