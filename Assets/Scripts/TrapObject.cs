using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : InteractiveObject
{
    protected override void ActivationWrapper()
    {
        EventController.Instance.Defeat();
    }
}
