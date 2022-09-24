using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactions", menuName = "ScriptableObjects/Interactions", order = 1)]
public class Stage1Interaction : ScriptableObject
{
    public List<GameObject> interactionStarter;
    public List<GameObject> interactionSubject;
}
