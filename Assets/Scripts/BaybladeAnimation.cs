using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaybladeAnimation : MonoBehaviour
{
    public int framePerUpdate = 5;
    public int currentFrame = 0;
    public int currentAsset = 0;
    public List<Material> lm;
    public MeshRenderer mr;

    // Update is called once per frame
    void Update()
    {
        if (currentFrame++ > framePerUpdate)
        {
            currentFrame = 0;
            if (currentAsset == 0)
            {
                currentAsset = 1;
            }
            else
            {
                currentAsset = 0;
            }
            mr.material = lm[currentAsset];
        }
    }
}
